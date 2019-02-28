using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaggNough.Entities {
	public class TreeNode<T> {
		public T Id;
		public T ParentId;
		public string Value;


		public TreeNode(T id, T parentId, string value) {
			Id = id;
			ParentId = parentId;
			Value = value;
		}

		public TreeNode(T id, T parentId) : this(id, parentId, $"Node_{id}_{parentId}") {
		}
	}
}
