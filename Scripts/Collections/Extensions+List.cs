using System;
using System.Collections.Generic;

namespace Foundation {
    public static partial class Extensions {
        public static List<Element> DropFirst<Element>(this List<Element> collection) {
            collection.RemoveAt(0);
            return collection;
        }

        public static List<Element> DropFirst<Element>(this List<Element> collection, int count) {
            collection.RemoveRange(0, count);
            return collection;
        }

        public static List<Element> DropLast<Element>(this List<Element> collection, int count) {
            collection.RemoveRange(collection.Count - count, count);
            return collection;
        }

        public static void Append<Element>(this List<Element> collection, in Element element)
            => collection.Add(element);

        public static void Append<Element>(this List<Element> collection, in IEnumerable<Element> other)
            => collection.AddRange(other);
    }
}