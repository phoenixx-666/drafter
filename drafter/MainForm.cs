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
using System.Windows.Forms;

namespace drafter
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		bool locked = false;
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
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
			Clipboard.SetText(tResult.Text);
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
