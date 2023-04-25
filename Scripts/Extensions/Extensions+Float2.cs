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
    }
}