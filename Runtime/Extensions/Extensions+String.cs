using System;
using System.Collections.Generic;

namespace Foundation {
    public static partial class Extensions {
        public static string Joined(this string[] collection, string separator) => String.Join(separator, collection);

        public static string Joined(this string[] collection, char separator) => String.Join(separator, collection);

        public static string Joined(this char[] collection, string separator) => String.Join(separator, collection);

        public static string Joined(this char[] collection, char separator) => String.Join(separator, collection);

        public static IEnumerable<T> Map<T>(this string input, Func<char, T> body) => input.ToCharArray().Map(body);

        public static string Reversed(this string value) => new string(value.ToCharArray().Reversed());
    }
}