using UnityEngine;

namespace Foundation {
	public interface ICopyable<T> {
		T Copy();
	}
}