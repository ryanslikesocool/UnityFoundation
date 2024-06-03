using System.Runtime.CompilerServices;
using static System.Runtime.CompilerServices.MethodImplOptions;
using Unity.Mathematics;
using UnityEngine;

namespace Foundation {
	public static partial class Extensions {
		[MethodImpl(AggressiveInlining)]
		public static Color WithOpacity(this Color color, float opacity) {
			color.a = opacity;
			return color;
		}

		[MethodImpl(AggressiveInlining)]
		public static Color ReplaceChannels(this Color color, ColorChannel channels, Color newValue) {
			if (channels.HasFlag(ColorChannel.Red)) {
				color.r = newValue.r;
			}
			if (channels.HasFlag(ColorChannel.Green)) {
				color.g = newValue.g;
			}
			if (channels.HasFlag(ColorChannel.Blue)) {
				color.b = newValue.b;
			}
			if (channels.HasFlag(ColorChannel.Alpha)) {
				color.a = newValue.a;
			}
			return color;
		}

		[MethodImpl(AggressiveInlining)]
		public static Color HDR(this Color color, float hdr) {
			color.r *= hdr;
			color.g *= hdr;
			color.b *= hdr;
			return color;
		}

		[MethodImpl(AggressiveInlining)]
		public static bool Approximately(this Color color, Color other, float epsilon = EPSILON4)
			=> color.AsFloat4().Approximately(other.AsFloat4(), epsilon);


	}
}