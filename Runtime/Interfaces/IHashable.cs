using UnityEngine;

namespace Foundation {
	public interface IHashable {
		void Hash(ref Hasher hasher);

		int GetHashCode();
	}
}