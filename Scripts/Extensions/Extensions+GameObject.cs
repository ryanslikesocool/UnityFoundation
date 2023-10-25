using System.Collections.Generic;
using UnityEngine;

namespace Foundation {
	public partial class Extensions {
		public static void AddChild(this GameObject parent, GameObject child) {
			child.transform.parent = parent.transform;
		}

		public static void AddChildren(this GameObject parent, IEnumerable<GameObject> children) {
			children.ForEach(child => AddChild(parent, child));
		}

		public static void RemoveFromParent(this GameObject child) {
			child.transform.parent = null;
		}
	}
}