using System;
using System.Collections.Generic;

namespace Foundation {
    public static partial class Extensions {
        public static bool Any<Element>(this IEnumerable<Element> collection, Func<Element, bool> body) {
            foreach (Element element in collection) {
                if (body(element)) {
                    return true;
                }
            }
            return false;
        }

        public static bool All<Element>(this IEnumerable<Element> collection, Func<Element, bool> body) {
            foreach (Element element in collection) {
                if (!body(element)) {
                    return false;
                }
            }
            return true;
        }

        public static Element First<Element>(this IEnumerable<Element> collection, Func<Element, bool> body) {
            foreach (Element element in collection) {
                if (body(element)) {
                    return element;
                }
            }
            return default;
        }
    }
}