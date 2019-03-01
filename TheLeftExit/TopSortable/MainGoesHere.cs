using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using TopSortable.Entities;

namespace TopSortable
{
    public static partial class MainGoesHere
    {
        // Точка входа.
        // Приложение не консольное; вне отладки ничего интересного не сделает. Даже консоль не откроет.
        public static void Main()
        {
            Example2();
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

            var sorted = list.TSort();

            var amirite = list.TSortCheck(sorted);

            ;
		}
	}
}
