using System;
using UnityEngine;

namespace Foundation {
    /// <summary>
    /// A property wrapper type that can read and write a value, as well as observe changes to the underlying value.
    /// </summary>
    /// <remarks>
    /// PropertyObserver should be used as the single source of truth for a given value.
    /// </remarks>
    [Serializable]
    public sealed class PropertyObserver<Value> : IChangeObserver<Value> where Value : struct {
        [SerializeField, Tooltip("The underlying property value.  Changes made to this value will not invoke callbacks.")] private Value _value;

        public Value wrappedValue {
            get => _value;
            set {
                Value oldValue = _value;
                _willSetFunction?.Invoke(_value, ref value);
                _value = value;
                _didSetFunction?.Invoke(oldValue, _value);
            }
        }

        private IChangeObserver<Value>.DidSetCallback _didSetFunction = default;
        private IChangeObserver<Value>.WillSetCallback _willSetFunction = default;

        public bool HasWillSetFunction => _willSetFunction != null;
        public bool HasDidSetFunction => _didSetFunction != null;

        /// <summary>
        /// Create a new property observer with no callbacks.
        /// </summary>
        /// <param name="initialValue">The initial property value.</param>
        public PropertyObserver(in Value initialValue) {
            this._value = initialValue;
        }

        /// <summary>
        /// Create a new property observer with no `willSet` function.
        /// </summary>
        /// <param name="initialValue">The initial property value.</param>
        /// <param name="didSet">The function to call immediately after the property was set.</param>
        public PropertyObserver(in Value initialValue, IChangeObserver<Value>.DidSetCallback didSet) : this(initialValue) {
            SetDidSetCallback(didSet);
        }

        /// <summary>
        /// Create a new property observer with a mutable `willSet` function.
        /// </summary>
        /// <param name="initialValue">The initial property value.</param>
        /// <param name="willSet">The function to call when the property is about to be set.</param>
        /// <param name="didSet">The function to call immediately after the property was set.</param>
        public PropertyObserver(in Value initialValue, IChangeObserver<Value>.WillSetCallback willSet, IChangeObserver<Value>.DidSetCallback didSet) : this(initialValue) {
            SetWillSetCallback(willSet);
            SetDidSetCallback(didSet);
        }

        /// <summary>
        /// Create a new property observer with an immutable `willSet` function.
        /// </summary>
        /// <param name="initialValue">The initial property value.</param>
        /// <param name="willSet">The function to call when the property is about to be set.</param>
        /// <param name="didSet">The function to call immediately after the property was set.</param>
        public PropertyObserver(in Value initialValue, IChangeObserver<Value>.ImmutableWillSetCallback willSet, IChangeObserver<Value>.DidSetCallback didSet) : this(initialValue) {
            SetWillSetCallback(willSet);
            SetDidSetCallback(didSet);
        }

        public void SetWillSetCallback(IChangeObserver<Value>.ImmutableWillSetCallback callback) {
            if (callback != null) {
                this._willSetFunction = (Value oldValue, ref Value newValue) => {
                    callback(oldValue, newValue);
                };
            } else {
                _willSetFunction = null;
            }
        }

        public void SetWillSetCallback(IChangeObserver<Value>.WillSetCallback callback) {
            _willSetFunction = callback;
        }

        public void SetDidSetCallback(IChangeObserver<Value>.DidSetCallback callback) {
            _didSetFunction = callback;
        }

        public static implicit operator Value(PropertyObserver<Value> v) => v.wrappedValue;
    }
}