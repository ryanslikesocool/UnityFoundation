using System;

namespace Foundation {
    public static partial class Extensions {
        public static bool TryGetValue<T>(this T? source, out T value) where T : struct {
            value = source.GetValueOrDefault();
            return source.HasValue;
        }
    }
}