using System.Runtime.CompilerServices;
using UnityEngine;

namespace Foundation {
	public static partial class Extensions {
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 ClampMagnitude(this Vector2 input, float min, float max) {
			float magnitude = Mathf.Clamp(input.magnitude, min, max);
			return input.normalized * magnitude;
		}

		/// <summary>
		/// Rotate a vector around the origin by <paramref name="angle"/> degrees.
		/// </summary>
		/// <param name="input">The input vector.</param>
		/// <param name="angle">The angle to rotate by, in degrees.</param>
		/// <returns>The given vector, rotated about the origin by <paramref name="angle"/> degrees.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 RotateDegrees(this Vector2 input, float angle)
			=> RotateRadians(input, angle * Mathf.Deg2Rad);

		/// <summary>
		/// Rotate a vector around the origin by <paramref name="angle"/> radians.
		/// </summary>
		/// <param name="input">The input vector.</param>
		/// <param name="angle">The angle to rotate by, in radians.</param>
		/// <returns>The given vector, rotated about the origin by <paramref name="angle"/> radians.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 RotateRadians(this Vector2 input, float angle) {
			float sn = Mathf.Sin(angle);
			float cs = Mathf.Cos(angle);

			return new Vector2(
				input.x * cs - input.y * sn,
				input.x * sn + input.y * cs
			);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Abs(this Vector2 input) => new Vector2(
			Mathf.Abs(input.x),
			Mathf.Abs(input.y)
		);
	}
}