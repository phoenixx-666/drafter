using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace drafter {
    public partial class ScreenshotViewer : Form {
        MainForm mainForm;

        public ScreenshotViewer(MainForm mainForm) {
            this.mainForm = mainForm;
            InitializeComponent();
        }

        public void ScreenshotViewer_Closing(object sender, FormClosingEventArgs e) {
            e.Cancel = true;
            Hide();
        }

        public void SetImage(Image image) {
            imageViewer.Image = image;
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
    }
}
