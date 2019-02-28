using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaggNough
{
    /*
     * Топологическая сортировка.
     * До вида LINQ не довёл. Возможно, позже доведу.
     * Можно использовать в комбинации с SortedList или другими типами для сортировки массивов.
     */

    /// <summary>
    /// Класс реализует топологическую сортировку графов с элементами, пронумерованными от 1 до elems.
    /// Отношения порядка добавляется динамически.
    /// </summary>
    class TopSortable
    {
        /// <summary>
        /// Количество элементов и номер последнего элемента
        /// </summary>
        private readonly int elems;
        /// <summary>
        /// Отношения порядка
        /// </summary>
        private List<(int,int)> links;
        /// <summary>
        /// Добавить отношение в граф.
        /// </summary>
        private void Append((int,int) ord)
        {
            if (ord.Item1 <= elems && ord.Item2 <= elems && !links.Contains(ord))
                links.Add(ord);
            else
                throw new Exception("Добавлено несуществующее отношение.");
        }
        /// <summary>
        /// Создать новый граф.
        /// </summary>
        /// <param name="n">Количество элементов</param>
        /// <param name="ls">Количество отношений</param>
        public TopSortable(int n, int ls = 0)
        {
            elems = n;
            links = new List<(int, int)>(ls);
        }
        /// <summary>
        /// Преобразовать граф в читаемый формат.
        /// </summary>
        public override string ToString()
        {
            var res = new StringBuilder();

            // 1. Математически верное определение графа. Трудно читается.

            //res.Append("({1.."); res.Append(elems); res.Append("},{");
            //foreach (var x in links)
            //{
            //    res.AppendFormat("({0},{1}),", x.Item1, x.Item2);
            //};
            //res.Remove(res.Length - 1, 1);
            //res.Append("})");

            // 2. Читаемое определение графа.
            
            res.Append('['); res.Append(elems); res.Append(']');
            foreach (var x in links)
            {
                res.AppendFormat("({0},{1})", x.Item1, x.Item2);
            }
            
            return res.ToString();
        }
        /// <summary>
        /// Добавить отношение порядка.
        /// </summary>
        public void AddOrd(int top, int bot)
        {
            if (top <= elems && bot <= elems)
                links.Add((top,bot));
            else
                throw new Exception("В отношении указан несуществующий элемент.");
        }
        public void AddOrds(params (int,int)[] ords)
        {
            foreach (var x in ords)
                Append(x);
        }
        /// <summary>
        /// Выполнить топологическую сортировку графа в массив.
        /// </summary>
        public int[] Sort() {
            // Копия массива связей
            var tmplist = links.ToList();
            // Неотсортированные элементы
            var defeli = new BitArray(elems, true);
            // Отсортированный массив
            var res = new List<int>(elems);
            while (tmplist.Count > 0)
            {
                // Неотсортированные элементы, soon-to-be максимумы
                var eli = new BitArray(defeli);
                var stuck = true;
                // Убираем немаксимумы
                foreach (var x in tmplist)
                    eli[x.Item2 - 1] = false;
                // Каждый максимум помещаем в массив и удаляем из графа.
                for (int i = 1; i <= elems; i++)
                    if (eli[i-1])
                    {
                        defeli[i - 1] = false;
                        res.Add(i);
                        tmplist.RemoveAll(x => x.Item1 == i);
                        stuck = false;
                    }
                // Если максимумов нет, граф плохой.
                if (stuck)
                    throw new Exception("Граф невозможно отсортировать.");
            }
            // Связей не осталось, помещаем несвязанные остатки в массив.
            for (int i = 1; i <= elems; i++)
                if (defeli[i - 1])
                    res.Add(i);
            return res.ToArray();
        }
        /// <summary>
        /// Проверить массив на соответствие порядку графа.
        /// </summary>
        public bool Check(int[] arr)
        {
            // "Для каждого (x,y): x левее y"
            foreach (var x in links)
                if (Array.IndexOf(arr,x.Item1) > Array.IndexOf(arr,x.Item2))
                    return false;
            return true;
        }
    }
}
