using System;

namespace Foundation {
    public static class FlagsHelper {
        public static bool Contains<T>(this T flags, T flag) where T : struct {
            int flagsValue = (int)(object)flags;
            int flagValue = (int)(object)flag;

            return (flagsValue & flagValue) != 0;
        }

        public static void Insert<T>(this ref T flags, T flag) where T : struct {
            int flagsValue = (int)(object)flags;
            int flagValue = (int)(object)flag;

            flags = (T)(object)(flagsValue | flagValue);
        }

        public static void Remove<T>(this ref T flags, T flag) where T : struct {
            int flagsValue = (int)(object)flags;
            int flagValue = (int)(object)flag;

            flags = (T)(object)(flagsValue & (~flagValue));
        }

        public static void Set<T>(this ref T flags, T flag, bool state) where T : struct {
            if (state == flags.Contains(flag)) {
                return;
            }

            if (state) {
                flags.Insert(flag);
            } else {
                flags.Remove(flag);
            }
        }
    }
}