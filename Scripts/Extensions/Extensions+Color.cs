using System.Runtime.CompilerServices;
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
    }
}