using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Foundation {
	public readonly struct PropertyComparer<Compared, Value> : IComparer<Compared> {
		private readonly IComparer<Value> innerComparer;
		private readonly Func<Compared, Value> selector;

		public PropertyComparer(
			in IComparer<Value> innerComparer,
			Func<Compared, Value> selector
		) {
			this.innerComparer = innerComparer;
			this.selector = selector;
		}

		// doesn't compile because microsoft hates me, specifically :(
		//public PropertyComparator(
		//	SortOrder sortOrder = SortOrder.Forward,
		//	Func<Compared, Value> selector
		//) where Value: IComparable<Value> {
		//	this.innerComparator = new ComparableComparator<Value>(sortOrder);
		//	this.selector = selector;
		//}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly int Compare(Compared lhs, Compared rhs)
			=> innerComparer.Compare(
				selector(lhs),
				selector(rhs)
			);
	}
}