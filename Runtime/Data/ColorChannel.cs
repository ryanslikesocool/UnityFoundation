using System;
using UnityEngine;

namespace Foundation {
	[Flags]
	public enum ColorChannel : byte {
		None = 0,

		Red = 1 << 0,
		Green = 1 << 1,
		Blue = 1 << 2,
		Alpha = 1 << 3,

		R = Red,
		G = Green,
		B = Blue,
		A = Alpha,

		RGB = Red | Green | Blue,
		RGBA = RGB | Alpha,
	}
}