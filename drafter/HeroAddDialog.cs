using System;
using System.Windows.Forms;

namespace drafter {
    public partial class HeroAddDialog : Form {
        public HeroAddDialog() {
            InitializeComponent();
        }

        private void HeroAddDialog_Load(object sender, EventArgs e) {
            cbAddHero.Items.AddRange(HeroList.Heroes);
        }

        private void CbAddHero_KeyPress(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter)
                bOK.PerformClick();
        }

        public string HeroChoice { get => cbAddHero.Text; }
    }
}
