using System.Runtime.CompilerServices;
using Unity.Mathematics;

namespace Foundation {
	public static partial class Extensions {
		public static float3 Wrap(this float3 value, float3 min, float3 max) => new float3(
			value.x.Wrap(min.x, max.x),
			value.y.Wrap(min.y, max.y),
			value.z.Wrap(min.z, max.z)
		);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Approximately(this float3 a, float3 b, float epsilon = EPSILON4)
			=> math.distancesq(a, b) < epsilon * epsilon;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 RotateAround(this float3 point, float3 pivot, quaternion rotation)
			=> math.rotate(rotation, point - pivot) + pivot;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 RotateAround(this float3 point, float3 pivot, float3 rotation, bool isDegrees) {
			rotation = math.select(rotation, math.radians(rotation), isDegrees);
			return RotateAround(point, pivot, quaternion.Euler(rotation));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Angle(float3 a, float3 b) {
			float dot = math.dot(a, b);
			dot /= math.length(a) * math.length(b);
			return math.acos(dot);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float SignedAngle(float3 a, float3 b, float3 normal) {
			float dot = math.dot(a, b);
			dot /= math.length(a) * math.length(b);
			float angle = math.acos(dot);
			float3 cross = math.cross(a, b);
			if (math.dot(normal, cross) < 0) {
				angle = -angle;
			}
			return angle;
		}
	}
}