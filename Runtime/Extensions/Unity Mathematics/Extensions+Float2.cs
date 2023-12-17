using System.Runtime.CompilerServices;
using Unity.Mathematics;

namespace Foundation {
	public static partial class Extensions {
		public static float2 Wrap(this float2 value, float2 min, float2 max) => new float2(
			value.x.Wrap(min.x, max.x),
			value.y.Wrap(min.y, max.y)
		);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 RoundToNearest(this float2 input, float2 nearest)
			=> math.round(input / nearest) * nearest;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 FloorToNearest(this float2 input, float2 nearest)
			=> math.floor(input / nearest) * nearest;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 CeilToNearest(this float2 input, float2 nearest)
			=> math.ceil(input / nearest) * nearest;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Approximately(this float2 a, float2 b, float epsilon = EPSILON4)
			=> math.distancesq(a, b) < epsilon * epsilon;

		/// <summary>
		/// Rotate a vector around the origin by <paramref name="angle"/> degreen.
		/// </summary>
		/// <param name="input">The input vector.</param>
		/// <param name="angle">The angle to rotate by, in degrees.</param>
		/// <returns>The given vector, rotated about the origin by <paramref name="angle"/> degrees.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 RotateDegrees(this float2 input, float angle)
			=> RotateRadians(input, math.radians(angle));

		/// <summary>
		/// Rotate a vector around the origin by <paramref name="angle"/> radians.
		/// </summary>
		/// <param name="input">The input vector.</param>
		/// <param name="angle">The angle to rotate by, in radians.</param>
		/// <returns>The given vector, rotated about the origin by <paramref name="angle"/> radians.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 RotateRadians(this float2 input, float angle) {
			math.sincos(angle, out float sn, out float cs);

			return new float2(
				input.x * cs - input.y * sn,
				input.x * sn + input.y * cs
			);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 Negate(this float2 value, bool2 condition) {
			for (int i = 0; i < 2; i++) {
				if (condition[i]) {
					value[i] *= -1;
				}
			}
			return value;
		}
	}
}