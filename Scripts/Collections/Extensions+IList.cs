using System;
using System.Collections;
using System.Collections.Generic;

namespace Foundation {
    public static partial class Extensions {
        public static Range<int> Bounds<_>(this IList<_> collection)
            => new Range<int>(0, collection.Count);

        public static Indices Indices<_>(this IList<_> collection)
            => new Indices(collection.Bounds());

        /// <summary>
        /// Returns the index of the first element in a collection that meets the condition.
        /// </summary>
        public static int? FirstIndex<Element>(this IList<Element> collection, Func<Element, bool> condition) {
            for (int i = 0; i < collection.Count; i++) {
                if (condition(collection[i])) {
                    return i;
                }
            }
            return null;
        }

        /// <summary>
        /// Returns the index of the last element in a collection that meets the condition.
        /// </summary>
        public static int? LastIndex<Element>(this IList<Element> collection, Func<Element, bool> condition) {
            for (int i = collection.Count - 1; i >= 0; i--) {
                if (condition(collection[i])) {
                    return i;
                }
            }
            return null;
        }

        public static bool IsEmpty<_>(this IList<_> collection) => collection.Count == 0;

#nullable enable
        /// <summary>
        /// Returns the first element in a collection.
        /// </summary>
        public static Element? First<Element>(this IList<Element> collection) => collection.IsEmpty() ? default(Element) : collection[0];

        /// <summary>
        /// Returns the last element in a collection.
        /// </summary>
        public static Element? Last<Element>(this IList<Element> collection) => collection.IsEmpty() ? default(Element) : collection[^1];

        /// <summary>
        /// Returns a random element in a collection.
        /// </summary>
        public static Element? Random<Element>(this IList<Element> collection) {
            if (collection.IsEmpty()) {
                return default(Element);
            }
            int index = UnityEngine.Random.Range(0, collection.Count);
            return collection[index];
        }
#nullable disable
    }
}