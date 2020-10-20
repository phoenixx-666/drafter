using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace drafter {
    public partial class ScreenshotViewer : Form {
        MainForm mainForm;
        Image image;
        SearchResult[] searchResults = new SearchResult[0];
        SearchResult[] bans = new SearchResult[0];
        SearchResult[] t1picks = new SearchResult[0];
        SearchResult[] t2picks = new SearchResult[0];
        SearchResult[] selection = new SearchResult[0];
        bool debug = false;

        public ScreenshotViewer(MainForm mainForm) {
            this.ResizeRedraw = true;
            this.mainForm = mainForm;
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            switch (keyData) {
                case Keys.F4:
                    debug = !debug;
                    if (searchResults.Any())
                        Invalidate();
                    break;
                case Keys.Delete:
                    if (selection.Any()) {
                        SetSearchResults(searchResults.Where(res => !selection.Contains(res)).ToArray(), false);
                    } else
                        MessageBox.Show("No search result has been selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
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

        public void SetImage(Image image) {
            this.image = image;
            searchResults = new SearchResult[0];
            selection = new SearchResult[0];
            UpdateBansPicks();
        }

        public void SetSearchResults(SearchResult[] searchResults, bool activateMainForm = true) {
            this.searchResults = searchResults;
            selection = new SearchResult[0];
            UpdateBansPicks();
            mainForm.SetBansPicks(
                bans.Select(t => t.HeroName).ToArray(),
                t1picks.Select(t => t.HeroName).ToArray(),
                t2picks.Select(t => t.HeroName).ToArray());
            if (activateMainForm)
                mainForm.Activate();
        }

        private void ScreenshotViewer_Load(object sender, EventArgs e) {

        }

        private void ScreenshotViewer_MouseClick(object sender, MouseEventArgs e) {
            var ratio = Math.Min((float)ClientRectangle.Width / image.Width, (float)ClientRectangle.Height / image.Height);
            var visibleSize = new SizeF(image.Width * ratio, image.Height * ratio);
            var offset = new PointF(ClientRectangle.Width / 2 - visibleSize.Width / 2, ClientRectangle.Height / 2 - visibleSize.Height / 2);

            var location = new PointF(e.X / ratio - offset.X / ratio, e.Y / ratio - offset.Y / ratio);
            if (location.X < 0 || location.X >= image.Width || location.Y < 0 || location.Y >= image.Height)
                return;
            if (e.Button == MouseButtons.Left) {
                var newSelection = searchResults.Where(res => res.Rect.Contains(location)).ToArray();
                if (newSelection != selection) {
                    selection = newSelection;
                    Invalidate();
                }
            } else if (e.Button == MouseButtons.Right) {
                if (searchResults.Length >= 16) {
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
                        SetSearchResults(searchResults.Append(result).OrderBy(t => t.Location.Y).ToArray(), false);
                    }
                }
            }
        }

        private void ScreenshotViewer_Paint(object sender, PaintEventArgs e) {
            var image = (Image)this.image.Clone();
            var ratio = Math.Min((float)e.ClipRectangle.Width / image.Width, (float)e.ClipRectangle.Height / image.Height);
            var dstSize = new SizeF(image.Width * ratio, image.Height * ratio);
            var offset = new PointF(e.ClipRectangle.Width / 2 - dstSize.Width / 2, e.ClipRectangle.Height / 2 - dstSize.Height / 2);

            using (var g = Graphics.FromImage(image)) {
                float radius = 0.025f * image.Width;
                var font = new Font("Courier New", 30, FontStyle.Bold, GraphicsUnit.Pixel);

                //Debug.WriteLine("================");
                foreach (var searchResult in searchResults) {
                    //Debug.WriteLine("{0:s} {1:d} {2:f6}", searchResult.HeroName, searchResult.MatchPoints.Count, searchResult.Distance);
                    Color clr;
                    if (selection.Contains(searchResult))
                        clr = Color.White;
                    else if (bans.Contains(searchResult))
                        clr = Color.Green;
                    else if (t1picks.Contains(searchResult))
                        clr = Color.Blue;
                    else if (t2picks.Contains(searchResult))
                        clr = Color.Red;
                    else
                        continue;
                    PointF pt = searchResult.Location;
                    //g.DrawEllipse(new Pen(clr, 2), pt.X - radius, pt.Y - radius, radius * 2, radius * 2);
                    g.DrawRectangle(new Pen(clr, 4), Rectangle.Round(searchResult.Rect));
                    if (debug) {
                        g.DrawLine(new Pen(clr, 2), pt.X, searchResult.Rect.Top, pt.X, pt.Y);
                        if (searchResult.MatchPointsRaw != null)
                            searchResult.MatchPointsRaw.ForEach(pt0 => g.DrawLine(new Pen(clr, 2), pt, pt0));
                    }
                    g.DrawString(searchResult.HeroName, font, Brushes.White, pt);
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
