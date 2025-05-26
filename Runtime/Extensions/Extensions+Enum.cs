using System;
using System.Runtime.CompilerServices;

namespace Foundation {
	public static partial class Extensions {
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsEmpty<E>(this E value) where E : Enum
			=> value.Equals(default(E));
	}
}