using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace Foundation {
	public static partial class IDictionaryExtensions {
		[MethodImpl(AggressiveInlining)]
		public static Value? GetValue<Key, Value>(this IDictionary<Key, Value> collection, Key key) where Value : struct {
			if (collection.TryGetValue(key, out Value value)) {
				return value;
			} else {
				return null;
			}
		}

		[MethodImpl(AggressiveInlining)]
		public static Key? FirstKey<Key, Value>(this IDictionary<Key, Value> collection, Predicate<Value> predicate) where Key : struct {
			foreach (Key key in collection.Keys) {
				if (predicate(collection[key])) {
					return key;
				}
			}
			return null;
		}

		/// <returns>The number of elements that matched the <see paramref="predicate"/> and were removed from the <see paramref="collection"/>.</returns>
		[MethodImpl(AggressiveInlining)]
		public static int RemoveAll<Key, Value>(this IDictionary<Key, Value> collection, Func<Key, Value, bool> predicate) {
			int removedCount = 0;

			Key[] keys = collection.Keys.ToArray();
			foreach (Key key in keys) {
				if (predicate(key, collection[key])) {
					collection.Remove(key);
					removedCount += 1;
				}
			}

			return removedCount;
		}

		/// <returns>The number of elements that matched the <see paramref="predicate"/> and were removed from the <see paramref="collection"/>.</returns>
		[MethodImpl(AggressiveInlining)]
		public static int RemoveAll<Key, Value>(this IDictionary<Key, Value> collection, Func<Key, bool> predicate) {
			int removedCount = 0;

			Key[] keys = collection.Keys.ToArray();
			foreach (Key key in keys) {
				if (predicate(key)) {
					collection.Remove(key);
					removedCount += 1;
				}
			}

			return removedCount;
		}

		/// <returns>The number of elements that matched the <see paramref="predicate"/> and were removed from the <see paramref="collection"/>.</returns>
		[MethodImpl(AggressiveInlining)]
		public static int RemoveAll<Key, Value>(this IDictionary<Key, Value> collection, Func<Value, bool> predicate) {
			int removedCount = 0;

			Key[] keys = collection.Keys.ToArray();
			foreach (Key key in keys) {
				if (predicate(collection[key])) {
					collection.Remove(key);
					removedCount += 1;
				}
			}

			return removedCount;
		}
	}
}