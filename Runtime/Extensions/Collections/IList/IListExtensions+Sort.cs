using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Foundation {
	public static partial class IListExtensions {
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Sort<Element>(this IList<Element> collection) where Element : IComparable<Element>
			=> collection.OrderBy(element => element);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SortBy<Element>(
			this IList<Element> collection,
			IComparer<Element> comparer
		) => collection.OrderBy(element => element, comparer);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SortBy<Element, Value>(
			this IList<Element> collection,
			Func<Element, Value> selector,
			IComparer<Value> comparer
		) where Value : IComparable<Value> => collection.OrderBy(selector, comparer);
	}
}