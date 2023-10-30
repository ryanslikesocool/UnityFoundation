using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Foundation {
	public partial class Extensions {
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddChild(this GameObject parent, GameObject child)
			=> child.transform.parent = parent.transform;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddChildren(this GameObject parent, in IEnumerable<GameObject> children)
			=> children.ForEach(child => AddChild(parent, child));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RemoveFromParent(this GameObject child)
			=> child.transform.parent = null;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasParent(this GameObject child, in Func<GameObject, bool> predicate) {
			GameObject parent = child.transform.parent.gameObject;
			return predicate(parent) || parent.HasParent(predicate);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasParent(this GameObject child, GameObject parent)
			=> child.HasParent(obj => obj == parent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static GameObject NearestParent(this GameObject child, in Func<GameObject, bool> predicate) {
			GameObject parent = child.transform.parent.gameObject;
			if (predicate(parent)) {
				return parent;
			} else {
				return parent.NearestParent(predicate);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasComponent<T>(this GameObject gameObject) where T : Component
			=> gameObject.TryGetComponent(out T _);
	}
}