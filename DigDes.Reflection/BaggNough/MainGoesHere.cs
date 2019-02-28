using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaggNough
{
    class MainGoesHere
    {
        /// <summary>
        /// Чтоб было удобно останавливать отладчик.
        /// </summary>
        public static void BreakHere() { }

        // Точка входа.
        // Приложение не консольное; вне отладки ничего интересного не сделает. Даже консоль не откроет.
        public static void Main()
        {
            // Now testing: TopSortable
            var ts = new TopSortable(5, 4);
            ts.AddOrd(4, 1);
            ts.AddOrds(
                (1, 2),
                (2, 3),
                (5, 3));
            // Это отношение портит граф:
            // ts.AddOrd(2, 1);
            var sorted = ts.Sort();
            var allgood = ts.Check(sorted);
            BreakHere();
            return;
        }
    }
}
