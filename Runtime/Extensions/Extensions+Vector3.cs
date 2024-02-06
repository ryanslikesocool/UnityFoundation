using System.Runtime.CompilerServices;
using UnityEngine;

namespace Foundation {
	public static partial class Extensions {
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 ClampMagnitude(this Vector3 input, float min, float max) {
			float magnitude = Mathf.Clamp(input.magnitude, min, max);
			return input.normalized * magnitude;
		}
	}
}