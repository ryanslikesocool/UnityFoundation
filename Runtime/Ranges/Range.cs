using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace Foundation {
	/// <summary>
	/// A half-open interval from a lower bound up to, but not including, an upper bound.
	/// </summary>
	[Serializable]
	public struct Range<Bound> : IEquatable<Range<Bound>> where Bound : IComparable<Bound> {//, IEquatable<Bound> {
		/// <summary>
		/// The range’s lower bound.
		/// </summary>
		public Bound lowerBound;

		/// <summary>
		/// The range’s upper bound.
		/// </summary>
		public Bound upperBound;

		//public bool isEmpty => lowerBound.Equals(upperBound);

		public Range(in Bound lowerBound, in Bound upperBound) {
			this.lowerBound = lowerBound;
			this.upperBound = upperBound;
		}

		/// <summary>
		/// Returns a <see langword="bool"/> value indicating whether the given element is contained within the range.
		/// </summary>
		/// <remarks>
		/// Because <see cref="Range"/> represents a half-open range, a <see cref="Range"/> instance does not contain its upper bound. element is contained in the range if it is greater than or equal to the lower bound and less than the upper bound.
		/// </remarks>
		/// <param name="value">The element to check for containment.</param>
		/// <returns><see langword="true"/> if <paramref name="element"/> is contained in the range; otherwise, <see langword="false"/>.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool Contains(in Bound element) => lowerBound.CompareTo(element) <= 0 && upperBound.CompareTo(element) > 0;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool Contains(in Range<Bound> other) => lowerBound.CompareTo(other.lowerBound) <= 0 && upperBound.CompareTo(other.upperBound) >= 0;

		/// <summary>
		/// Returns a <see langword="bool"/> value indicating whether this range and the given range contain an element in common.
		/// </summary>
		/// <param name="other">A range to check for elements in common.</param>
		/// <returns><see langword="true"/> if this range and <paramref name="other"/> have at least one element in common; otherwise, <see langword="false"/>.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool Overlaps(in Range<Bound> other) {
			bool lower = Contains(other.lowerBound) || other.Contains(lowerBound);
			bool upper = Contains(other.upperBound) || other.Contains(upperBound);
			return lower || upper;
		}

		/// <summary>
		/// Returns a copy of this range clamped to the given limiting range.
		/// </summary>
		/// <param name="limits">The range to clamp the bounds of this range.</param>
		/// <returns>A new range clamped to the bounds of <paramref name="limits"/>.</returns>
		public readonly Range<Bound> ClampedTo(in Range<Bound> limits) {
			Bound lower = lowerBound.CompareTo(limits.lowerBound) >= 0 ? lowerBound : limits.lowerBound;
			Bound upper = upperBound.CompareTo(limits.upperBound) <= 0 ? upperBound : limits.upperBound;
			return new Range<Bound>(lower, upper);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Range<int> Create(in System.Range range) => new Range<int>(
			range.Start.Value,
			range.End.Value
		);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool Equals(Range<Bound> other) => lowerBound.Equals(other.lowerBound) && upperBound.Equals(other.upperBound);
		//public static Range<Bound> operator ..<(Bound lowerBound, Bound upperBound) => new Range(lowerBound, upperBound);

		public override readonly string ToString() => $"Range<{typeof(Bound)}>({lowerBound.ToString()} ..< {upperBound.ToString()})";
	}

	public static partial class Extensions {
		public static System.Range ConvertToSystemRange(this Range<int> range) => new System.Range(new Index(range.lowerBound), new Index(range.upperBound));
	}
}