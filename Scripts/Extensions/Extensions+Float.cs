using System.Runtime.CompilerServices;
using Unity.Mathematics;

namespace Foundation {
    public static partial class Extensions {
        public const float EPSILON1 = 0.1f;
        public const float EPSILON2 = 0.01f;
        public const float EPSILON3 = 0.001f;
        public const float EPSILON4 = 0.0001f;
        public const float EPSILON5 = 0.00001f;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float RoundToNearest(this float input, float nearest)
            => math.round(input / nearest) * nearest;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float FloorToNearest(this float input, float nearest)
            => math.floor(input / nearest) * nearest;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float CeilToNearest(this float input, float nearest)
            => math.ceil(input / nearest) * nearest;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Approximately(this float input, float other, float epsilon = EPSILON4)
            => math.abs(input - other) < (epsilon * epsilon);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Wrap(this float input, float max)
            => Wrap(input, 0, max);

        public static float Wrap(this float input, float min, float max) {
            if (min >= max) {
                throw new System.ArgumentOutOfRangeException("'min' is equal to or greater than 'max'");
            }

            float delta = max - min;

            while (input < min) {
                input += delta;
            }
            while (input > max) {
                input -= delta;
            }

            return input;
        }
    }
}