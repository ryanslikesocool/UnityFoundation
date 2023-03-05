using System;
using UnityEngine;

namespace Foundation {
    /// <summary>
    /// A property wrapper type that can read and write a value.
    /// </summary>
    /// <remarks>
    /// Use State as the single source of truth for a given value.
    /// </remarks>
    [Serializable]
    public sealed class State<Value> : IMutablePropertyWrapper<Value> {
        [SerializeField, Tooltip("The underlying property value.")] private Value _value;

        public Value wrappedValue {
            get => _value;
            set => _value = value;
        }

        /// <summary>
        /// Creates the state with an initial value.
        /// </summary>
        /// <param name="initialValue">An initial value of the state.</param>
        public State(Value initialValue = default) {
            this._value = initialValue;
        }

        public static implicit operator Value(State<Value> wrapper) => wrapper.wrappedValue;
    }
}