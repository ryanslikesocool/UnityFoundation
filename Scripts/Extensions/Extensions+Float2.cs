using Unity.Mathematics;

namespace Foundation {
    public static partial class Extensions {
        public static float2 Wrap(this float2 value, float2 min, float2 max) => new float2(
            value.x.Wrap(min.x, max.x),
            value.y.Wrap(min.y, max.y)
        );
    }
}