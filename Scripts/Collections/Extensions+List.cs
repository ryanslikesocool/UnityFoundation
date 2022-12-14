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

        public static List<Element> Filter<Element>(this List<Element> collection, Func<Element, bool> condition) {
            List<Element> result = new List<Element>(collection.Count);
            for (int i = 0; i < collection.Count; i++) {
                if (condition(collection[i])) {
                    result.Add(collection[i]);
                }
            }
            return result;
        }
    }
}