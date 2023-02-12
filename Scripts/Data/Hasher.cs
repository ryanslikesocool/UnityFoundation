using System;

namespace Foundation {
    public struct Hasher {
        private HashCode hashCode;

        public void Combine<H>(in H value) {
            hashCode.Add(value);
        }

        public int Finalize() => hashCode.ToHashCode();
    }
}