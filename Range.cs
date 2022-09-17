using System;

namespace Foundation {
    [Serializable]
    public struct Range<Bound> : CustomStringConvertible where Bound : IComparable<Bound>, IEquatable<Bound> {
        public readonly Bound lowerBound;
        public readonly Bound upperBound;

        //public bool isEmpty => lowerBound.Equals(upperBound);

        public string description => $"Range<{typeof(Bound)}>({lowerBound.ToString()} ..< {upperBound.ToString()})";

        public Range(Bound lowerBound, Bound upperBound) {
            this.lowerBound = lowerBound;
            this.upperBound = upperBound;
        }

        public bool Contains(Bound value) => (value.CompareTo(lowerBound) >= 0) && (value.CompareTo(upperBound) < 0);

        public bool Contains(Range<Bound> other) => (other.lowerBound.CompareTo(lowerBound) >= 0) && (other.upperBound.CompareTo(upperBound) <= 0);

        public bool Overlaps(Range<Bound> other) {
            bool lower = Contains(other.lowerBound) || other.Contains(lowerBound);
            bool upper = Contains(other.upperBound) || other.Contains(upperBound);
            return lower || upper;
        }

        public Range<Bound> ClampedTo(Range<Bound> other) {
            Bound lower = lowerBound.CompareTo(other.lowerBound) >= 0 ? lowerBound : other.lowerBound;
            Bound upper = upperBound.CompareTo(other.upperBound) <= 0 ? upperBound : other.upperBound;
            return new Range<Bound>(lower, upper);
        }

        //public static Range<Bound> operator ..<(Bound lowerBound, Bound upperBound) => new Range(lowerBound, upperBound);
    }
}