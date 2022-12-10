using System;
using System.Collections;

namespace Foundation {
    /// <summary>
    /// A half-open interval from a lower bound up to, but not including, an upper bound.
    /// </summary>
    [Serializable]
    public readonly struct Range<Bound> : CustomStringConvertible where Bound : IComparable<Bound>, IEquatable<Bound> {
        public readonly Bound lowerBound;
        public readonly Bound upperBound;

        //public bool isEmpty => lowerBound.Equals(upperBound);

        public string description => $"Range<{typeof(Bound)}>({lowerBound.ToString()} ..< {upperBound.ToString()})";

        public Range(in Bound lowerBound, in Bound upperBound) {
            this.lowerBound = lowerBound;
            this.upperBound = upperBound;
        }

        public bool Contains(in Bound value) => (value.CompareTo(lowerBound) >= 0) && (value.CompareTo(upperBound) < 0);

        public bool Contains(in Range<Bound> other) => (other.lowerBound.CompareTo(lowerBound) >= 0) && (other.upperBound.CompareTo(upperBound) <= 0);

        public bool Overlaps(in Range<Bound> other) {
            bool lower = Contains(other.lowerBound) || other.Contains(lowerBound);
            bool upper = Contains(other.upperBound) || other.Contains(upperBound);
            return lower || upper;
        }

        public Range<Bound> ClampedTo(in Range<Bound> other) {
            Bound lower = lowerBound.CompareTo(other.lowerBound) >= 0 ? lowerBound : other.lowerBound;
            Bound upper = upperBound.CompareTo(other.upperBound) <= 0 ? upperBound : other.upperBound;
            return new Range<Bound>(lower, upper);
        }

        public static Range<int> Create(in System.Range range) => new Range<int>(
            range.Start.Value,
            range.End.Value
        );
        //public static Range<Bound> operator ..<(Bound lowerBound, Bound upperBound) => new Range(lowerBound, upperBound);
    }

    public static partial class Extensions {
        public static System.Range SystemRange(this Range<int> range) => new System.Range(new Index(range.lowerBound), new Index(range.upperBound));

        public static IEnumerator GetEnumerator(this Range<int> range) => new IntRangeEnumerator(range);
    }

    internal class IntRangeEnumerator : IEnumerator {
        public Range<int> _range;

        private int position;

        public IntRangeEnumerator(Range<int> range) {
            _range = range;
            position = range.lowerBound - 1;
        }

        public bool MoveNext() {
            position++;
            return (position < _range.upperBound);
        }

        public void Reset() {
            position = _range.lowerBound - 1;
        }

        object IEnumerator.Current => Current;

        public int Current => position;
    }
}