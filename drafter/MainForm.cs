using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace drafter {
	public partial class MainForm : Form {
		bool locked = false;
        readonly Control[] teamAPicks, teamBPicks;
		readonly Dictionary<ComboBox, int> indicesSimple, indicesFPLeft, indicesFPRight;
        readonly Dictionary<object, Dictionary<ComboBox, int>> indexDicts;
        readonly Dictionary<object, ComboBox> initialCBoxes;
		ComboBox initialCBox;
        ScreenshotViewer screenshotViewer;
        Regex cleanRe = null, parserRe = null;
        Timer timer = null;
        int message_cycles;
        int message_duration;
        Dictionary<string, Emgu.CV.Mat> heroDescriptors;

        public MainForm() {
			InitializeComponent();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            this.Icon = new Icon(assembly.GetManifestResourceStream("drafter.drafter.ico"));

            teamAPicks = new Control[] {
				c_t1b1,
				c_t1b2,
				c_t1b3,
				c_t1h1,
				c_t1h2,
				c_t1h3,
				c_t1h4,
				c_t1h5,
			};
			teamBPicks = new Control[] {
				c_t2b1,
				c_t2b2,
				c_t2b3,
				c_t2h1,
				c_t2h2,
				c_t2h3,
				c_t2h4,
				c_t2h5,
			};

			indicesSimple = new Dictionary<ComboBox, int>() {
				// Left Team Bans
				{c_t1b1, 1},
				{c_t1b2, 2},
				{c_t1b3, 3},
				// Right Team Bans
				{c_t2b1, 4},
				{c_t2b2, 5},
				{c_t2b3, 6},
				// Left Team Picks
				{c_t1h1, 7},
				{c_t1h2, 8},
				{c_t1h3, 9},
				{c_t1h4, 10},
				{c_t1h5, 11},
				// Right Team Picks
				{c_t2h1, 12},
				{c_t2h2, 13},
				{c_t2h3, 14},
				{c_t2h4, 15},
				{c_t2h5, 16}
			};
			indicesFPLeft = new Dictionary<ComboBox, int>() {
				// First Bans
				{c_t1b1, 1},
				{c_t2b1, 2},
				// Second Bans
				{c_t1b2, 3},
				{c_t2b2, 4},
				// First Pick
				{c_t1h1, 5},
				// Second Picks
				{c_t2h1, 6},
				{c_t2h2, 7},
				// Third Picks
				{c_t1h2, 8},
				{c_t1h3, 9},
				// Third Bans
				{c_t2b3, 10},
				{c_t1b3, 11},
				// Fourth Picks
				{c_t2h3, 12},
				{c_t2h4, 13},
				// Fifth Picks
				{c_t1h4, 14},
				{c_t1h5, 15},
				// Last Pick
				{c_t2h5, 16}
			};
			indicesFPRight = new Dictionary<ComboBox, int>() {
				// First Bans
				{c_t2b1, 1},
				{c_t1b1, 2},
				// Second Bans
				{c_t2b2, 3},
				{c_t1b2, 4},
				// First Pick
				{c_t2h1, 5},
				// Second Picks
				{c_t1h1, 6},
				{c_t1h2, 7},
				// Third Picks
				{c_t2h2, 8},
				{c_t2h3, 9},
				// Third Bans
				{c_t1b3, 10},
				{c_t2b3, 11},
				// Fourth Picks
				{c_t1h3, 12},
				{c_t1h4, 13},
				// Fifth Picks
				{c_t2h4, 14},
				{c_t2h5, 15},
				// Last Pick
				{c_t1h5, 16}
			};
			indexDicts = new Dictionary<object, Dictionary<ComboBox, int>>() {
				{rbSimple, indicesSimple},
				{rbFPLeft, indicesFPLeft},
				{rbFPRight, indicesFPRight}
			};
			initialCBoxes = new Dictionary<object, ComboBox>() {
				{rbSimple, c_t1b1},
				{rbFPLeft, c_t1b1},
				{rbFPRight, c_t2b1}
			};
			initialCBox = c_t1b1;
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
			switch (keyData) {
				case Keys.Control | Keys.Tab:
					jumpBetweenTeams();
					return true;
				case Keys.Control | Keys.C:
					if (copy())
						return true;
					break;
				case Keys.Control | Keys.Shift | Keys.C:
                case Keys.Control | Keys.S:
					copy(true);
					return true;
				case Keys.Control | Keys.Shift | Keys.V:
                case Keys.Control | Keys.R:
					paste();
					return true;
                case Keys.Control | Keys.W:
                    swap();
                    return true;
                case Keys.Control | Keys.Space:
                    clear();
                    return true;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		void jumpBetweenTeams() {
			if (!(ActiveControl is ComboBox))
				return;
			for (int i = 0; i < teamAPicks.Length; i++) {
				if (ActiveControl == teamAPicks[i]) {
					teamBPicks[i].Focus();
					break;
				}
				if (ActiveControl == teamBPicks[i]) {
					teamAPicks[i].Focus();
					break;
				}
			}
		}

		bool copy(bool force = false) {
			if (!force) {
				if (ActiveControl is ComboBox && ((ComboBox)ActiveControl).SelectedText != "")
					return false;
				else if (ActiveControl is TextBox && ((TextBox)ActiveControl).SelectedText != "")
					return false;
			}
			Clipboard.SetText(tResult.Text);
            indicate("Copied to clipboard!");
			return true;
		}

		void paste() {
            if (Clipboard.ContainsText())
                tryPaste(Clipboard.GetText());
            else if (Clipboard.ContainsImage())
                tryPaste(Clipboard.GetImage());
            else
                MessageBox.Show("Unsupported clipboard content.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void tryPaste(string text) {
            if (cleanRe == null)
                cleanRe = new Regex("\\s+");

            text = cleanRe.Replace(text, " ");
            if (text.Length == 0)
                return;

            if (parserRe == null)
                parserRe = new Regex("(t[12](b[1-3]|h[1-5])|battleground|win)");

            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (string chunk in text.Split('|')) {
                string[] keyval = chunk.Split('=');
                if (keyval.Length != 2)
                    continue;
                string key = keyval[0].Trim(), val = keyval[1].Trim();
                if (parserRe.Match(key).Success && val.Length != 0)
                    dict[key] = val;
            };

            if (dict.Count == 0)
                return;

            locked = true;
            foreach (ComboBox c in Controls.OfType<ComboBox>().Where(c => c != c_bg)) {
                string key = c.Name.Substring(2);
                c.Text = dict.ContainsKey(key) ? dict[key] : c.Text;
            }
            c_bg.Text = dict.ContainsKey("battleground") ? dict["battleground"] : c_bg.Text;
            if (dict.ContainsKey("win")) {
                string winner = dict["win"];
                ch_t1w.Checked = winner == "1";
                ch_t2w.Checked = winner == "2";
            }
            locked = false;
            update();
        }

        void tryPaste(Image image) {
            var minSize = new Size(1920, 1080);
            Size newSize;
            if (image.Width < minSize.Width || image.Height < minSize.Height) {
                var ratio = Math.Max((double)minSize.Width / image.Width, (double)minSize.Height / image.Height);
                newSize = new Size((int)(ratio * image.Width), (int)(ratio * image.Height));
            } else 
                newSize = image.Size;
            var newRect = new Rectangle(Point.Empty, newSize);
            var bitmap = new Bitmap(newSize.Width, newSize.Height, PixelFormat.Format24bppRgb);

            var graphics = Graphics.FromImage(bitmap);
            
            graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

            using (var wrapMode = new System.Drawing.Imaging.ImageAttributes()) {
                wrapMode.SetWrapMode(System.Drawing.Drawing2D.WrapMode.TileFlipXY);
                graphics.DrawImage(image, newRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
            }

            var data = bitmap.LockBits(newRect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            var nBytes = data.Stride * data.Height;
            var cvImage = new Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte>(newSize);
            unsafe {
                Buffer.MemoryCopy(data.Scan0.ToPointer(), cvImage.Mat.DataPointer.ToPointer(), nBytes, nBytes);
            }
            bitmap.UnlockBits(data);

            var sift = new Emgu.CV.Features2D.SIFT();
            Emgu.CV.Features2D.DescriptorMatcher matcher;
            var use_bf = true;
            if (use_bf)
                matcher = new Emgu.CV.Features2D.BFMatcher(Emgu.CV.Features2D.DistanceType.L2);
            else
                matcher = new Emgu.CV.Features2D.FlannBasedMatcher(new Emgu.CV.Flann.KdTreeIndexParams(5), new Emgu.CV.Flann.SearchParams());

            if (heroDescriptors == null) {
                heroDescriptors = new Dictionary<string, Emgu.CV.Mat>();
                foreach (var filename in Directory.GetFiles("portraits", "*.png")) {
                    var portrait = new Emgu.CV.Image<Emgu.CV.Structure.Bgra, byte>(filename);
                    var heroname = Path.GetFileNameWithoutExtension(filename);
                    var kp0 = new Emgu.CV.Util.VectorOfKeyPoint();
                    var des0 = new Emgu.CV.Mat();
                    sift.DetectAndCompute(portrait, null, kp0, des0, false);
                    heroDescriptors[heroname] = des0;
                }
            }

            var kp = new Emgu.CV.Util.VectorOfKeyPoint();
            var des = new Emgu.CV.Mat();
            sift.DetectAndCompute(cvImage, null, kp, des, false);

            var searchResults = new List<SearchResult>();
            foreach (var kvp in heroDescriptors) {
                var vMatches = new Emgu.CV.Util.VectorOfVectorOfDMatch();
                matcher.KnnMatch(kvp.Value, des, vMatches, 2);
                const float maxdist = 0.7f;
                var matches = vMatches.ToArrayOfArray().Where(m => m[0].Distance < maxdist * m[1].Distance).ToList();
                if (matches.Count > 0)
                    searchResults.Add(new SearchResult(kvp.Key, matches, kp));
            }
            searchResults.Sort((a, b) => -a.Distance.CompareTo(b.Distance));
            searchResults.RemoveAll(t => searchResults.Take(searchResults.IndexOf(t)).Select(u => u.HeroName).Contains(t.HeroName));
            var bans_picks = searchResults.Take(16).OrderBy(t => t.Location.Y).ToList();
            var bans = bans_picks.Take(6).OrderBy(t => t.Location.X).ToList();
            var picks = bans_picks.Skip(6).OrderBy(t => t.Location.X).ToList();
            var t1picks = picks.Take(5).OrderBy(t => t.Location.Y).ToList();
            var t2picks = picks.Skip(5).OrderBy(t => t.Location.Y).ToList();

            float radius = 0.025f * bitmap.Width;
            var font = new Font("Courier New", 30, FontStyle.Bold, GraphicsUnit.Pixel);
            Debug.WriteLine("================");
            foreach (var searchResult in searchResults) {
                Debug.WriteLine("{0:s} {1:d} {2:f6}", searchResult.HeroName, searchResult.MatchPoints.Count, searchResult.Distance);
                Color clr;
                if (bans.Contains(searchResult))
                    clr = Color.Green;
                else if (t1picks.Contains(searchResult))
                    clr = Color.Blue;
                else if (t2picks.Contains(searchResult))
                    clr = Color.Red;
                else
                    continue;
                PointF pt = searchResult.Location;
                //graphics.DrawEllipse(new Pen(clr, 2), pt.X - radius, pt.Y - radius, radius * 2, radius * 2);
                graphics.DrawLine(new Pen(clr, 1), pt.X, pt.Y - radius, pt.X, pt.Y);
                graphics.DrawRectangle(new Pen(clr, 2), Rectangle.Round(searchResult.Rect));
                foreach (var pt0 in searchResult.MatchPointsRaw)
                    graphics.DrawLine(new Pen(clr, 1), pt, pt0);
                graphics.DrawString(searchResult.HeroName, font, Brushes.White, pt);
            }

            graphics.Dispose();

            locked = true;
            try {
                c_t1b1.Text = bans[0].HeroName;
                c_t1b2.Text = bans[1].HeroName;
                c_t1b3.Text = bans[2].HeroName;
                c_t2b1.Text = bans[3].HeroName;
                c_t2b2.Text = bans[4].HeroName;
                c_t2b3.Text = bans[5].HeroName;
                c_t1h1.Text = t1picks[0].HeroName;
                c_t1h2.Text = t1picks[1].HeroName;
                c_t1h3.Text = t1picks[2].HeroName;
                c_t1h4.Text = t1picks[3].HeroName;
                c_t1h5.Text = t1picks[4].HeroName;
                c_t2h1.Text = t2picks[0].HeroName;
                c_t2h2.Text = t2picks[1].HeroName;
                c_t2h3.Text = t2picks[2].HeroName;
                c_t2h4.Text = t2picks[3].HeroName;
                c_t2h5.Text = t2picks[4].HeroName;
            } catch (ArgumentOutOfRangeException) { };
            locked = false;
            update();

            if (screenshotViewer == null)
                screenshotViewer = new ScreenshotViewer(this);
            screenshotViewer.SetImage(bitmap);
            screenshotViewer.Show();
            this.Activate();
        }

        void update() {
            string template = "|t1h1={0} |t1h2={1} |t1h3={2} |t1h4={3} |t1h5={4} |t1b1={5} |t1b2={6} |t1b3={7}\r\n" +
                              "|t2h1={8} |t2h2={9} |t2h3={10} |t2h4={11} |t2h5={12} |t2b1={13} |t2b2={14} |t2b3={15}\r\n";
            string winner = "";
            if (ch_t1w.Checked && !ch_t2w.Checked)
                winner = "1";
            else if (ch_t2w.Checked && !ch_t1w.Checked)
                winner = "2";
            string result = string.Format(template,
                                          c_t1h1.Text, c_t1h2.Text, c_t1h3.Text, c_t1h4.Text, c_t1h5.Text,
                                          c_t1b1.Text, c_t1b2.Text, c_t1b3.Text,
                                          c_t2h1.Text, c_t2h2.Text, c_t2h3.Text, c_t2h4.Text, c_t2h5.Text,
                                          c_t2b1.Text, c_t2b2.Text, c_t2b3.Text);
            if (c_bg.Text != "" || winner != "")
                result += string.Format("|battleground={0} |win={1}\r\n", c_bg.Text, winner);
            tResult.Text = result;
        }

        void swap(ComboBox a, ComboBox b) {
            string temp = a.Text;
            a.Text = b.Text;
            b.Text = temp;
        }

        void swap() {
            locked = true;
            swap(c_t1h1, c_t2h1);
            swap(c_t1h2, c_t2h2);
            swap(c_t1h3, c_t2h3);
            swap(c_t1h4, c_t2h4);
            swap(c_t1h5, c_t2h5);
            swap(c_t1b1, c_t2b1);
            swap(c_t1b2, c_t2b2);
            swap(c_t1b3, c_t2b3);
            if (ch_t1w.Checked != ch_t2w.Checked) {
                ch_t1w.Checked = !ch_t1w.Checked;
                ch_t2w.Checked = !ch_t2w.Checked;
            }
            locked = false;
            update();
        }

        void clear() {
            locked = true;
            foreach (ComboBox c in Controls.OfType<ComboBox>()) {
                c.Text = "";
            }
            ch_t1w.Checked = ch_t2w.Checked = false;
            locked = false;
            update();
            initialCBox.Focus();
        }

        void indicate(string message) {
            if (timer == null) {
                timer = new Timer();
                timer.Interval = 10;
                timer.Tick += Timer_Tick;
            }
            timer.Stop();
            message_cycles = message_duration = 4 * message.Length;
            l_indication.Text = message;
            l_indication.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
            l_indication.Visible = true;
            timer.Start();
        }

        void MainFormLoad(object sender, EventArgs e) {
            string herolistfile = Path.Combine(Application.StartupPath, "herolist");
			if (!File.Exists(herolistfile)) {
				herolistfile = Path.Combine(Application.StartupPath, "herolist.default");
			}
			string[] heroes = File.ReadAllLines(herolistfile, Encoding.UTF8).Select(s => {
            	s = (string)Regex.Replace(s, "#.*$", "");
            	return (string)Regex.Replace(s, "\\s", "");
            }).Where(s => s != "").ToArray();

			foreach (ComboBox c in Controls.OfType<ComboBox>().Where(c => c != c_bg)) {
				foreach (string heroname in heroes) {
					c.Items.Add(heroname);
				}
				c.TextChanged += cboxChange;
			}

			foreach (string bgname in new string[] {
			         "Alterac Pass", "Battlefield of Eternity", "Blackheart's Bay",
			         "Braxis Holdout", "Cursed Hollow", "Dragon Shire",
			         "Garden of Terror", "Hanamura Temple", "Haunted Mines",
			         "Infernal Shrines", "Sky Temple", "Tomb of the Spider Queen",
			         "Towers of Doom", "Volskaya Foundry", "Warhead Junction"
			         }) {
				c_bg.Items.Add(bgname);
			}
			c_bg.TextChanged += cboxChange;
			ch_t1w.CheckedChanged += cboxChange;
			ch_t2w.CheckedChanged += cboxChange;
			update();
		}

		void cboxChange(object sender, EventArgs e) {
			if (locked)
				return;

			update();
		}

        void BSwapClick(object sender, EventArgs e) {
            swap();
		}

		void BCopyClick(object sender, EventArgs e) {
			copy(true);
		}

        void BClearClick(object sender, EventArgs e) {
            clear();
		}

		void TResultGotFocus(object sender, EventArgs e) {
			tResult.SelectAll();
		}

		void RadioButtonCheckedChanged(object sender, EventArgs e) {
			RadioButton radioButton = (RadioButton)sender;
			if (!radioButton.Checked)
				return;
			foreach (KeyValuePair<ComboBox, int> kvp in indexDicts[sender]) {
				kvp.Key.TabIndex = 100 + kvp.Value;
			}
			initialCBox = initialCBoxes[sender];
			initialCBox.Focus();
		}

        void Timer_Tick(object sender, EventArgs e) {
            message_cycles--;
            if (message_cycles == 0) {
                l_indication.Visible = false;
                timer.Stop();
            } else {
                double multiplier = ((double)message_duration - message_cycles) / message_duration;
                Color fore_color = Color.FromKnownColor(KnownColor.ControlText);
                Color back_color = Color.FromKnownColor(KnownColor.Control);
                Color dest_color = Color.FromArgb(fore_color.R + (int)(multiplier * (back_color.R - fore_color.R)),
                                                  fore_color.G + (int)(multiplier * (back_color.G - fore_color.G)),
                                                  fore_color.B + (int)(multiplier * (back_color.B - fore_color.B)));
                l_indication.ForeColor = dest_color;
            }
        }
	}
}
