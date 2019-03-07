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
        // Немного подредактированная версия TopSortable.Sort()
        public static List<T> TSort<T>(this List<TreeNode<T>> treenodelist)
        {
            // Скопируем дерево.
            var tree = treenodelist.ToList();
            // Проиндексируем элементы дерева.
            // Предположим, что подвешенных элементов нет, то есть у каждого есть хотя бы одна связь.
            var apples = new List<T>();
            foreach (var branch in tree)
            {
                if (!apples.Contains(branch.Id))
                    apples.Add(branch.Id);
                if (!apples.Contains(branch.ParentId))
                    apples.Add(branch.ParentId);
            }
            // Результат
            var barbecue = new List<T>(apples.Count);
            // Элементы, ещё не удалённые с дерева.
            var ontree = new BitArray(apples.Count, true);
            while (tree.Count > 0)
            {
                var stuck = false;
                // Находим максимумы
                var topapples = new BitArray(ontree);
                foreach (var branch in tree)
                    topapples[apples.IndexOf(branch.Id)] = false;
                // Помещаем их с дерева в результат
                for (int i = 0; i < apples.Count; i++)
                    if (topapples[i])
                    {
                        ontree[i] = false;
                        tree.RemoveAll(x => x.ParentId.Equals(apples[i]));
                        barbecue.Add(apples[i]);
                        stuck = true;
                    }
                if (!stuck)
                    throw new Exception("Дерево невозможно отсортировать.");
            }
            // Оставшиеся элементы помещаем в конец
            for (int i = 0; i < apples.Count; i++)
                if (ontree[i])
                    barbecue.Add(apples[i]);
            return barbecue;
        }

        public static bool TSortCheck<T>(this List<TreeNode<T>> tree, List<T> barbecue)
        {
            foreach (var x in tree)
                if (barbecue.IndexOf(x.ParentId) > barbecue.IndexOf(x.Id))
                    return false;
            return true;
        }
    }
}