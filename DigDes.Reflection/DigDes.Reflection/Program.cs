using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DigDes.Reflection.Model;

namespace DigDes.Reflection {
	class Program {
		static void Main(string[] args) {
			IlExample();
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
