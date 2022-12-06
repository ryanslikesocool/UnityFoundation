/*
using System;

namespace Foundation {
    [Serializable]
    public struct ClosedRange<Bound> : CustomStringConvertible where Bound : IComparable<Bound>, IEquatable<Bound> {
        public readonly Bound lowerBound;
        public readonly Bound upperBound;

        //public bool isEmpty => lowerBound.Equals(upperBound);

        public string description => $"ClosedRange<{typeof(Bound)}>({lowerBound.ToString()} ... {upperBound.ToString()})";

        public ClosedRange(in Bound lowerBound, in Bound upperBound) {
            this.lowerBound = lowerBound;
            this.upperBound = upperBound;
        }

        public bool Contains(in Bound value) => (value.CompareTo(lowerBound) >= 0) && (value.CompareTo(upperBound) <= 0);

        public bool Contains(in ClosedRange<Bound> other) => (other.lowerBound.CompareTo(lowerBound) >= 0) && (other.upperBound.CompareTo(upperBound) <= 0);

        public bool Overlaps(in ClosedRange<Bound> other) {
            bool lower = Contains(other.lowerBound) || other.Contains(lowerBound);
            bool upper = Contains(other.upperBound) || other.Contains(upperBound);
            return lower || upper;
        }

        public ClosedRange<Bound> ClampedTo(in ClosedRange<Bound> other) {
            Bound lower = lowerBound.CompareTo(other.lowerBound) >= 0 ? lowerBound : other.lowerBound;
            Bound upper = upperBound.CompareTo(other.upperBound) <= 0 ? upperBound : other.upperBound;
            return new ClosedRange<Bound>(lower, upper);
        }

        //public static ClosedRange<Bound> operator ...(Bound lowerBound, Bound upperBound) => new ClosedRange(lowerBound, upperBound);
    }
}
*/