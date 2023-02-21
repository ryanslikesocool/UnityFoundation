using System;
using UnityEngine;

namespace Foundation {
    /// <summary>
    /// A type that represents either a wrapped value or the absence of a value.
    /// </summary>
    [Serializable]
    public struct Optional<Value> : IMutablePropertyWrapper<Value> where Value : struct {
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
        public bool HasValue => _hasValue;

        public Optional(bool hasValue, Value v) {
            this._value = v;
            this._hasValue = hasValue;
        }

        private Optional(Value v) {
            this._value = v;
            this._hasValue = true;
        }

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
    }
}