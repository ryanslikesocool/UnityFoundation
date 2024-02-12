using UnityEngine;

namespace Foundation {
	public static partial class Extensions {
		public static float GetDuration(this AnimationCurve curve) => curve.keys.Last().time - curve.keys.First().time;
	}
}