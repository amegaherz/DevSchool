using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DigDes.Reflection.Model;

namespace DigDes.Reflection {
	class Program {
		static void Main(string[] args) {
			LinqExample();
			//IlExample();
		}

		private static void LinqExample() {

			var w = Stopwatch.StartNew();
			var arr = new int[] {
				10000,2,3,4,5,6,7,10
			};

			var min = arr.Min();
			var max = arr.Max();
			var buf2 = Enumerable.Range(min, max);

			buf2 = buf2.Except(arr).ToArray();


			//Array.Sort(arr);

			////if (arr[arr.Length -1] - arr[0])


			//var buf = new List<int>(arr[arr.Length - 1] - arr[0] - arr.Length + 1);
			//for (int i = 1; i < arr.Length; i++) {
			//	if (arr[i] - arr[i - 1] > 1) {
			//		for (int j = 1; j <= arr[i] - arr[i - 1] - 1; j++) {
			//			buf.Add(arr[i - 1] + j);
			//		}
			//	}
			//}
			w.Stop();
			return;
		}

		private static void IlExample() {

			BaseObj a = new BaseObj();
			var t = a.GetType();
			var mi = t.GetMethod("Get6", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
			var val = mi.Invoke(a, null);

			var tt = typeof(BaseObj);
			var types = tt.Assembly.GetTypes();

			Type abstrType = null;
			
			foreach (var type in types) {
				if (type.Name == "AbstractObj") {
					abstrType = type;
					break;
				}
			}

			var obj = Activator.CreateInstance(abstrType);
			
		}





	}
	

}
