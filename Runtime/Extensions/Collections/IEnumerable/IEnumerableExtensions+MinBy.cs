using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Foundation {
	public static partial class IEnumerableExtensions {
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Element MinBy<Element>(
			this IEnumerable<Element> collection,
			IComparer<Element> comparer
		) => collection.Aggregate((lhs, rhs) => {
			if (comparer.Compare(lhs, rhs) < 0) {
				return lhs;
			} else {
				return rhs;
			}
		});

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Element MinBy<Element, Value>(
			this IEnumerable<Element> collection,
			Func<Element, Value> selector,
			IComparer<Value> comparer
		) => collection.MinBy(
			new PropertyComparer<Element, Value>(
				comparer,
				selector
			)
		);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Element MinBy<Element, Value>(
			this IEnumerable<Element> collection,
			Func<Element, Value> selector
		) where Value : IComparable<Value> => collection.MinBy(
			selector,
			new ComparableComparer<Value>()
		);
	}
}