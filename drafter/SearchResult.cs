using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Emgu.CV.Structure;

namespace drafter {
    public class SearchResult {

        public SearchResult(string name, RectangleF rect, PointF location) {
            Name = name;
            Rect = rect;
            Location = location;
        }

        public SearchResult(string name, List<Emgu.CV.Structure.MDMatch[]> matches, Emgu.CV.Util.VectorOfKeyPoint kp, float minSide = 0.0f) {
            Name = name.Split('_')[0];
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

            var (w, h) = (xs.Last() - xs.First(), ys.Last() - ys.First());
            var (cx, cy) = (xs.First() + w / 2, ys.First() + h / 2);
            (w, h) = (Math.Max(w, minSide), Math.Max(h, minSide));
#if DEBUG
            System.Diagnostics.Debug.WriteLine("{0:s}: {1:f}, {2:f}, {3:f}", name, minSide, w, h);
#endif

            MatchPoints = mp;
            Rect = new RectangleF(cx - w / 2, cy - h / 2, w, h);
            Location = new PointF(cx, cy);
            Distance = matches.Select(m => (double)1 / m[0].Distance).Sum();
            /*if (HeroName == "chromie")
                Distance *= 2;
            else if (HeroName == "greymane")
                Distance *= 3;*/
        }

        public string Name { get; }
        public List<Emgu.CV.Structure.MDMatch[]> Matches { get; }
        public List<PointF> MatchPoints { get; }
        public List<PointF> MatchPointsRaw { get; }
        public RectangleF Rect { get; }
        public PointF Location { get; }
        public double Distance { get; }
    }
}
