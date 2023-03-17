using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

namespace Foundation {
    public static partial class Extensions {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Angle(quaternion a, quaternion b) {
            float3 forward = new float3(0, 0, 1);
            float3 va = math.rotate(a, forward);
            float3 vb = math.rotate(b, forward);
            return Angle(va, vb);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float SignedAngle(quaternion a, quaternion b, float3 normal) {
            float3 forward = new float3(0, 0, 1);
            float3 va = math.rotate(a, forward);
            float3 vb = math.rotate(b, forward);
            return SignedAngle(va, vb, normal);
        }
    }
}