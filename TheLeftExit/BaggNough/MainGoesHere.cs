using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaggNough.Entities;

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


		public static void Example2() {
			var list = new List<TreeNode<int>> {
				new TreeNode<int>(13, 10),
				new TreeNode<int>(12, 2),
				new TreeNode<int>(10, 2),
				new TreeNode<int>(14, 6),
				new TreeNode<int>(11, 8),
				new TreeNode<int>(5, 4),
				new TreeNode<int>(3, 4),
				new TreeNode<int>(9, 8),
				new TreeNode<int>(7, 3),
				new TreeNode<int>(6, 4),
				new TreeNode<int>(2, 4),
				new TreeNode<int>(8, 3)
			};

			// :TODO: А вот так сможешь? :)
			// var sorted = list.TSort();
		}
	}
}
