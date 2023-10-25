using UnityEngine;

namespace Foundation {
	public static partial class Extensions {
		public static void Reset(this Transform transform) {
			transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
			transform.localScale = Vector3.one;
		}
	}
}