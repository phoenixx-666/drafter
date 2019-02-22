/*
 * Created by SharpDevelop.
 * User: nonexyst
 * Date: 10.02.2019
 * Time: 14:59
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace drafter
{
	public partial class MainForm : Form
	{
		bool locked = false;
		Control[] teamAPicks, teamBPicks;

		public MainForm()
		{
			InitializeComponent();

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
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
			switch (keyData) {
				case Keys.Control | Keys.Tab:
					jumpBetweenTeams();
					break;
				case Keys.Control | Keys.C:
					copy();
					break;
				case Keys.Control | Keys.Shift | Keys.C:
					copy(true);
					return true;
				case Keys.Control | Keys.Shift | Keys.V:
					paste();
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

		void copy(bool force = false) {
			if (!force) {
				if (ActiveControl is ComboBox && ((ComboBox)ActiveControl).SelectedText != "")
					return;
				else if (ActiveControl is TextBox && ((TextBox)ActiveControl).SelectedText != "")
					return;
			}
			Clipboard.SetText(tResult.Text);
		}

		void paste() {
			string text = Regex.Replace(Clipboard.GetText(), "\\s", "");
			if (text.Length == 0)
				return;

			Dictionary<string, string> dict = new Dictionary<string, string>();
			foreach (string chunk in text.Split('|')) {
               	string[] keyval = chunk.Split('=');
               	if (keyval.Length != 2)
               		continue;
               	string key = keyval[0], val = keyval[1];
               	if (Regex.Match(key, "t[12](b[1-3]|h[1-5])").Success)
               	    dict[key] = val; 
            };

			if (dict.Count == 0)
				return;

			locked = true;
			foreach (ComboBox c in Controls.OfType<ComboBox>()) {
				string key = c.Name.Substring(2);
				c.Text = dict.ContainsKey(key) ? dict[key] : "";
			}
			locked = false;
			update();
		}

		void MainFormLoad(object sender, EventArgs e)
		{
			string[] heroes = new[] {
				"abathur",
				"alarak",
				"alexstrasza",
				"ana",
				"anubarak",
				"artanis",
				"arthas",
				"auriel",
				"azmodan",
				"blaze",
				"brightwing",
				"butcher",
				"cassia",
				"chen",
				"cho",
				"chromie",
				"deckard",
				"dehaka",
				"diablo",
				"dva",
				"etc",
				"falstad",
				"fenix",
				"gall",
				"garrosh",
				"gazlowe",
				"genji",
				"greymane",
				"guldan",
				"hanzo",
				"illidan",
				"imperius",
				"jaina",
				"johanna",
				"junkrat",
				"kaelthas",
				"kelthuzad",
				"kerrigan",
				"kharazim",
				"leoric",
				"lili",
				"liming",
				"lucio",
				"lunara",
				"maiev",
				"malfurion",
				"malganis",
				"malthael",
				"medivh",
				"mephisto",
				"morales",
				"muradin",
				"murky",
				"nazeebo",
				"nova",
				"orphea",
				"probius",
				"ragnaros",
				"raynor",
				"rehgar",
				"rexxar",
				"samuro",
				"sgt",
				"sonya",
				"stitches",
				"stukov",
				"sylvanas",
				"tassadar",
				"thrall",
				"tlv",
				"tracer",
				"tychus",
				"tyrael",
				"tyrande",
				"uther",
				"valeera",
				"valla",
				"varian",
				"whitemane",
				"xul",
				"yrel",
				"zagara",
				"zarya",
				"zeratul",
				"zuljin",
			};

			foreach (ComboBox c in Controls.OfType<ComboBox>()) {
				foreach (string heroname in heroes) {
					c.Items.Add(heroname);
				}
				c.TextChanged += cboxChange;
			}
			update();
		}

		void cboxChange(object sender, EventArgs e) {
			if (locked)
				return;

			update();
		}

		void update() {

			string template = "|t1h1={0} |t1h2={1} |t1h3={2} |t1h4={3} |t1h5={4} |t1b1={5} |t1b2={6} |t1b3={7}\r\n" +
				"|t2h1={8} |t2h2={9} |t2h3={10} |t2h4={11} |t2h5={12} |t2b1={13} |t2b2={14} |t2b3={15}\r\n";
			tResult.Text = string.Format(template,
			                             c_t1h1.Text, c_t1h2.Text, c_t1h3.Text, c_t1h4.Text, c_t1h5.Text,
			                             c_t1b1.Text, c_t1b2.Text, c_t1b3.Text,
			                             c_t2h1.Text, c_t2h2.Text, c_t2h3.Text, c_t2h4.Text, c_t2h5.Text,
			                             c_t2b1.Text, c_t2b2.Text, c_t2b3.Text);
		}

		void swap(ComboBox a, ComboBox b) {
			string temp = a.Text;
			a.Text = b.Text;
			b.Text = temp;
		}

		void BSwapClick(object sender, EventArgs e)
		{
			locked = true;
			swap(c_t1h1, c_t2h1);
			swap(c_t1h2, c_t2h2);
			swap(c_t1h3, c_t2h3);
			swap(c_t1h4, c_t2h4);
			swap(c_t1h5, c_t2h5);
			swap(c_t1b1, c_t2b1);
			swap(c_t1b2, c_t2b2);
			swap(c_t1b3, c_t2b3);
			locked = false;
			update();
		}

		void BCopyClick(object sender, EventArgs e)
		{
			copy(true);
		}

		void BClearClick(object sender, EventArgs e)
		{
			locked = true;
			foreach (ComboBox c in Controls.OfType<ComboBox>()) {
				c.Text = "";
			}
			locked = false;
			update();
			c_t1b1.Focus();
		}

		void TResultGotFocus(object sender, EventArgs e)
		{
			tResult.SelectAll();
		}
	}
}
