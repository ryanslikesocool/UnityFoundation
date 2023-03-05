using System;
using UnityEngine;

namespace Foundation {
    /// <summary>
    /// A type that represents either a wrapped value or the absence of a value.
    /// </summary>
    [Serializable]
    public struct Optional<Value> : IMutablePropertyWrapper<Value>, IEquatable<Optional<Value>>, IEquatable<Value> where Value : struct {
        [SerializeField] private Value _value;
        [SerializeField] private bool _hasValue;

        public Value wrappedValue {
            get {
                if (!HasValue) {
                    throw new System.InvalidOperationException("Serializable nullable object must have a value.");
                }
                return _value;
            }
            set {
                _hasValue = true;
                _value = value;
            }
        }

        /// <summary>
        /// Does the wrapper's underlying value exist?
        /// </summary>
        public bool HasValue => _hasValue;

        public Optional(bool hasValue, Value value) {
            this._value = value;
            this._hasValue = hasValue;
        }

        private Optional(Value value) {
            this._value = value;
            this._hasValue = true;
        }

        /// <summary>
        /// Attempt to retrieve the underlying value.
        /// </summary>
        /// <param name="value">The underlying value, if it exists; <see langword="default"/> otherwise.</param>
        /// <returns><see langword="true"/> if the underlying value exists; <see langword="false"/> otherwise.</returns>
        public bool TryGetValue(out Value value) {
            value = this._value;
            return HasValue;
        }

        public Value? System {
            get {
                if (!HasValue) {
                    return null;
                } else {
                    return _value;
                }
            }
        }

        public static implicit operator Optional<Value>(Value value)
            => new Optional<Value>(value);

        public static implicit operator Optional<Value>(System.Nullable<Value> value)
            => value.HasValue ? new Optional<Value>(value.Value) : new Optional<Value>();

        public static implicit operator System.Nullable<Value>(Optional<Value> value)
            => value.HasValue ? (Value?)value.wrappedValue : null;

        public override int GetHashCode() => (_hasValue, _value).GetHashCode();

        public override bool Equals(object obj) {
            switch (obj) {
                case Optional<Value> other:
                    return this.Equals(other);
                case Value other:
                    return this.Equals(other);
                case null:
                    return !_hasValue;
                default:
                    return false;
            }
        }

        public bool Equals(Optional<Value> other) {
            if (_hasValue == other._hasValue) {
                return _hasValue ? _value.GetHashCode() == other._value.GetHashCode() : true;
            } else {
                return false;
            }
        }

        public bool Equals(Value other) {
            if (!HasValue) { return false; }
            return _value.GetHashCode() == other.GetHashCode();
        }
    }
}