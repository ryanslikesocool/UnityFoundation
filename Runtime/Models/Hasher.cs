namespace Foundation {
	public struct Hasher {
		private int hash;

		public Hasher(uint hash = 2166136261) {
			unchecked {
				this.hash = (int)hash;
			}
		}

		public void Combine<T>(T value) {
			hash = (hash * 16777619) ^ value.GetHashCode();
		}

		public readonly int Finalize() {
			return hash;
		}
	}
}