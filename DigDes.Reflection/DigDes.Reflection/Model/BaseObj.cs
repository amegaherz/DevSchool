using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigDes.Reflection.Model {
	public class BaseObj : IMyInterface {
		public int Gat5() {
			return 16;
		}

		public void GetAnyOperation(int arg) {
			throw new NotImplementedException();
		}

		private int Get6() {
			return 5;
		}
	}
}
