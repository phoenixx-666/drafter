using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace drafter {
    public enum Stage {
        LoadingData, ProcessingImage, Complete
    }

    public partial class ScreenshotViewer : Form {
        const int maxSearchResults = 16;

        readonly MainForm mainForm;
        Image image;
        SearchResult[] searchResults = new SearchResult[0];
        SearchResult[] bans = new SearchResult[0];
        SearchResult[] t1picks = new SearchResult[0];
        SearchResult[] t2picks = new SearchResult[0];
        SearchResult[] selection = new SearchResult[0];
        SearchResult bg = null;
        bool debug = false;

        public ScreenshotViewer(MainForm mainForm) {
            ResizeRedraw = true;
            this.mainForm = mainForm;
            InitializeComponent();
        }

        public bool SearchForBattleground => menuSearchBG.Checked;

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            switch (keyData) {
                case Keys.F4:
                    debug = !debug;
                    Invalidate();
                    return true;
                case Keys.Delete:
                    if (selection.Any()) {
                        SetSearchResults(searchResults.Where(res => !selection.Contains(res)).ToArray(), bg, false);
                    } else
                        MessageBox.Show("No search result has been selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return true;
                case Keys.Control | Keys.Shift | Keys.C:
                case Keys.Control | Keys.S:
                case Keys.Control | Keys.Shift | Keys.V:
                case Keys.Control | Keys.R:
                case Keys.Control | Keys.W:
                case Keys.Control | Keys.Space:
                    mainForm.InvokeProcessCmdKey(ref msg, keyData);
                    return true;
                case Keys.Control | Keys.C:
                    mainForm.InvokeProcessCmdKey(ref msg, Keys.Control | Keys.S);
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void ScreenshotViewer_Closing(object sender, FormClosingEventArgs e) {
            e.Cancel = true;
            Hide();
        }

        void UpdateBansPicks() {
            bans = searchResults.Take(6).OrderBy(t => t.Location.X).ToArray();
            var picks = searchResults.Skip(6).OrderBy(t => t.Location.X).ToArray();
            t1picks = picks.Take(5).OrderBy(t => t.Location.Y).ToArray();
            t2picks = picks.Skip(5).OrderBy(t => t.Location.Y).ToArray();
            Invalidate();
        }

        void UpdateMenu() {
            menuSelectHero.DropDownItems.Clear();
            int index = 1;
            foreach (var result in searchResults.OrderBy(result => result.Name)) {
                string text = string.Format("{0:d}: {1:s}", index, result.Name == string.Empty ? "<wasted ban>" : result.Name);
                var item = menuSelectHero.DropDownItems.Add(text);
                item.Click += new EventHandler((object sender, EventArgs e) => {
                    selection = new SearchResult[] { result };
                    Invalidate();
                });
                index++;
            }
            menuSelectHero.Enabled = searchResults.Any();
        }

        public void SetImage(Image image) {
            Text = "Screenshot Viewer";
            if (this.image != null)
                this.image.Dispose();
            this.image = image;
            searchResults = new SearchResult[0];
            selection = new SearchResult[0];
            bg = null;

            UpdateBansPicks();
            UpdateMenu();
            Activate();
        }

        public void SetProgress(Stage stage) {
            switch (stage) {
                case Stage.LoadingData:
                    prog.Style = ProgressBarStyle.Marquee;
                    prog.Show();
                    Text = "Screenshot Viewer [Loading data...]";
                    break;
                case Stage.ProcessingImage:
                    prog.Style = ProgressBarStyle.Marquee;
                    prog.Show();
                    Text = "Screenshot Viewer [Processing image...]";
                    break;
                case Stage.Complete:
                    prog.Hide();
                    Text = "Screenshot Viewer";
                    break;
            }
        }

        public void SetProgress(double progress) {
            prog.Style = ProgressBarStyle.Continuous;
            prog.Value = (int)(progress * prog.Maximum);
            Text = string.Format("Screenshot Viewer [Processing image ({0:d}%)...]", (int)(progress * 100));
        }

        public void SetSearchResults(SearchResult[] searchResults, SearchResult bg, bool activateMainForm = true) {
            this.searchResults = searchResults;
            selection = new SearchResult[0];
            this.bg = bg;
            UpdateBansPicks();
            UpdateMenu();
            mainForm.SetBansPicks(
                bans.Select(t => t.Name).ToArray(),
                t1picks.Select(t => t.Name).ToArray(),
                t2picks.Select(t => t.Name).ToArray());
            if (activateMainForm)
                mainForm.Activate();
        }

        private void ScreenshotViewer_Load(object sender, EventArgs e) {
            ScreenshotViewer_Resize(sender, e);
        }

        private void ScreenshotViewer_Resize(object sender, EventArgs e) {
            prog.Left = 0;
            prog.Top = ClientRectangle.Height - prog.Height;
            prog.Width = ClientRectangle.Width;
        }

        private void ScreenshotViewer_MouseClick(object sender, MouseEventArgs e) {
            var ratio = Math.Min((float)ClientRectangle.Width / image.Width, (float)(ClientRectangle.Height - menu.Height) / image.Height);
            var visibleSize = new SizeF(image.Width * ratio, image.Height * ratio);
            var offset = new PointF(ClientRectangle.Width / 2 - visibleSize.Width / 2, (ClientRectangle.Height - menu.Height) / 2 - visibleSize.Height / 2);

            var location = new PointF(e.X / ratio - offset.X / ratio, (e.Y - menu.Height) / ratio - offset.Y / ratio);
            if (location.X < 0 || location.X >= image.Width || location.Y < 0 || location.Y >= image.Height)
                return;
            if (e.Button == MouseButtons.Left) {
                var newSelection = searchResults.Where(res => res.Rect.Contains(location)).ToArray();
                if (newSelection != selection) {
                    selection = newSelection;
                    Invalidate();
                }
            } else if (e.Button == MouseButtons.Right) {
                if (searchResults.Length >= maxSearchResults) {
                    MessageBox.Show("Cannot add more custom search results", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                using (var dlg = new HeroAddDialog()) {
                    if (dlg.ShowDialog() == DialogResult.OK) {
                        var rectSide = Math.Min(image.Width, image.Height) / 30.0f;
                        var rect = RectangleF.FromLTRB(
                            location.X - rectSide, location.Y - rectSide,
                            location.X + rectSide, location.Y + rectSide);
                        var result = new SearchResult(dlg.HeroChoice, rect, location);
                        SetSearchResults(searchResults.Append(result).OrderBy(t => t.Location.Y).ToArray(), bg, false);
                    }
                }
            }
        }

        private void ScreenshotViewer_Paint(object sender, PaintEventArgs e) {
            var image = (Image)this.image.Clone();
            var ratio = Math.Min((float)e.ClipRectangle.Width / image.Width, (float)e.ClipRectangle.Height / image.Height);
            var dstSize = new SizeF(image.Width * ratio, image.Height * ratio);
            var offset = new PointF(e.ClipRectangle.Width / 2 - dstSize.Width / 2, menu.Height + e.ClipRectangle.Height / 2 - dstSize.Height / 2);

            using (var g = Graphics.FromImage(image)) {
                float radius = 0.025f * image.Width;
                var font = new Font("Courier New", 30, FontStyle.Bold, GraphicsUnit.Pixel);
                if (debug)
                    g.DrawString("debug mode on", font, Brushes.LightYellow, Point.Empty);

                foreach (var searchResult in bg == null ? searchResults : searchResults.Append(bg)) {
                    Color clr;
                    if (selection.Contains(searchResult))
                        clr = Color.White;
                    else if (bans.Contains(searchResult))
                        clr = Color.Green;
                    else if (t1picks.Contains(searchResult))
                        clr = Color.Blue;
                    else if (t2picks.Contains(searchResult))
                        clr = Color.Red;
                    else if (searchResult == bg)
                        clr = Color.Purple;
                    else
                        continue;
                    PointF pt = searchResult.Location;
                    g.FillRectangle(new SolidBrush(Color.FromArgb(127, clr)), searchResult.Rect);
                    g.DrawRectangle(new Pen(clr, 4), Rectangle.Round(searchResult.Rect));
                    var text = searchResult.Name;
                    if (debug) {
                        if (searchResult.MatchPointsRaw != null) {
                            g.DrawLine(new Pen(clr, 2), pt.X, searchResult.Rect.Top, pt.X, pt.Y);
                            searchResult.MatchPointsRaw.ForEach(pt0 => g.DrawLine(new Pen(clr, 2), pt, pt0));
                            text += string.Format("\npts: {0:d}", searchResult.MatchPointsRaw.Count);
                        }
                    }
                    g.DrawString(text, font, Brushes.White, pt);
                }
            }

            var graphics = e.Graphics;
            graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

            graphics.DrawImage(image, new RectangleF(offset, dstSize));
        }
    }
}
