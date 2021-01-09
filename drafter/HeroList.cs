using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace drafter {
    class HeroList {
        private static HeroList instance;
        private readonly string[] heroes;

        private HeroList() {
            string herolistfile = Path.Combine(Application.StartupPath, "herolist");
            if (!File.Exists(herolistfile)) {
                herolistfile = Path.Combine(Application.StartupPath, "herolist.default");
            }
            heroes = File.ReadAllLines(herolistfile, Encoding.UTF8).Select(s => {
                s = (string)Regex.Replace(s, "#.*$", "");
                return (string)Regex.Replace(s, "\\s", "");
            }).Where(s => s != "").ToArray();
        }

        public static string[] Heroes {
            get {
                if (instance == null)
                    instance = new HeroList();
                return instance.heroes;
            }
        }
    }
}
