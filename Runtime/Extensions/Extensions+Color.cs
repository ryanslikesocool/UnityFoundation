using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

namespace Foundation {
	public static partial class Extensions {
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Color Opacity(this Color color, float opacity) {
			color.a = opacity;
			return color;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Color HDR(this Color color, float hdr) {
			color.r *= hdr;
			color.g *= hdr;
			color.b *= hdr;
			return color;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Approximately(this Color color, Color other, float epsilon = EPSILON4)
			=> color.AsFloat4().Approximately(other.AsFloat4(), epsilon);
	}
}