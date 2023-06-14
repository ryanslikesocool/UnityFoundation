using System;
using System.Collections.Generic;

namespace Foundation {
    public static partial class Extensions {
        /// <summary>
        /// Calls the given closure on each element in the sequence in the same order as a foreach loop.
        /// </summary>
        /// <remarks>
        /// Using the ForEach method is distinct from a foreach loop in two important ways:
        /// <br/>1. You cannot use a break or continue statement to exit the current call of the body closure or skip subsequent calls.
        /// <br/>2. Using the return statement in the body closure will exit only from the current call to body, not from any outer scope, and won’t skip subsequent calls.
        /// </remarks>
        /// <param name="body">A closure that takes an element of the sequence as a parameter.</param>
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
        /// Returns a collection containing the results of mapping the given closure over the sequence’s elements.
        /// </summary>
        /// <param name="transform">A mapping closure. <paramref name="transform"/> accepts an element of this sequence as its parameter and returns a transformed value of the same or of a different type.</param>
        /// <returns>A collection containing the transformed elements of this sequence.</returns>
        public static IEnumerable<Result> Map<Element, Result>(this IEnumerable<Element> collection, Func<Element, Result> transform) {
            foreach (Element element in collection) {
                yield return transform(element);
            }
        }

        /// <summary>
        /// Returns a collection containing, in order, the elements of the sequence that satisfy the given predicate.
        /// </summary>
        /// <param name="isIncluded">A closure that takes an element of the sequence as its argument and returns a Boolean value indicating whether the element should be included in the returned array.</param>
        /// <returns>A collection of the elements that isIncluded allowed.</returns>
        public static IEnumerable<Element> Filter<Element>(this IEnumerable<Element> collection, Func<Element, bool> isIncluded) {
            foreach (Element element in collection) {
                if (isIncluded(element)) {
                    yield return element;
                }
            }
        }

        /// <summary>
        /// Returns a collection containing the concatenated results of calling the given transformation with each element of this sequence.
        /// </summary>
        /// <param name="transform">A closure that accepts an element of this sequence as its argument and returns a sequence or collection.</param>
        /// <returns>The resulting flattened collection.</returns>
        public static IEnumerable<Element> FlatMap<Source, Element>(this IEnumerable<Source> collection, Func<Source, IEnumerable<Element>> transform) {
            foreach (Source source in collection) {
                foreach (Element element in transform(source)) {
                    yield return element;
                }
            }
        }

        public static IEnumerable<Element> CompactMap<Source, Element>(this IEnumerable<Source> collection, Func<Source, Element?> transform) where Element : struct {
            foreach (Source source in collection) {
                if (transform(source).TryGetValue(out Element element)) {
                    yield return element;
                }
            }
        }

        public static IEnumerable<Element> CompactMap<Source, Element>(this IEnumerable<Source> collection, Func<Source, Element> transform) where Element : class {
            foreach (Source source in collection) {
                if (transform(source).TryGetValue(out Element element)) {
                    yield return element;
                }
            }
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

        // System.Linq passthrough
        public static Element[] ToArray<Element>(this IEnumerable<Element> collection) => System.Linq.Enumerable.ToArray(collection);

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