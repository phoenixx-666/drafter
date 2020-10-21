using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace drafter {
    public class SearchResult {

        public SearchResult(string heroname, RectangleF rect, PointF location) {
            HeroName = heroname;
            Rect = rect;
            Location = location;
        }

        public SearchResult(string heroname, List<Emgu.CV.Structure.MDMatch[]> matches, Emgu.CV.Util.VectorOfKeyPoint kp) {
            HeroName = heroname.Split('_')[0];
            Matches = matches.Where(m => m[0].Distance <= matches.Select(m0 => (double)m0[0].Distance).Median()).ToList();
            var nmatches = matches.Count;
            var mp = matches.Select(m => kp[m[0].TrainIdx].Point).ToList();
            MatchPointsRaw = mp;

            var averdist = new Dictionary<PointF, double>();
            var median_point = new PointF((float)mp.Select(pt => (double)pt.X).Median(), (float)mp.Select(pt => (double)pt.Y).Median());
            if (mp.Count > 1)
                mp.ForEach(pt => averdist[pt] = Math.Sqrt(Math.Pow(pt.X - median_point.X, 2) + Math.Pow(pt.Y - median_point.Y, 2)));
            else averdist[mp.First()] = 0;

            //mp.ForEach(pt => averdist[pt] = mp.Where(pt1 => pt1 != pt)
            //.Select(pt1 => Math.Sqrt(Math.Pow(pt1.X - pt.X, 2) + Math.Pow(pt1.Y - pt.Y, 2))).Average());
            var deviation = averdist.Values.Median() * 2;
            //var deviation = averdist.Count > 1 ? averdist.Values.Average() + averdist.Values.StDev() * 3 : 0;

            mp = mp.Where(pt => averdist[pt] <= deviation).ToList();

            var xs = mp.Select(pt => pt.X).OrderBy(n => n).ToList();
            var ys = mp.Select(pt => pt.Y).OrderBy(n => n).ToList();

            var (minx, maxx) = (xs.First(), xs.Last());
            var (miny, maxy) = (ys.First(), ys.Last());

            MatchPoints = mp;
            Rect = new RectangleF(minx, miny, maxx - minx, maxy - miny);
            Location = new PointF(Rect.X + Rect.Width / 2, Rect.Y + Rect.Height / 2);
            Distance = matches.Select(m => (double)1 / m[0].Distance).Sum();
            /*if (HeroName == "chromie")
                Distance *= 2;
            else if (HeroName == "greymane")
                Distance *= 3;*/
        }

        public string HeroName { get; }
        public List<Emgu.CV.Structure.MDMatch[]> Matches { get; }
        public List<PointF> MatchPoints { get; }
        public List<PointF> MatchPointsRaw { get; }
        public RectangleF Rect { get; }
        public PointF Location { get; }
        public double Distance { get; }
    }
}
