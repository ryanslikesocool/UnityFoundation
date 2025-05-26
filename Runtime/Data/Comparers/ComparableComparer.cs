using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Foundation {
	public readonly struct ComparableComparer<Compared> : IComparer<Compared> where Compared : IComparable<Compared> {
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly int Compare(Compared lhs, Compared rhs)
			=> lhs.CompareTo(rhs);
	}
}