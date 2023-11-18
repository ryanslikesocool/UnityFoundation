using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Foundation {
	public static partial class Extensions {
		// MARK: - Filter

		/*
        /// <summary>
        /// Adds the elements of a sequence to the end of the array.
        /// </summary>
        /// <param name="newElements">The elements to append to the array.</param>
        public static Element[] Append<Element>(this Element[] collection, params Element[] newElements)
            => Append(collection, newElements);
        */

		/// <summary>
		/// Adds the elements of a sequence to the end of the array.
		/// </summary>
		/// <param name="newElements">The elements to append to the array.</param>
		public static void Append<Element>(this Element[] collection, Element[] newElements) => collection = Appending(collection, newElements);

		public static Element[] Appending<Element>(this Element[] collection, Element[] newElements) {
			int lowerBound = collection.Length;
			int upperBound = collection.Length + newElements.Length;

			Element[] result = new Element[upperBound];

			for (int i = 0; i < lowerBound; i++) {
				result[i] = collection[i];
			}
			for (int i = lowerBound; i < upperBound; i++) {
				result[i] = newElements[i - lowerBound];
			}

			return result;
		}

		/// <summary>
		/// Returns an array containing the elements of this sequence in reverse order.
		/// </summary>
		/// <returns>An array containing the elements of this sequence in reverse order.</returns>
		public static Element[] Reversed<Element>(this Element[] collection) {
			Array.Reverse(collection);
			return collection;
		}

		/// <summary>
		/// Reverses an array inline.
		/// </summary>
		/// <param name="collection">The array to reverse</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Reverse<Element>(this Element[] collection)
			=> Array.Reverse(collection);

		/// <summary>
		/// Inserts an element into an array at an index.
		/// </summary>
		/// <param name="collection">The collection to mutate.</param>
		/// <param name="newElement">The element to insert into the collection.</param>
		/// <param name="index">The new element's index.</param>
		public static Element[] Insert<Element>(this Element[] collection, Element newElement, int index) {
			Array.Resize(ref collection, collection.Length + 1);

			for (int i = collection.Length - 2; i >= index; i--) {
				collection[i + 1] = collection[i];
			}
			collection[index] = newElement;

			return collection;
		}

		/// <summary>
		/// Removes an element from an array at an index.
		/// </summary>
		/// <param name="collection">The collection to mutate.</param>
		/// <param name="index">The index of the element to remove.</param>
		public static Element[] RemoveAt<Element>(this Element[] collection, int index) {
			for (int i = index + 1; i < collection.Length; i++) {
				collection[i - 1] = collection[i];
			}
			Array.Resize(ref collection, collection.Length - 1);
			return collection;
		}

		/// <summary>
		/// Removes the first element in an array.
		/// </summary>
		/// <param name="collection">The collection to mutate.</param>
		public static Element[] DropFirst<Element>(this Element[] collection)
			=> collection.RemoveAt(0);

		/// <summary>
		/// Removes the last element in an array.
		/// </summary>
		public static Element[] DropLast<Element>(this Element[] collection)
			=> collection.RemoveAt(collection.Length - 1);

		/// <summary>
		/// Performs a shallow copy of an array.
		/// </summary>
		/// <param name="collection">The collection to perform a shallow copy on.</param>
		public static Element[] ShallowCopy<Element>(this Element[] collection) {
			Element[] result = new Element[collection.Length];
			Array.Copy(collection, result, collection.Length);
			return result;
		}
	}
}