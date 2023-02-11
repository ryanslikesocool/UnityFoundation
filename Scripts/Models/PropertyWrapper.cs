using System;

namespace Foundation {
    public class PropertyWrapper<Value> {
        private Value _wrappedValue;

        /// <summary>
        /// Access the underlying wrapped value.  Setting this value will invoke the `willSet` and `didSet` functions of the wrapper.
        /// </summary>
        public Value WrappedValue {
            get => _wrappedValue;
            set {
                Value oldValue = _wrappedValue;
                willSetFunction?.Invoke(_wrappedValue, ref value);
                _wrappedValue = value;
                didSetFunction?.Invoke(oldValue, _wrappedValue);
            }
        }

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

        private DidSetCallback didSetFunction = default;
        private WillSetCallback willSetFunction = default;

        /// <summary>
        /// Create a new property wrapper with no callbacks.
        /// </summary>
        /// <param name="initialValue">The initial property value.</param>
        public PropertyWrapper(in Value initialValue) {
            this._wrappedValue = initialValue;
        }

        /// <summary>
        /// Create a new property wrapper with no `willSet` function.
        /// </summary>
        /// <param name="initialValue">The initial property value.</param>
        /// <param name="didSet">The function to call after the property was set. The parameter is the property value.</param>
        public PropertyWrapper(in Value initialValue, DidSetCallback didSet) {
            this._wrappedValue = initialValue;
            SetDidSetCallback(didSet);
        }

        /// <summary>
        /// Create a new property wrapper with a mutable `willSet` function.
        /// </summary>
        /// <param name="initialValue">The initial property value.</param>
        /// <param name="willSet">The function to call when the property is about to be set.  The parameter is the new property value to do work with.  Return the parameter (or another value) to make that the new property value.</param>
        /// <param name="didSet">The function to call after the property was set. The parameter is the property value.</param>
        public PropertyWrapper(in Value initialValue, WillSetCallback willSet, DidSetCallback didSet) {
            this._wrappedValue = initialValue;
            SetWillSetCallback(willSet);
            SetDidSetCallback(didSet);
        }

        /// <summary>
        /// Create a new property wrapper with an immutable `willSet` function.
        /// </summary>
        /// <param name="initialValue">The initial property value.</param>
        /// <param name="willSet">The function to call when the property is about to be set.  The first parameter is the old property value.  The second parameter is the new property value.</param>
        /// <param name="didSet">The function to call after the property was set. The parameter is the property value.</param>
        public PropertyWrapper(in Value initialValue, ImmutableWillSetCallback willSet, DidSetCallback didSet) {
            this._wrappedValue = initialValue;
            SetWillSetCallback(willSet);
            SetDidSetCallback(didSet);
        }

        /// <summary>
        /// Replace this property wrapper's `willSet` callback with a new immutable one.
        /// </summary>
        /// <param name="callback">The new immutable `willSet` callback.</param>
        public void SetWillSetCallback(ImmutableWillSetCallback callback) {
            if (callback != null) {
                this.willSetFunction = (Value oldValue, ref Value newValue) => {
                    callback(oldValue, newValue);
                };
            } else {
                willSetFunction = null;
            }
        }

        /// <summary>
        /// Replace this property wrapper's `willSet` callback with a new mutable one.
        /// </summary>
        /// <param name="callback">The new mutable `willSet` callback.</param>
        public void SetWillSetCallback(WillSetCallback callback) {
            willSetFunction = callback;
        }

        /// <summary>
        /// Replace this property wrapper's `didSet` callback with a new one.
        /// </summary>
        /// <param name="callback">The new `willSet` callback.</param>
        public void SetDidSetCallback(DidSetCallback callback) {
            didSetFunction = callback;
        }

        public static implicit operator Value(PropertyWrapper<Value> v) => v.WrappedValue;
    }
}