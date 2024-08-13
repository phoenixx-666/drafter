using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Emgu.CV;
using Microsoft.WindowsAPICodePack.Taskbar;

using Image = System.Drawing.Image;
using Message = System.Windows.Forms.Message;

namespace drafter {
    public partial class MainForm : Form {
        bool locked = false;
        readonly Control[] teamAPicks, teamBPicks;
        readonly Dictionary<ComboBox, int> indicesSimple, indicesFPLeft, indicesFPRight;
        readonly Dictionary<object, Dictionary<ComboBox, int>> indexDicts;
        readonly Dictionary<object, ComboBox> initialCBoxes;
        ComboBox initialCBox;
        ScreenshotViewer screenshotViewer;
        Regex cleanRe = null, commentRe = null, parserRe = null;
        Timer timer = null;
        int message_cycles;
        int message_duration;
        Dictionary<string, Emgu.CV.Mat> heroDescriptors;
        Dictionary<string, Emgu.CV.Mat> bgnameDescriptors;
        Emgu.CV.Features2D.SIFT sift;
        Emgu.CV.Features2D.DescriptorMatcher matcher;

        public MainForm() {
            InitializeComponent();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            this.Icon = new Icon(assembly.GetManifestResourceStream("drafter.drafter.ico"));
#if DEBUG
            this.Text = "drafter [Debug]";
#endif

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

        public void InvokeProcessCmdKey(ref Message msg, Keys keyData) {
            Activate();
            ProcessCmdKey(ref msg, keyData);
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

            if (commentRe == null)
                commentRe = new Regex("<!\\-\\-((?!\\-\\->).)*\\-\\->");

            text = commentRe.Replace(text, "");
            if (text.Length == 0)
                return;

            if (parserRe == null)
                parserRe = new Regex("^(t[12](b[1-3]|h[1-5])|map|winner|team[12]side)$");

            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (string chunk in text.Split('|')) {
                string[] keyval = chunk.Split('=');
                if (keyval.Length != 2)
                    continue;
                string key = keyval[0].Trim(), val = keyval[1].Trim();
                if (parserRe.Match(key).Success)
                    dict[key] = val;
            };

            if (!dict.Any()) {
                MessageBox.Show(
                    "The clipboard does not contain any related data.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            locked = true;
            foreach (ComboBox c in Controls.OfType<ComboBox>().Where(c => c != c_bg)) {
                string key = c.Name.Substring(2);
                if (dict.ContainsKey(key))
                    c.Text = dict[key];
            }
            if (dict.ContainsKey("map"))
                c_bg.Text = dict["map"];
            if (dict.ContainsKey("winner")) {
                string winner = dict["winner"];
                ch_t1w.Checked = winner == "1";
                ch_t2w.Checked = winner == "2";
            }
            if (dict.ContainsKey("team1side")) {
                if (dict["team1side"] == "blue")
                    resetColors();
                else
                    reverseColors();
            } else if (dict.ContainsKey("team2side")) {
                if (dict["team2side"] == "red")
                    resetColors();
                else
                    reverseColors();
            }
            locked = false;
            update();
        }

        void tryPaste(Image image) {
            if (worker.IsBusy) {
                MessageBox.Show(
                    "The program is already processing an image parsing task. " +
                    "In order to initiate a new task, you must wait until the current one finishes.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            worker.RunWorkerAsync(image);
        }

        void update() {
            const string template = "\t\t|team1side={16} |team2side={17} |winner={18}\r\n" +
                                    "\t\t|vod= |length=\r\n" +
                                    "\t\t<!-- Hero picks -->\r\n" +
                                    "\t\t|t1h1={0} |t1h2={1} |t1h3={2} |t1h4={3} |t1h5={4}\r\n" +
                                    "\t\t|t2h1={8} |t2h2={9} |t2h3={10} |t2h4={11} |t2h5={12}\r\n" +
                                    "\t\t<!-- Hero bans -->\r\n" +
                                    "\t\t|t1b1={5} |t1b2={6} |t1b3={7}\r\n" +
                                    "\t\t|t2b1={13} |t2b2={14} |t2b3={15}";
            string winner = "";
            if (ch_t1w.Checked && !ch_t2w.Checked)
                winner = "1";
            else if (ch_t2w.Checked && !ch_t1w.Checked)
                winner = "2";
            string result = string.Format(template,
                                          c_t1h1.Text, c_t1h2.Text, c_t1h3.Text, c_t1h4.Text, c_t1h5.Text,
                                          c_t1b1.Text, c_t1b2.Text, c_t1b3.Text,
                                          c_t2h1.Text, c_t2h2.Text, c_t2h3.Text, c_t2h4.Text, c_t2h5.Text,
                                          c_t2b1.Text, c_t2b2.Text, c_t2b3.Text,
                                          t_s1.Text, t_s2.Text, winner);
            if (c_bg.Text != "")
                result = string.Format("map={0}\r\n", c_bg.Text) + result;
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
            swapColors();
            locked = false;
            update();
        }

        void swapColors() {
            if (t_s1.Text == "blue") {
                reverseColors();
            } else {
                resetColors();
            }
        }

        void reverseColors() {
            t_s1.Text = "red";
            t_s1.ForeColor = Color.Red;
            t_s2.Text = "blue";
            t_s2.ForeColor = Color.Blue;

            if (!locked)
                update();
        }

        void resetColors() {
            t_s1.Text = "blue";
            t_s1.ForeColor = Color.Blue;
            t_s2.Text = "red";
            t_s2.ForeColor = Color.Red;

            if (!locked)
                update();
        }

        void clear() {
            locked = true;
            foreach (ComboBox c in Controls.OfType<ComboBox>()) {
                c.Text = "";
            }
            ch_t1w.Checked = ch_t2w.Checked = false;
            resetColors();
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

        static Dictionary<string, Emgu.CV.Mat> loadDescriptors(string archivename) {
            var result = new Dictionary<string, Emgu.CV.Mat>();
            using (var fstream = new FileStream(archivename, FileMode.Open))
            using (var archive = new ZipArchive(fstream, ZipArchiveMode.Read)) {
                foreach (var file in archive.Entries) {
                    using (var stream = file.Open())
                    using (var reader = new BinaryReader(stream)) {
                        var bytes = reader.ReadBytes((int)file.Length);
                        var des = new Emgu.CV.Mat(bytes.Length / 4 / 128, 128, Emgu.CV.CvEnum.DepthType.Cv32F, 1);
                        unsafe {
                            fixed (byte* ptr = bytes) {
                                Buffer.MemoryCopy(ptr, des.DataPointer.ToPointer(), bytes.Length, bytes.Length);
                            }
                        }
                        result[file.Name] = des;
                    }
                }
            }
            return result;
        }

        private void Worker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e) {
            var image = (Image)e.Argument;
            var minSize = new Size(3840, 2160);
            Size newSize;
            if (image.Width < minSize.Width || image.Height < minSize.Height) {
                var ratio = Math.Max((double)minSize.Width / image.Width, (double)minSize.Height / image.Height);
                newSize = new Size((int)(ratio * image.Width), (int)(ratio * image.Height));
            } else
                newSize = image.Size;
            var rectSide = Math.Min(newSize.Width, newSize.Height) / 15.0f;
            var newRect = new Rectangle(Point.Empty, newSize);
            Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> cvImage;
            using (var bitmap = new Bitmap(newSize.Width, newSize.Height, PixelFormat.Format24bppRgb)) {

                using (var graphics = Graphics.FromImage(bitmap)) {
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    using (var wrapMode = new ImageAttributes()) {
                        wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                        graphics.DrawImage(image, newRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                    }
                }

                Invoke(new Action(() => {
                    if (screenshotViewer == null)
                        screenshotViewer = new ScreenshotViewer(this) { Top = Top, Left = Right };
                    screenshotViewer.SetImage(new Bitmap(bitmap));
                    screenshotViewer.Show();
                }));

                var data = bitmap.LockBits(newRect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                var nBytes = data.Stride * data.Height;
                cvImage = new Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte>(newSize);
                unsafe {
                    Buffer.MemoryCopy(data.Scan0.ToPointer(), cvImage.Mat.DataPointer.ToPointer(), nBytes, nBytes);
                }
                bitmap.UnlockBits(data);
            }

            if (sift == null)
                sift = new Emgu.CV.Features2D.SIFT(edgeThreshold: 25, sigma: 1.2);
            if (matcher == null) {
                var use_bf = true;
                if (use_bf)
                    matcher = new Emgu.CV.Features2D.BFMatcher(Emgu.CV.Features2D.DistanceType.L2);
                else
                    matcher = new Emgu.CV.Features2D.FlannBasedMatcher(new Emgu.CV.Flann.KdTreeIndexParams(5), new Emgu.CV.Flann.SearchParams());
            }

            if (heroDescriptors == null) {
                Invoke(new Action(() => {
                    screenshotViewer.SetProgress(Stage.LoadingData);
                }));
                heroDescriptors = loadDescriptors("portraits.zip");
                bgnameDescriptors = loadDescriptors("bgnames.zip");
            }

            int nTotal = heroDescriptors.Values.Concat(bgnameDescriptors.Values).Select(desc => desc.Rows).Sum();
            int nCurrent = 0;
            using (var kp = new Emgu.CV.Util.VectorOfKeyPoint())
            using (var des = new Emgu.CV.Mat()) {
                Invoke(new Action(() => {
                    screenshotViewer.SetProgress(Stage.ProcessingImage);
                }));
                sift.DetectAndCompute(cvImage, null, kp, des, false);
                cvImage.Dispose();

                Invoke(new Action(() => {
                    screenshotViewer.SetProgress(0.0);
                }));

                var searchResults = new List<SearchResult>();
                var bgSearchResults = new List<SearchResult>();

                foreach (var kvp in heroDescriptors) {
                    using (var vMatches = new Emgu.CV.Util.VectorOfVectorOfDMatch()) {
                        matcher.KnnMatch(kvp.Value, des, vMatches, 2);
                        const float maxdist = 0.7f;
                        var matches = vMatches.ToArrayOfArray().Where(m => m[0].Distance < maxdist * m[1].Distance).ToList();
                        if (matches.Any())
                            searchResults.Add(new SearchResult(kvp.Key, matches, kp, rectSide));
                    }
                    nCurrent += kvp.Value.Rows;
                    Invoke(new Action(() => {
                        screenshotViewer.SetProgress((double)nCurrent / nTotal);
                        TaskbarManager.Instance.SetProgressValue(nCurrent * 1000 / nTotal, 1000, this.Handle);
                    }));
                }
                if ((bool)Invoke(new Func<object>(() => (object)screenshotViewer.SearchForBattleground))) {
                    foreach (var kvp in bgnameDescriptors) {
                        using (var vMatches = new Emgu.CV.Util.VectorOfVectorOfDMatch()) {
                            matcher.KnnMatch(kvp.Value, des, vMatches, 2);
                            const float maxdist = 0.7f;
                            var matches = vMatches.ToArrayOfArray().Where(m => m[0].Distance < maxdist * m[1].Distance).ToList();
                            if (matches.Any()) {
                                bgSearchResults.Add(new SearchResult(kvp.Key, matches, kp));
                            }
                        }
                        nCurrent += kvp.Value.Rows;
                        Invoke(new Action(() => {
                            screenshotViewer.SetProgress((double)nCurrent / nTotal);
                            TaskbarManager.Instance.SetProgressValue(nCurrent * 1000 / nTotal, 1000, this.Handle);
                        }));
                    }
                }

                searchResults.Sort((a, b) => -a.Distance.CompareTo(b.Distance));
                searchResults.RemoveAll(t => searchResults.Take(searchResults.IndexOf(t)).Select(u => u.Name).Contains(t.Name));
                var bans_picks = searchResults.Take(16).OrderBy(t => t.Location.Y).ToList();
                var bans = bans_picks.Take(6).OrderBy(t => t.Location.X).ToList();
                var picks = bans_picks.Skip(6).OrderBy(t => t.Location.X).ToList();
                var t1picks = picks.Take(5).OrderBy(t => t.Location.Y).ToList();
                var t2picks = picks.Skip(5).OrderBy(t => t.Location.Y).ToList();

                var bgSearchResult = bgSearchResults.OrderBy(t => -t.Distance).FirstOrDefault();
                Invoke(new Action(() => {
                    screenshotViewer.SetProgress(Stage.Complete);
                    TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress, this.Handle);
                    screenshotViewer.SetSearchResults(bans_picks.ToArray(), bgSearchResult);
                    if (bgSearchResult != null)
                        c_bg.Text = bgSearchResult.Name;
                    screenshotViewer.Show();
                    Focus();
                }));
            }
        }

        private void Worker_Completed(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e) {
            if (e.Error == null)
                return;

            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Error, this.Handle);

            IWin32Window owner = screenshotViewer != null ? (IWin32Window)screenshotViewer : this;
            MessageBox.Show(owner, "There was an error processing image.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            if (screenshotViewer != null)
                screenshotViewer.SetProgress(Stage.Complete);
            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress, this.Handle);
        }

        public void SetBansPicks(string[] bans, string[] t1picks, string[] t2picks) {
            locked = true;
            c_t1b1.Text = bans.ElementAtOrDefault(0);
            c_t1b2.Text = bans.ElementAtOrDefault(1);
            c_t1b3.Text = bans.ElementAtOrDefault(2);
            c_t2b1.Text = bans.ElementAtOrDefault(3);
            c_t2b2.Text = bans.ElementAtOrDefault(4);
            c_t2b3.Text = bans.ElementAtOrDefault(5);
            c_t1h1.Text = t1picks.ElementAtOrDefault(0);
            c_t1h2.Text = t1picks.ElementAtOrDefault(1);
            c_t1h3.Text = t1picks.ElementAtOrDefault(2);
            c_t1h4.Text = t1picks.ElementAtOrDefault(3);
            c_t1h5.Text = t1picks.ElementAtOrDefault(4);
            c_t2h1.Text = t2picks.ElementAtOrDefault(0);
            c_t2h2.Text = t2picks.ElementAtOrDefault(1);
            c_t2h3.Text = t2picks.ElementAtOrDefault(2);
            c_t2h4.Text = t2picks.ElementAtOrDefault(3);
            c_t2h5.Text = t2picks.ElementAtOrDefault(4);
            resetColors();
            locked = false;
            update();
        }

        void MainFormLoad(object sender, EventArgs e) {

            Controls.OfType<ComboBox>().Where(c => c != c_bg).ToList().ForEach(c => {
                c.Items.AddRange(HeroList.Heroes);
                c.TextChanged += cboxChange;
            });

            c_bg.Items.AddRange(new string[] {
                "Alterac Pass", "Battlefield of Eternity", "Blackheart's Bay",
                "Braxis Holdout", "Cursed Hollow", "Dragon Shire",
                "Garden of Terror", "Hanamura Temple", "Haunted Mines",
                "Infernal Shrines", "Sky Temple", "Tomb of the Spider Queen",
                "Towers of Doom", "Volskaya Foundry", "Warhead Junction"
            });

            c_bg.TextChanged += cboxChange;
			ch_t1w.CheckedChanged += checkboxChange;
			ch_t2w.CheckedChanged += checkboxChange;
			update();
		}

		void cboxChange(object sender, EventArgs e) {
			if (locked)
				return;

			update();
		}

        void checkboxChange(object sender, EventArgs e) {
            if (locked)
                return;

            var chbox = sender as CheckBox;
            if (chbox != null && chbox.Checked) {
                locked = true;
                if (chbox == ch_t1w)
                    ch_t2w.Checked = false;
                else if (chbox == ch_t2w)
                    ch_t1w.Checked = false;
                locked = false;
            }

            update();
        }

        void BSwapClick(object sender, EventArgs e) {
            swap();
		}

		void BCopyClick(object sender, EventArgs e) {
			copy(true);
		}

        void BSwapColorsCluck(object sender, EventArgs e) {
            swapColors();
        }

        void BPaste_Click(object sender, EventArgs e) {
            paste();
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
