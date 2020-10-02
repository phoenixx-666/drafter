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
                    if (searchResults.Length > 0)
                        Invalidate();
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
        }

        public void SetImage(Image image) {
            this.image = image;
            searchResults = new SearchResult[0];
            UpdateBansPicks();
            Invalidate();
        }

        public void SetSearchResults(SearchResult[] searchResults) {
            this.searchResults = searchResults;
            UpdateBansPicks();
            mainForm.SetBansPicks(
                bans.Select(t => t.HeroName).ToArray(),
                t1picks.Select(t => t.HeroName).ToArray(),
                t2picks.Select(t => t.HeroName).ToArray());
            Invalidate();
        }

        protected override bool ShowWithoutActivation => true;

        protected override CreateParams CreateParams {
            get {
                var parms = base.CreateParams;
                parms.ExStyle |= 0x08000000;
                return parms;
            }
        }

        private void ScreenshotViewer_Load(object sender, EventArgs e) {

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
                    if (bans.Contains(searchResult))
                        clr = Color.Green;
                    else if (t1picks.Contains(searchResult))
                        clr = Color.Blue;
                    else if (t2picks.Contains(searchResult))
                        clr = Color.Red;
                    else
                        continue;
                    PointF pt = searchResult.Location;
                    //g.DrawEllipse(new Pen(clr, 2), pt.X - radius, pt.Y - radius, radius * 2, radius * 2);
                    g.DrawRectangle(new Pen(clr, 2), Rectangle.Round(searchResult.Rect));
                    if (debug) {
                        g.DrawLine(new Pen(clr, 1), pt.X, searchResult.Rect.Top, pt.X, pt.Y);
                        foreach (var pt0 in searchResult.MatchPointsRaw)
                            g.DrawLine(new Pen(clr, 1), pt, pt0);
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
