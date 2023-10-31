using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Foundation {
	public static partial class Extensions {
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float ValueAtEdge(this in Rect rect, RectTransform.Edge edge) => edge switch {
			RectTransform.Edge.Left => rect.xMin,
			RectTransform.Edge.Right => rect.xMax,
			RectTransform.Edge.Bottom => rect.yMin,
			RectTransform.Edge.Top => rect.yMax,
			_ => throw new ArgumentException()
		};

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float ValueAtEdge(this RectTransform rectTransform, RectTransform.Edge edge)
			=> rectTransform.rect.ValueAtEdge(edge);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Sign(this RectTransform.Edge edge) => edge switch {
			RectTransform.Edge.Left => -1,
			RectTransform.Edge.Right => 1,
			RectTransform.Edge.Bottom => -1,
			RectTransform.Edge.Top => 1,
			_ => throw new ArgumentException()
		};

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Vector(this RectTransform.Edge edge, float length) => edge switch {
			RectTransform.Edge.Left => Vector2.left * length,
			RectTransform.Edge.Right => Vector2.right * length,
			RectTransform.Edge.Bottom => Vector2.down * length,
			RectTransform.Edge.Top => Vector2.up * length,
			_ => throw new ArgumentException()
		};

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 OffsetValueAtEdge(this RectTransform rectTransform, RectTransform.Edge edge, float length) {
			float value = ValueAtEdge(rectTransform, edge) - length;
			return edge.Vector(value);
		}
	}
}