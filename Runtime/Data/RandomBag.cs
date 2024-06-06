using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace Foundation {
	public struct RandomBag<Element> : IList<Element> {
		private List<Element> _backing;

		public readonly int Count => _backing.Count;
		public readonly bool IsEmpty => _backing.IsEmpty();

		public readonly bool IsReadOnly => false;

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

		[MethodImpl(AggressiveInlining)]
		public void Append(IEnumerable<Element> other)
			=> _backing.Append(other);

		[MethodImpl(AggressiveInlining)]
		public void Append(Element item)
			=> _backing.Append(item);

		// MARK: - IList

		public Element this[int index] {
			readonly get => _backing[index];
			set => _backing[index] = value;
		}

		[MethodImpl(AggressiveInlining)]
		public readonly int IndexOf(Element item)
			=> _backing.IndexOf(item);

		[MethodImpl(AggressiveInlining)]
		public void Insert(int index, Element item)
			=> _backing.Insert(index, item);

		[MethodImpl(AggressiveInlining)]
		public void RemoveAt(int index)
			=> _backing.RemoveAt(index);

		[MethodImpl(AggressiveInlining)]
		public void Add(Element item)
			=> _backing.Add(item);

		[MethodImpl(AggressiveInlining)]
		public void Clear()
			=> _backing.Clear();

		[MethodImpl(AggressiveInlining)]
		public bool Contains(Element item)
			=> _backing.Contains(item);

		[MethodImpl(AggressiveInlining)]
		public readonly void CopyTo(Element[] collection, int index)
			=> _backing.CopyTo(collection, index);

		[MethodImpl(AggressiveInlining)]
		public bool Remove(Element item)
			=> _backing.Remove(item);

		[MethodImpl(AggressiveInlining)]
		public readonly IEnumerator<Element> GetEnumerator()
			=> _backing.GetEnumerator();

		[MethodImpl(AggressiveInlining)]
		readonly IEnumerator IEnumerable.GetEnumerator()
			=> _backing.GetEnumerator();
	}
}