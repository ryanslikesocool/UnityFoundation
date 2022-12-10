using System;
using System.Collections;

namespace Foundation {
    public static partial class Extensions {
        public static IEnumerator GetEnumerator(this System.Range range) => new SystemRangeEnumerator(range);
    }

    internal class SystemRangeEnumerator : IEnumerator {
        public System.Range _range;

        private int position;

        public SystemRangeEnumerator(System.Range range) {
            _range = range;
            position = _range.Start.Value - 1;
        }

        public bool MoveNext() {
            position++;
            return (position < _range.End.Value);
        }

        public void Reset() {
            position = _range.Start.Value - 1;
        }

        object IEnumerator.Current => Current;

        public int Current => position;
    }
}