using System;

namespace Foundation {
    public class PropertyWrapper<Value> {
        private Value _wrappedValue;
        public Value WrappedValue {
            get => _wrappedValue;
            set {
                Value oldValue = _wrappedValue;
                willSetFunction?.Invoke(_wrappedValue, ref value);
                _wrappedValue = value;
                didSetFunction?.Invoke(oldValue, _wrappedValue);
            }
        }

        public delegate void WillSetCallback(Value oldValue, ref Value newValue);
        public delegate void ImmutableWillSetCallback(Value oldValue, Value newValue);
        public delegate void DidSetCallback(Value oldValue, Value newValue);

        private DidSetCallback didSetFunction = default;
        private WillSetCallback willSetFunction = default;

        /// <summary>
        /// Create a new property wrapper with a mutable `willSet` function.
        /// </summary>
        /// <param name="initialValue">The initial property value.</param>
        /// <param name="willSet">The function to call when the property is about to be set.  The parameter is the new property value to do work with.  Return the parameter (or another value) to make that the new property value.</param>
        /// <param name="didSet">The function to call after the property was set. The parameter is the property value.</param>
        public PropertyWrapper(in Value initialValue, WillSetCallback willSet, DidSetCallback didSet) {
            this._wrappedValue = initialValue;
            this.willSetFunction = willSet;
            this.didSetFunction = didSet;
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
            this.didSetFunction = didSet;
        }

        public void SetWillSetCallback(ImmutableWillSetCallback callback) {
            if (callback != null) {
                this.willSetFunction = (Value oldValue, ref Value newValue) => {
                    callback(oldValue, newValue);
                };
            } else {
                willSetFunction = null;
            }
        }

        public void SetWillSetCallback(WillSetCallback callback) {
            willSetFunction = callback;
        }

        public void SetDidSetCallback(DidSetCallback callback) {
            didSetFunction = callback;
        }

        public static implicit operator Value(PropertyWrapper<Value> v) => v.WrappedValue;
    }
}