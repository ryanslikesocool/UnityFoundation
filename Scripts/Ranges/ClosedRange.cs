using System;
using System.Collections;

namespace Foundation {
    /// <summary>
    /// An interval from a lower bound up to, and including, an upper bound.
    /// </summary>
    [Serializable]
    public struct ClosedRange<Bound> : IEquatable<ClosedRange<Bound>>, ICustomStringConvertible where Bound : IComparable<Bound>, IEquatable<Bound> {
        /// <summary>
        /// The range’s lower bound.
        /// </summary>
        public Bound lowerBound;

        /// <summary>
        /// The range’s upper bound.
        /// </summary>
        public Bound upperBound;

        //public bool isEmpty => lowerBound.Equals(upperBound);

        public string description => $"ClosedRange<{typeof(Bound)}>({lowerBound.ToString()} ... {upperBound.ToString()})";

        public ClosedRange(in Bound lowerBound, in Bound upperBound) {
            this.lowerBound = lowerBound;
            this.upperBound = upperBound;
        }

        /// <summary>
        /// Returns a <see langword="bool"/> value indicating whether the given element is contained within the range.
        /// </summary>
        /// <remarks>
        /// A <see cref="ClosedRange"/> instance contains both its lower and upper bound. element is contained in the range if it is between the two bounds or equal to either bound.
        /// </remarks>
        /// <param name="element">The element to check for containment.</param>
        /// <returns><see langword="true"/> if <paramref name="element"/> is contained in the range; otherwise, <see langword="false"/>.</returns>
        public bool Contains(in Bound element) => (element.CompareTo(lowerBound) >= 0) && (element.CompareTo(upperBound) <= 0);

        public bool Contains(in ClosedRange<Bound> other) => (other.lowerBound.CompareTo(lowerBound) >= 0) && (other.upperBound.CompareTo(upperBound) <= 0);

        /// <summary>
        /// Returns a <see langword="bool"/> value indicating whether this range and the given range contain an element in common.
        /// </summary>
        /// <param name="other">A range to check for elements in common.</param>
        /// <returns><see langword="true"/> if this range and <paramref name="other"/> have at least one element in common; otherwise, <see langword="false"/>.</returns>
        public bool Overlaps(in ClosedRange<Bound> other) {
            bool lower = Contains(other.lowerBound) || other.Contains(lowerBound);
            bool upper = Contains(other.upperBound) || other.Contains(upperBound);
            return lower || upper;
        }

        /// <summary>
        /// Returns a copy of this range clamped to the given limiting range.
        /// </summary>
        /// <param name="limits">The range to clamp the bounds of this range.</param>
        /// <returns>A new range clamped to the bounds of <paramref name="limits"/>.</returns>
        public ClosedRange<Bound> ClampedTo(in ClosedRange<Bound> limits) {
            Bound lower = lowerBound.CompareTo(limits.lowerBound) >= 0 ? lowerBound : limits.lowerBound;
            Bound upper = upperBound.CompareTo(limits.upperBound) <= 0 ? upperBound : limits.upperBound;
            return new ClosedRange<Bound>(lower, upper);
        }

        /// <summary>
        /// Returns the given element clamped to this range.
        /// </summary>
        public Bound Clamping(in Bound element) {
            if (element.CompareTo(lowerBound) < 0) {
                return lowerBound;
            } else if (element.CompareTo(upperBound) > 0) {
                return upperBound;
            } else {
                return element;
            }
        }

        public bool Equals(ClosedRange<Bound> other) => lowerBound.Equals(other.lowerBound) && upperBound.Equals(other.upperBound);
        //public static ClosedRange<Bound> operator ...(Bound lowerBound, Bound upperBound) => new ClosedRange(lowerBound, upperBound);
    }

    public static partial class Extensions {
        public static System.Range ConvertToSystemRange(this ClosedRange<int> range) => new System.Range(new Index(range.lowerBound), new Index(range.upperBound + 1));
    }
}