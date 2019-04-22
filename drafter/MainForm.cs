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
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace drafter
{
	public partial class MainForm : Form
	{
		bool locked = false;
		Control[] teamAPicks, teamBPicks;
		Dictionary<ComboBox, int> indicesSimple, indicesFPLeft, indicesFPRight;
		Dictionary<object, Dictionary<ComboBox, int>> indexDicts;
		Dictionary<object, ComboBox> initialCBoxes;
		ComboBox initialCBox;

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

		bool copy(bool force = false) {
			if (!force) {
				if (ActiveControl is ComboBox && ((ComboBox)ActiveControl).SelectedText != "")
					return false;
				else if (ActiveControl is TextBox && ((TextBox)ActiveControl).SelectedText != "")
					return false;
			}
			Clipboard.SetText(tResult.Text);
			return true;
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
			string herolistfile = Path.Combine(Application.StartupPath, "herolist");
			if (!File.Exists(herolistfile)) {
				herolistfile = Path.Combine(Application.StartupPath, "herolist.default");
			}
			string[] heroes = File.ReadAllLines(herolistfile, Encoding.UTF8).Select(s => {
            	s = (string)Regex.Replace(s, "#.*$", "");
            	return (string)Regex.Replace(s, "\\s", "");
            }).Where(s => s != "").ToArray();

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
			initialCBox.Focus();
		}

		void TResultGotFocus(object sender, EventArgs e)
		{
			tResult.SelectAll();
		}

		void RadioButtonCheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = sender as RadioButton;
			if (radioButton == null || !radioButton.Checked)
				return;
			foreach (KeyValuePair<ComboBox, int> kvp in indexDicts[sender]) {
				kvp.Key.TabIndex = 100 + kvp.Value;
			}
			initialCBox = initialCBoxes[sender];
			initialCBox.Focus();
		}
	}
}
