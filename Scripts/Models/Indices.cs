using System;
using System.Collections;
using Unity.Mathematics;

namespace Foundation {
    public readonly struct Indices : IEnumerable {
        public readonly int start;
        public readonly int end;
        public readonly Direction direction;

        public int count => math.abs(end - start);

        public bool isEmpty => start == end;

        public enum Direction {
            Forwards = 1,
            Backwards = -1
        }

        public Indices(int start, int end, Direction? direction = null) {
            this.start = start;
            this.end = end;
            this.direction = direction ?? (start < end ? Direction.Forwards : Direction.Backwards);
        }

        public Indices(in Range<int> bounds) : this(bounds.lowerBound, bounds.upperBound - 1) { }

        public Indices(in System.Range bounds) : this(Range<int>.Create(bounds)) { }

        public Indices(in ClosedRange<int> bounds) : this(bounds.lowerBound, bounds.upperBound) { }

        public Indices DropFirst(int count = 1)
            => new Indices(
                start + (int)direction * count,
                end
            );

        public Indices DropLast(int count = 1)
            => new Indices(
                start,
                end - (int)direction * count
            );

        public Indices Reversed()
            => new Indices(
                start,
                end,
                direction == Direction.Forwards ? Direction.Backwards : Direction.Forwards
            );

        public IEnumerator GetEnumerator()
            => new IndicesEnumerator(this);
    }

    internal class IndicesEnumerator : IEnumerator {
        private Indices _indices;

        private int position;

        public IndicesEnumerator(Indices indices) {
            _indices = indices;
            Reset();
            //position = indices.lowerBound - 1;
        }

        public bool MoveNext() {
            position += (int)_indices.direction;
            return position != _indices.end;
        }

        public void Reset() {
            position = _indices.start - (int)_indices.direction;
        }

        object IEnumerator.Current => Current;

        public int Current => position;
    }
}