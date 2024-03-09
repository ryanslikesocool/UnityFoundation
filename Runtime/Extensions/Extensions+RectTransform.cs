using System;
using UnityEngine;

namespace Foundation {
	public static partial class Extensions {
		public static void SetInsetFromParentEdge(this RectTransform rectTransform, RectTransform.Edge edge, float inset) {
			float size = edge switch {
				RectTransform.Edge.Left => rectTransform.rect.width,
				RectTransform.Edge.Right => rectTransform.rect.width,
				RectTransform.Edge.Bottom => rectTransform.rect.height,
				RectTransform.Edge.Top => rectTransform.rect.height,
				_ => throw new ArgumentException()
			};
			rectTransform.SetInsetAndSizeFromParentEdge(edge, inset, size);
		}

		public static void SetSizeWithCurrentAnchors(this RectTransform rectTransform, Vector2 size) {
			rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
			rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y);
		}
	}
}