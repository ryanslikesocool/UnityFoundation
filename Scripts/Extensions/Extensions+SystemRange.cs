using System;
using System.Collections;

namespace Foundation {
    public static partial class Extensions {
        public static IEnumerator GetEnumerator(this System.Range range)
            => new Indices(range).GetEnumerator();
    }
}