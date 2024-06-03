using System.Runtime.CompilerServices;
using UnityEngine;

namespace Foundation {
	public static partial class Extensions {
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 ClampMagnitude(this Vector3 input, float min, float max) {
			float magnitude = Mathf.Clamp(input.magnitude, min, max);
			return input.normalized * magnitude;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Approximately(this Vector3 lhs, Vector3 rhs)
			=> Mathf.Approximately(lhs.x, rhs.x) && Mathf.Approximately(lhs.y, rhs.y) && Mathf.Approximately(lhs.z, rhs.z);
	}
}