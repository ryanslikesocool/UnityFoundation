using Unity.Mathematics;

namespace Foundation {
    public static partial class Extensions {
        public static float3 Wrap(this float3 value, float3 min, float3 max) => new float3(
            value.x.Wrap(min.x, max.x),
            value.y.Wrap(min.y, max.y),
            value.z.Wrap(min.z, max.z)
        );
    }
}