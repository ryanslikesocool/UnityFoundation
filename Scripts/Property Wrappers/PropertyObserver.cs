using System;
using UnityEngine;

namespace Foundation {
    /// <summary>
    /// A property wrapper type that can read and write a value, as well as observe changes to the underlying value.
    /// </summary>
    /// <remarks>
    /// Use PropertyObserver as the single source of truth for a given value.
    /// </remarks>
    [Serializable]
    public sealed class PropertyObserver<Value> : IMutablePropertyWrapper<Value> where Value : struct {
        [SerializeField, Tooltip("The underlying property value.  Changes made to this value will not invoke callbacks.")] private Value _value;

        /// <summary>
        /// Access the underlying wrapped value.  Setting this value will invoke the `willSet` and `didSet` functions of the wrapper.
        /// </summary>
        public Value wrappedValue {
            get => _value;
            set {
                Value oldValue = _value;
                _willSetFunction?.Invoke(_value, ref value);
                _value = value;
                _didSetFunction?.Invoke(oldValue, _value);
            }
        }

        /// <summary>
        /// A binding to the property observer value.
        /// </summary>
        public readonly Binding<Value> projectedValue;

        /// <summary>
        /// The `willSet` callback is called immediately before the wrapped value is set.
        /// </summary>
        /// <param name="oldValue">The old (current) wrapped value.</param>
        /// <param name="newValue">The new mutable wrapped value.</param>
        public delegate void WillSetCallback(Value oldValue, ref Value newValue);

        /// <summary>
        /// The `willSet` callback is called immediately before the wrapped value is set.
        /// </summary>
        /// <param name="oldValue">The old (current) wrapped value.</param>
        /// <param name="newValue">The new immutable wrapped value.</param>
        public delegate void ImmutableWillSetCallback(Value oldValue, Value newValue);

        /// <summary>
        /// The `didSet` callback is called immediately after the wrapped value is set.
        /// </summary>
        /// <param name="oldValue">The old wrapped value.</param>
        /// <param name="newValue">The new (current) wrapped value.</param>
        public delegate void DidSetCallback(Value oldValue, Value newValue);

        private DidSetCallback _didSetFunction = default;
        private WillSetCallback _willSetFunction = default;

        /// <summary>
        /// Create a new property wrapper with no callbacks.
        /// </summary>
        /// <param name="initialValue">The initial property value.</param>
        public PropertyObserver(in Value initialValue) {
            this._value = initialValue;
        }

        /// <summary>
        /// Create a new property wrapper with no `willSet` function.
        /// </summary>
        /// <param name="initialValue">The initial property value.</param>
        /// <param name="didSet">The function to call after the property was set. The parameter is the property value.</param>
        public PropertyObserver(in Value initialValue, DidSetCallback didSet) {
            this._value = initialValue;
            SetDidSetCallback(didSet);

            projectedValue = new Binding<Value>(propertyWrapper: this);
        }

        /// <summary>
        /// Create a new property wrapper with a mutable `willSet` function.
        /// </summary>
        /// <param name="initialValue">The initial property value.</param>
        /// <param name="willSet">The function to call when the property is about to be set.  The parameter is the new property value to do work with.  Return the parameter (or another value) to make that the new property value.</param>
        /// <param name="didSet">The function to call after the property was set. The parameter is the property value.</param>
        public PropertyObserver(in Value initialValue, WillSetCallback willSet, DidSetCallback didSet) {
            this._value = initialValue;
            SetWillSetCallback(willSet);
            SetDidSetCallback(didSet);

            projectedValue = new Binding<Value>(propertyWrapper: this);
        }

        /// <summary>
        /// Create a new property wrapper with an immutable `willSet` function.
        /// </summary>
        /// <param name="initialValue">The initial property value.</param>
        /// <param name="willSet">The function to call when the property is about to be set.  The first parameter is the old property value.  The second parameter is the new property value.</param>
        /// <param name="didSet">The function to call after the property was set. The parameter is the property value.</param>
        public PropertyObserver(in Value initialValue, ImmutableWillSetCallback willSet, DidSetCallback didSet) {
            this._value = initialValue;
            SetWillSetCallback(willSet);
            SetDidSetCallback(didSet);

            projectedValue = new Binding<Value>(propertyWrapper: this);
        }

        /// <summary>
        /// Replace this property wrapper's `willSet` callback with a new immutable one.
        /// </summary>
        /// <param name="callback">The new immutable `willSet` callback.</param>
        public void SetWillSetCallback(ImmutableWillSetCallback callback) {
            if (callback != null) {
                this._willSetFunction = (Value oldValue, ref Value newValue) => {
                    callback(oldValue, newValue);
                };
            } else {
                _willSetFunction = null;
            }
        }

        /// <summary>
        /// Replace this property wrapper's `willSet` callback with a new mutable one.
        /// </summary>
        /// <param name="callback">The new mutable `willSet` callback.</param>
        public void SetWillSetCallback(WillSetCallback callback) {
            _willSetFunction = callback;
        }

        /// <summary>
        /// Replace this property wrapper's `didSet` callback with a new one.
        /// </summary>
        /// <param name="callback">The new `willSet` callback.</param>
        public void SetDidSetCallback(DidSetCallback callback) {
            _didSetFunction = callback;
        }

        public bool HasWillSetFunction => _willSetFunction != null;
        public bool HasDidSetFunction => _didSetFunction != null;

        public static implicit operator Value(PropertyObserver<Value> v) => v.wrappedValue;
    }
}