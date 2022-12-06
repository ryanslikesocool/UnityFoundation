using System;
using System.Collections.Generic;

namespace Foundation {
    public static partial class Extensions {
        public static Result[] Map<Element, Result>(this Element[] collection, Func<Element, Result> body) {
            Result[] result = new Result[collection.Length];
            for (int i = 0; i < collection.Length; i++) {
                result[i] = body(collection[i]);
            }
            return result;
        }

        public static Element[] Filter<Element>(this Element[] collection, Func<Element, bool> body) {
            List<Element> result = new List<Element>(collection.Length);
            for (int i = 0; i < collection.Length; i++) {
                if (body(collection[i])) {
                    result.Add(collection[i]);
                }
            }
            return result.ToArray();
        }

        public static int FirstIndex<Element>(this Element[] collection, Func<Element, bool> body) {
            for (int i = 0; i < collection.Length; i++) {
                if (body(collection[i])) {
                    return i;
                }
            }
            return -1;
        }

        public static Element First<Element>(this Element[] collection) => collection[0];

        public static Element Last<Element>(this Element[] collection) => collection[^1];

        public static Element Random<Element>(this Element[] collection) {
            int index = UnityEngine.Random.Range(0, collection.Length);
            return collection[index];
        }

        public static Element[] Fill<Element>(this Element[] collection, Element value) {
            for (int i = 0; i < collection.Length; i++) {
                collection[i] = value;
            }
            return collection;
        }
    }
}