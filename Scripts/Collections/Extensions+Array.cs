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

        public static Result[] FlatMap<Element, Result>(this Element[] collection, Func<Element, Result[]> body) {
            int count = 0;
            Result[][] intermediate = new Result[collection.Length][];
            for (int i = 0; i < collection.Length; i++) {
                intermediate[i] = body(collection[i]);
                count += intermediate[i].Length;
            }

            Result[] result = new Result[count];
            int accumulated = 0;
            for (int i = 0; i < intermediate.Length; i++) {
                for (int j = 0; j < intermediate[i].Length; j++) {
                    result[accumulated + j] = intermediate[i][j];
                }
                accumulated += intermediate[i].Length;
            }
            return result;
        }

        public static Element[] FlatMap<Element>(this Element[][] collection) {
            int count = 0;
            for (int i = 0; i < collection.Length; i++) {
                count += collection[i].Length;
            }
            Element[] result = new Element[count];
            int accumulated = 0;
            for (int i = 0; i < collection.Length; i++) {
                for (int j = 0; j < collection[i].Length; j++) {
                    result[accumulated + j] = collection[i][j];
                }
                accumulated += collection[i].Length;
            }
            return result;
        }

        public static Result[] CompactMap<Element, Result>(this Element[] collection, Func<Element, Result> body) where Result : class {
            int count = 0;
            Result[] result = new Result[collection.Length];
            for (int i = 0; i < collection.Length; i++) {
                Result newElement = body(collection[i]);

                if (newElement != null) {
                    result[count] = newElement;
                    count++;
                }
            }
            Array.Resize(ref result, count);
            return result;
        }

        public static Element[] CompactMap<Element>(this Element[] collection) where Element : class {
            int count = 0;
            Element[] result = new Element[collection.Length];
            for (int i = 0; i < collection.Length; i++) {
                if (collection[i] != null) {
                    result[count] = collection[i];
                    count++;
                }
            }
            Array.Resize(ref result, count);
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

        public static Element[] Fill<Element>(this Element[] collection, Element value) {
            for (int i = 0; i < collection.Length; i++) {
                collection[i] = value;
            }
            return collection;
        }

        public static Element[] Appent<Element>(this Element[] collection, params Element[] other)
            => Append(collection, other);

        public static Element[] Append<Element>(this Element[] collection, Element[] other) {
            int lowerBound = collection.Length;
            int upperBound = collection.Length + other.Length;

            Element[] result = new Element[upperBound];

            for (int i = 0; i < lowerBound; i++) {
                result[i] = collection[i];
            }
            for (int i = lowerBound; i < upperBound; i++) {
                result[i] = other[i - lowerBound];
            }

            return result;
        }
    }
}