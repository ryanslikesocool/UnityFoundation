using System.Runtime.CompilerServices;
using Unity.Mathematics;

namespace Foundation {
    public static partial class Extensions {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 Wrap(this float2 value, float2 min, float2 max) => new float2(
            value.x.Wrap(min.x, max.x),
            value.y.Wrap(min.y, max.y)
        );

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Approximately(this float2 a, float2 b, float epsilon = EPSILON4)
            => math.distancesq(a, b) < math.abs(epsilon);
    }
}