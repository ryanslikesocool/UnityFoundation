using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Foundation {
	public static partial class Extensions {
		public static void Update<Key, Value>(this Dictionary<Key, Value> collection, Key key, ActionRef<Value> transform) {
			if (collection.Remove(key, out Value value)) {
				transform(ref value);
				collection.Add(key, value);
			}
		}

		public static void Update<Key, Value>(this Dictionary<Key, Value> collection, Key key, Func<Value, Value> transform) {
			if (collection.Remove(key, out Value value)) {
				collection.Add(key, transform(value));
			}
		}

		public static void Update<Key, Value>(this Dictionary<Key, Value> collection, Key key, Value @default, ActionRef<Value> transform) {
			Value value;
			if (!collection.Remove(key, out value)) {
				value = @default;
			}

			transform(ref value);
			collection.Add(key, value);
		}

		public static void Update<Key, Value>(this Dictionary<Key, Value> collection, Key key, Value @default, Func<Value, Value> transform) {
			Value value;
			if (!collection.Remove(key, out value)) {
				value = @default;
			}

			collection.Add(key, transform(value));
		}
	}
}