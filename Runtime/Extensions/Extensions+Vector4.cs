using System.Runtime.CompilerServices;
using UnityEngine;

namespace Foundation {
    public static partial class Extensions {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Approximately(this Vector4 input, Vector4 other, float epsilon = EPSILON4)
            => Vector2.SqrMagnitude(input - other) < epsilon * epsilon;
    }
}