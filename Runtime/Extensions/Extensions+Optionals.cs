using System;

namespace Foundation {
    public static partial class Extensions {
        public static bool TryGetValue<T>(this T? source, out T value) where T : struct {
            value = source.GetValueOrDefault();
            return source.HasValue;
        }

        public static bool TryGetValue<T>(this T source, out T value) where T : class {
            if (source is not null) {
                value = source;
                return true;
            } else {
                value = null;
                return false;
            }
        }
    }
}