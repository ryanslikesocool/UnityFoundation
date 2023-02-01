using System;

namespace Foundation {
    public static class Guard {
        internal class GuardClauseException : Exception {
            public GuardClauseException() { }
            public GuardClauseException(string message) : base(message) { }
            public GuardClauseException(string message, Exception inner) : base(message, inner) { }
        }

        public static void NotNull<Value>(in Value value, in string name) {
            if (!value.Equals((Value)default)) { return; }

            throw new GuardClauseException("Was expecting a non-null object.", new ArgumentNullException(name ?? nameof(value)));
        }

        public static void Require(bool condition, in string message) {
            if (condition) { return; }

            throw new GuardClauseException(message);
        }

        public static void Require(Func<bool> predicate, in string message) {
            if (predicate()) { return; }

            throw new GuardClauseException(message);
        }

        public static void NotImplemented() {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Unwrap an optional.
        /// </summary>
        /// <param name="source">The value to unwrap.</param>
        /// <param name="value">The unwrapped optional.  This may be <see langword="default"/>.</param>
        /// <returns><see langword="true"/> if the <paramref name="value"/> is not <see langword="null"/>; <see langword="false"/> otherwise.</returns>
        public static bool Let<Value>(Value? source, out Value value) where Value : struct {
            value = source.GetValueOrDefault();
            return source.HasValue;
        }
    }
}