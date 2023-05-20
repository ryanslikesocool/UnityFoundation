using System;
using System.Collections.Generic;

namespace Foundation {
    public static partial class Extensions {
        public static void ForEach<Element>(this IEnumerable<Element> collection, Action<Element> body) {
            foreach (Element element in collection) {
                body(element);
            }
        }

        /// <summary>
        /// Returns true if any element in a collection meets the condition, false otherwise.
        /// </summary>
        public static bool Any<Element>(this IEnumerable<Element> collection, Func<Element, bool> condition) {
            foreach (Element element in collection) {
                if (condition(element)) {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns true if all elements in a collection meet the condition, false otherwise.
        /// </summary>
        public static bool All<Element>(this IEnumerable<Element> collection, Func<Element, bool> condition) {
            foreach (Element element in collection) {
                if (!condition(element)) {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Counts the number of elements in a collection where the condition is true.
        /// </summary>
        public static int Count<Element>(this IEnumerable<Element> collection, Func<Element, bool> condition) {
            int result = 0;
            foreach (Element element in collection) {
                if (condition(element)) {
                    result++;
                }
            }
            return result;
        }

        public static bool Contains<Element>(this IEnumerable<Element> collection, Func<Element, bool> condition) {
            foreach (Element element in collection) {
                if (condition(element)) {
                    return true;
                }
            }
            return false;
        }

#nullable enable
        /// <summary>
        /// Returns the first element in a collection that matches the condition, null otherwise.
        /// </summary>
        public static Element? First<Element>(this IEnumerable<Element> collection, Func<Element, bool> condition) {
            foreach (Element element in collection) {
                if (condition(element)) {
                    return element;
                }
            }
            return default(Element?);
        }
#nullable disable
    }
}