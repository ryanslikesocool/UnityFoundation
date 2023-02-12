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
    public sealed class State<Value> : IPropertyWrapper<Value> where Value : struct {
        [SerializeField, Tooltip("The underlying property value.")] private Value _value;

        /// <summary>
        /// The underlying value referenced by the state variable.
        /// </summary>
        public Value wrappedValue {
            get => _value;
            set => _value = value;
        }

        /// <summary>
        /// A binding to the state value.
        /// </summary>
        public readonly Binding<Value> projectedValue;

        /// <summary>
        /// Creates the state with an initial value.
        /// </summary>
        /// <param name="initialValue">An initial value of the state.</param>
        public State(Value initialValue) {
            this._value = initialValue;

            this.projectedValue = new Binding<Value>(propertyWrapper: this);
        }

        public static implicit operator Value(State<Value> v) => v.wrappedValue;
    }
}