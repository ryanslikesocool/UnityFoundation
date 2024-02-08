using System;
using System.Collections;
using System.Collections.Generic;
using Random = Unity.Mathematics.Random;
using System.Runtime.CompilerServices;
using System.Linq.Expressions;

namespace Foundation {
	public static partial class Extensions {
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Range<int> Bounds<_>(this IList<_> collection)
			=> new Range<int>(0, collection.Count);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Indices Indices<_>(this IList<_> collection)
			=> new Indices(collection.Bounds());

		/// <summary>
		/// Fills all elements in an array with the provided value.
		/// </summary>
		/// <param name="collection">The collection to mutate.</param>
		/// <param name="value">The value that will be assigned to all indices in the collection.</param>
		public static List Fill<List, Element>(this List collection, Element value) where List : IList<Element> {
			for (int i = 0; i < collection.Count; i++) {
				collection[i] = value;
			}
			return collection;
		}

		/// <summary>
		/// Returns the index of the first element in a collection that matches the given predicate.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int? FirstIndex<Element>(this IList<Element> collection, Predicate<Element> condition) {
			for (int i = 0; i < collection.Count; i++) {
				if (condition(collection[i])) {
					return i;
				}
			}
			return null;
		}

		/// <summary>
		/// Returns the index of the last element in a collection that matches the given predicate.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int? LastIndex<Element>(this IList<Element> collection, Predicate<Element> condition) {
			for (int i = collection.Count - 1; i >= 0; i--) {
				if (condition(collection[i])) {
					return i;
				}
			}
			return null;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsEmpty<_>(this IList<_> collection) => collection.Count == 0;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Element> Reversed<Element>(this IList<Element> collection) {
			for (int i = collection.Count - 1; i >= 0; i--) {
				yield return collection[i];
			}
		}

		/// <summary>
		/// Returns a subset of the given collection in the range <c>lowerBounds ..< upperBound</c>.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Element> InRange<Element>(this IList<Element> collection, int lowerBound, int upperBound) {
			for (int i = lowerBound; i < upperBound; i++) {
				yield return collection[i];
			}
		}

#nullable enable
		/// <summary>
		/// Returns the first element in a collection.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Element? First<Element>(this IList<Element> collection) => collection.IsEmpty() ? default(Element?) : collection[0];

		/// <summary>
		/// Returns the first element in a collection that matches the given predicate.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Element? First<Element>(this IList<Element> collection, Predicate<Element> predicate) {
			if (collection.FirstIndex(predicate) is int index) {
				return collection[index];
			}
			return default(Element?);
		}

		/// <summary>
		/// Returns the last element in a collection.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Element? Last<Element>(this IList<Element> collection) => collection.IsEmpty() ? default(Element?) : collection[^1];

		/// <summary>
		/// Returns the last element in a collection that matches the given predicate.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Element? Last<Element>(this IList<Element> collection, Predicate<Element> predicate) {
			if (collection.LastIndex(predicate) is int index) {
				return collection[index];
			}
			return default(Element?);
		}

		/// <summary>
		/// Returns a random element in a collection.
		/// </summary>
		public static Element? Random<Element>(this IList<Element> collection) {
			if (collection.IsEmpty()) {
				return default(Element?);
			}
			int index = UnityEngine.Random.Range(0, collection.Count);
			return collection[index];
		}

		public static Element? Random<Element>(this IList<Element> collection, ref Random rng) {
			if (collection.IsEmpty()) {
				return default(Element?);
			}
			int index = rng.NextInt(collection.Count);
			return collection[index];
		}
#nullable disable
	}
}