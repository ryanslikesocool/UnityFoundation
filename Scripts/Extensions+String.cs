using System;

namespace Foundation {
    public static partial class Extensions {
        public static string Joined(this string[] collection, string separator) => String.Join(separator, collection);

        public static string Joined(this string[] collection, char separator) => String.Join(separator, collection);

        public static string Joined(this char[] collection, string separator) => String.Join(separator, collection);

        public static string Joined(this char[] collection, char separator) => String.Join(separator, collection);

        public static T[] Map<T>(this string input, Func<char, T> body) => input.ToCharArray().Map(body);
    }
}