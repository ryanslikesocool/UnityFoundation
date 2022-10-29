using System;
using System.Collections.Generic;

namespace Foundation {
    public static partial class Extensions {
        public static List<Result> Map<Element, Result>(this List<Element> collection, Func<Element, Result> body) {
            List<Result> result = new List<Result>(collection.Count);
            for (int i = 0; i < collection.Count; i++) {
                result.Add(body(collection[i]));
            }
            return result;
        }

        public static List<Element> Filter<Element>(this List<Element> collection, Func<Element, bool> body) {
            List<Element> result = new List<Element>(collection.Count);
            for (int i = 0; i < collection.Count; i++) {
                if (body(collection[i])) {
                    result.Add(collection[i]);
                }
            }
            return result;
        }

        public static int FirstIndex<Element>(this List<Element> collection, Func<Element, bool> body) {
            for (int i = 0; i < collection.Count; i++) {
                if (body(collection[i])) {
                    return i;
                }
            }
            return -1;
        }

        public static Element First<Element>(this List<Element> collection) => collection[0];

        public static Element Last<Element>(this List<Element> collection) => collection[collection.Count - 1];

        public static Element Random<Element>(this List<Element> collection) {
            int index = UnityEngine.Random.Range(0, collection.Count);
            return collection[index];
        }
    }
}