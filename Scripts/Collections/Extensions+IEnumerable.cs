using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ForEach<Element>(this IEnumerable<Element> collection, Action<Element> body) {
            foreach (Element element in collection) {
                body(element);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Element> CompactMap<Source, Element>(this IEnumerable<Source> collection, Func<Source, Element?> transform) where Element : struct {
            foreach (Source source in collection) {
                if (transform(source).TryGetValue(out Element element)) {
                    yield return element;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Count<Element>(this IEnumerable<Element> collection, Func<Element, bool> condition) {
            int result = 0;
            foreach (Element element in collection) {
                if (condition(element)) {
                    result++;
                }
            }
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains<Element>(this IEnumerable<Element> collection, Func<Element, bool> condition) {
            foreach (Element element in collection) {
                if (condition(element)) {
                    return true;
                }
            }
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Element> FlatMap<Element>(this IEnumerable<IEnumerable<Element>> collections) {
            foreach (IEnumerable<Element> collection in collections) {
                foreach (Element element in collection) {
                    yield return element;
                }
            }
        }

#nullable enable
        /// <summary>
        /// Returns the first element in a collection that matches the condition, <see langword="null"/> otherwise.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Element? First<Element>(this IEnumerable<Element> collection, Func<Element, bool> condition) {
            foreach (Element element in collection) {
                if (condition(element)) {
                    return element;
                }
            }
            return default(Element?);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Element? First<Element>(this IEnumerable<Element> collection) {
            foreach (Element element in collection) {
                return element;
            }
            return default(Element?);
        }
#nullable disable

        // MARK: - Linq Passthrough

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Element[] ToArray<Element>(this IEnumerable<Element> collection) => System.Linq.Enumerable.ToArray(collection);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Count<Element>(this IEnumerable<Element> collection) => System.Linq.Enumerable.Count(collection);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Element> Unique<Element>(this IEnumerable<Element> collection) => System.Linq.Enumerable.Distinct(collection);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Element> Append<Element>(this IEnumerable<Element> collection, Element element) => System.Linq.Enumerable.Append(collection, element);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Element> Join<Element>(this IEnumerable<Element> lhs, IEnumerable<Element> rhs) => System.Linq.Enumerable.Concat(lhs, rhs);

        /// <summary>
        /// Returns a collection containing the results of mapping the given closure over the sequence’s elements.
        /// </summary>
        /// <param name="transform">A mapping closure. <paramref name="transform"/> accepts an element of this sequence as its parameter and returns a transformed value of the same or of a different type.</param>
        /// <returns>A collection containing the transformed elements of this sequence.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Result> Map<Element, Result>(this IEnumerable<Element> collection, Func<Element, Result> transform) => System.Linq.Enumerable.Select(collection, transform);

        /// <summary>
        /// Returns a collection containing, in order, the elements of the sequence that satisfy the given predicate.
        /// </summary>
        /// <param name="isIncluded">A closure that takes an element of the sequence as its argument and returns a Boolean value indicating whether the element should be included in the returned array.</param>
        /// <returns>A collection of the elements that isIncluded allowed.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Element> Filter<Element>(this IEnumerable<Element> collection, Func<Element, bool> isIncluded) => System.Linq.Enumerable.Where(collection, isIncluded);

        /// <summary>
        /// Returns a collection containing the concatenated results of calling the given transformation with each element of this sequence.
        /// </summary>
        /// <param name="transform">A closure that accepts an element of this sequence as its argument and returns a sequence or collection.</param>
        /// <returns>The resulting flattened collection.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Element> FlatMap<Source, Element>(this IEnumerable<Source> collection, Func<Source, IEnumerable<Element>> transform) => System.Linq.Enumerable.SelectMany(collection, transform);

        /// <summary>
        /// Returns true if all elements in a collection meet the condition, false otherwise.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool All<Element>(this IEnumerable<Element> collection, Func<Element, bool> condition) => System.Linq.Enumerable.All(collection, condition);

        /// <summary>
        /// Returns true if any element in a collection meets the condition, false otherwise.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Any<Element>(this IEnumerable<Element> collection, Func<Element, bool> condition) => System.Linq.Enumerable.Any(collection, condition);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<System.Linq.IGrouping<Key, Element>> Chunked<Element, Key>(this IEnumerable<Element> collection, Func<Element, Key> function) => System.Linq.Enumerable.GroupBy(collection, function);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<Element> ToList<Element>(this IEnumerable<Element> collection) => System.Linq.Enumerable.ToList(collection);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains<Element>(this IEnumerable<Element> collection, Element item) => System.Linq.Enumerable.Contains(collection, item);
    }
}