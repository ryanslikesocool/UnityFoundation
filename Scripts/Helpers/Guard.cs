using System;
using System.Runtime.CompilerServices;

namespace Foundation {
    public static class Guard {
        internal class GuardClauseException : Exception {
            public GuardClauseException() { }
            public GuardClauseException(string message) : base(message) { }
            public GuardClauseException(string message, Exception inner) : base(message, inner) { }
        }

        public static void NotNullThrow<Value>(in Value value, in string name) {
            if (!value.Equals((Value)default)) { return; }

            throw new GuardClauseException("Was expecting a non-null object.", new ArgumentNullException(name ?? nameof(value)));
        }

        public static void RequireThrow(bool condition, in string message) {
            if (condition) { return; }

            throw new GuardClauseException(message);
        }

        public static void RequireThrow(Func<bool> predicate, in string message) {
            if (predicate()) { return; }

            throw new GuardClauseException(message);
        }

        public static void NotImplemented() {
            throw new NotImplementedException();
        }

        // MARK: - Validation

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NotNull<Value>(in Value value)
            => !value.Equals((Value)default);

        /// <summary>
        /// A set of guards that unwrap an optional.
        /// </summary>
        public static class Unwrap {
            public static bool Let<Value>(Value? source, out Value value) where Value : struct {
                value = source.GetValueOrDefault();
                return source.HasValue;
            }

            public static bool LetElse<Value>(Value? source, out Value value) where Value : struct
                => !Let(source, out value);

            public static bool Let<Value>(Value source, out Value value) where Value : class {
                value = source;
                return source != null;
            }

            public static bool LetElse<Value>(Value source, out Value value) where Value : class
                => !Let(source, out value);
        }

        /// <summary>
        /// A set of <see cref="bool"/>-returning guards that require a condition to be either <see langword="true"/> or <see langword="false"/>
        /// </summary>
        public static class Require {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool True(bool condition)
                => condition;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool False(bool condition)
                => !condition;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool True(Func<bool> condition)
                => condition();

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool False(Func<bool> condition)
                => !condition();
        }
    }
}