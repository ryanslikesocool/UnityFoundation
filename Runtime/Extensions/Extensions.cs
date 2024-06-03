using System;
using System.Runtime.CompilerServices;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace Foundation {
	public static partial class Extensions {
		public delegate void ActionRef<T>(ref T arg1);

		[MethodImpl(AggressiveInlining)]
		public static bool ValueChanged<T>(ref T original, T newValue) {
			if (!original.Equals(newValue)) {
				original = newValue;
				return true;
			} else {
				return false;
			}
		}
	}
}