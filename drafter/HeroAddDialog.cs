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
    public partial class HeroAddDialog : Form {
        public HeroAddDialog() {
            InitializeComponent();
        }

        private void HeroAddDialog_Load(object sender, EventArgs e) {
            cbAddHero.Items.AddRange(HeroList.Instance.Heroes);
        }

        private void CbAddHero_KeyPress(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter)
                bOK.PerformClick();
        }

        public string HeroChoice { get => cbAddHero.Text; }
    }
}
