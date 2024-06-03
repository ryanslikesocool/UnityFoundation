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

		public static string RemovePrefix(this string value, in string prefix) {
			if (value.StartsWith(prefix)) {
				return value.Remove(0, prefix.Length);
			}
			return value;
		}

		public static string RemovePrefix(this string value, in string prefix, StringComparison comparisonType) {
			if (value.StartsWith(prefix, comparisonType)) {
				return value.Remove(0, prefix.Length);
			}
			return value;
		}

		public static string RemoveSuffix(this string value, in string suffix) {
			if (value.EndsWith(suffix)) {
				return value.Remove(value.Length - suffix.Length);
			}
			return value;
		}

		public static string RemoveSuffix(this string value, in string suffix, StringComparison comparisonType) {
			if (value.EndsWith(suffix, comparisonType)) {
				return value.Remove(value.Length - suffix.Length);
			}
			return value;
		}
	}
}