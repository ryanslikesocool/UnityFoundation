using Unity.Mathematics;

namespace Foundation {
    public static partial class Extensions {
        public static float RoundToNearest(this float input, float nearest)
            => math.round(input / nearest) * nearest;

        public static float FloorToNearest(this float input, float nearest)
            => math.floor(input / nearest) * nearest;

        public static float CeilToNearest(this float input, float nearest)
            => math.ceil(input / nearest) * nearest;

        public static bool Approximately(this float input, float other, float epsilon = 0.00001f)
            => math.abs(input - other) < epsilon;

        public static float Wrap(this float input, float max)
            => Wrap(input, 0, max);

        public static float Wrap(this float input, float min, float max) {
            Guard.Require(min < max, "'min' is equal or greater than 'max'");
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