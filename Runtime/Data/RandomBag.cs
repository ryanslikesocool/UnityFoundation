using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Foundation {
	public struct RandomBag<Element> : IList<Element> {
		private List<Element> _backing;

		public int Count => _backing.Count;
		public bool IsEmpty => _backing.IsEmpty();

		public bool IsReadOnly => false;

		public RandomBag(List<Element> values) {
			_backing = values;
		}

		public RandomBag(IEnumerable<Element> values) : this(values.ToList()) { }

		public Element Pull() {
			int index = UnityEngine.Random.Range(0, Count);
			return Pull(index);
		}

		public Element Pull(ref Unity.Mathematics.Random random) {
			int index = random.NextInt(0, Count - 1);
			return Pull(index);
		}

		public Element Pull(int index) {
			Element result = _backing[index];
			_backing.RemoveAt(index);
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)] public void Append(IEnumerable<Element> other) => _backing.Append(other);

		[MethodImpl(MethodImplOptions.AggressiveInlining)] public void Append(Element item) => _backing.Append(item);

		// MARK: - IList

		public Element this[int index] {
			get => _backing[index];
			set => _backing[index] = value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)] public int IndexOf(Element item) => _backing.IndexOf(item);

		[MethodImpl(MethodImplOptions.AggressiveInlining)] public void Insert(int index, Element item) => _backing.Insert(index, item);

		[MethodImpl(MethodImplOptions.AggressiveInlining)] public void RemoveAt(int index) => _backing.RemoveAt(index);

		[MethodImpl(MethodImplOptions.AggressiveInlining)] public void Add(Element item) => _backing.Add(item);

		[MethodImpl(MethodImplOptions.AggressiveInlining)] public void Clear() => _backing.Clear();

		[MethodImpl(MethodImplOptions.AggressiveInlining)] public bool Contains(Element item) => _backing.Contains(item);

		[MethodImpl(MethodImplOptions.AggressiveInlining)] public void CopyTo(Element[] collection, int index) => _backing.CopyTo(collection, index);

		[MethodImpl(MethodImplOptions.AggressiveInlining)] public bool Remove(Element item) => _backing.Remove(item);

		[MethodImpl(MethodImplOptions.AggressiveInlining)] public IEnumerator<Element> GetEnumerator() => _backing.GetEnumerator();

		[MethodImpl(MethodImplOptions.AggressiveInlining)] IEnumerator IEnumerable.GetEnumerator() => _backing.GetEnumerator();
	}
}