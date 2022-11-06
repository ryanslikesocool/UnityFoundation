using System;

namespace Foundation {
    public class PropertyWrapper<Value> {
        private Value _wrappedValue;
        public Value WrappedValue {
            get => _wrappedValue;
            set {
                willSetFunction?.Invoke(_wrappedValue, ref value);
                _wrappedValue = value;
                didSetFunction?.Invoke(_wrappedValue);
            }
        }

        private delegate void ActionRef(Value oldValue, ref Value newValue);

        private Action<Value> didSetFunction = null;
        private ActionRef willSetFunction = null;

        /// <summary>
        /// Create a new property wrapper with a mutable `willSet` function.
        /// </summary>
        /// <param name="initialValue">The initial property value.</param>
        /// <param name="willSet">The function to call when the property is about to be set.  The parameter is the new property value to do work with.  Return the parameter (or another value) to make that the new property value.</param>
        /// <param name="didSet">The function to call after the property was set. The parameter is the property value.</param>
        public PropertyWrapper(in Value initialValue, Func<Value, Value> willSet, Action<Value> didSet) {
            this._wrappedValue = initialValue;
            this.willSetFunction = (Value oldValue, ref Value newValue) => {
                newValue = willSet(newValue);
            };
            this.didSetFunction = didSet;
        }

        /// <summary>
        /// Create a new property wrapper with an immutable `willSet` function.
        /// </summary>
        /// <param name="initialValue">The initial property value.</param>
        /// <param name="willSet">The function to call when the property is about to be set.  The first parameter is the old property value.  The second parameter is the new property value.</param>
        /// <param name="didSet">The function to call after the property was set. The parameter is the property value.</param>
        public PropertyWrapper(in Value initialValue, Action<Value, Value> willSet, Action<Value> didSet) {
            this._wrappedValue = initialValue;
            this.willSetFunction = (Value oldValue, ref Value newValue) => {
                willSet(oldValue, newValue);
            };
            this.didSetFunction = didSet;
        }

        public static implicit operator Value(PropertyWrapper<Value> v) => v.WrappedValue;
    }
}