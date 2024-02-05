using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

namespace Foundation {
	public static partial class Extensions {
		public static float4 Wrap(this float4 value, float4 min, float4 max) => new float4(
			value.x.Wrap(min.x, max.x),
			value.y.Wrap(min.y, max.y),
			value.z.Wrap(min.z, max.z),
			value.w.Wrap(min.w, max.w)
		);


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 AsFloat4(this Color color)
			=> new float4(color.r, color.g, color.b, color.a);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Approximately(this float4 a, float4 b, float epsilon = EPSILON4)
			=> math.distancesq(a, b) < epsilon * epsilon;
	}
}