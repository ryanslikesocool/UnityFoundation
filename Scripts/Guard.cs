using System;
using UnityEngine;

namespace Foundation {
    public static class Guard {
        internal class GuardClauseException : Exception {
            public GuardClauseException() { }
            public GuardClauseException(string message) : base(message) { }
            public GuardClauseException(string message, Exception inner) : base(message, inner) { }
        }

        public static void NotNull(object value, string name) {
            if (value != null) { return; }

            throw new GuardClauseException(string.Empty, new ArgumentNullException(name ?? nameof(value)));
        }

        public static void Require(Func<bool> predicate, string message) {
            if (predicate()) { return; }

            throw new GuardClauseException(message);
        }
    }
}