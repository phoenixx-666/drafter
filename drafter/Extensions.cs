using System;
using System.Collections.Generic;
using System.Linq;

namespace drafter {
    public static class Extensions {
        public static double Median(this IEnumerable<double> values) {
            var count = values.Count();
            var sorted = values.OrderBy(t => t);
            if (count % 2 == 0)
                return sorted.Skip((int)(count / 2) - 1).Take(2).Average();
            else
                return sorted.ElementAt((int)((count - 1) / 2));
        }

        public static double StDev(this IEnumerable<double> values) {
            double mean = values.Average();
            return Math.Sqrt(1 / (values.Count() - 1) * values.Select(x => Math.Pow(x - mean, 2)).Sum());
        }

        public static void ForEach<T>(this IEnumerable<T> self, Action<T, int> action, int start = 0) {
            foreach (var e in self) action(e, start++);
        }
    }
}
