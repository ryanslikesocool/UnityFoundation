using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace Foundation {
	public static partial class Extensions {
		/// <summary>
		/// Returns a sequence of pairs (n, x), where n represents a consecutive integer starting at zero and x represents an element of the sequence.
		/// </summary>
		[MethodImpl(AggressiveInlining)]
		public static IEnumerable<(int, Element)> Enumerated<Element>(this NativeArray<Element> collection) where Element : struct {
			for (int i = 0; i < collection.Length; i++) {
				yield return (i, collection[i]);
			}
		}

		/// <summary>
		/// Returns a subset of the given collection in the range lowerBounds ..< upperBound.
		/// </summary>
		[MethodImpl(AggressiveInlining)]
		public static IEnumerable<Element> SubSequence<Element>(this NativeArray<Element> collection, int lowerBound, int upperBound) where Element : struct {
			for (int i = lowerBound; i < upperBound; i++) {
				yield return collection[i];
			}
		}
	}
}