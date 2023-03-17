using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

namespace Foundation {
    public static partial class Extensions {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 ToFloat4(this Color color)
            => new float4(color.r, color.g, color.b, color.a);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Approximately(this float4 a, float4 b, float epsilon = EPSILON4)
            => math.distancesq(a, b) < math.abs(epsilon);
    }
}