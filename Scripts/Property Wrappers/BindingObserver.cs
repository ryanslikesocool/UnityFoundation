using System;
using UnityEngine;

namespace Foundation {
    /// <summary>
    /// A binding type that can read and write a value, as well as observe changes to the underlying value.
    /// </summary>
    public struct BindingObserver<Value> : IChangeObserver<Value> where Value : struct {
        public delegate Value Get();
        public delegate void Set(Value value);

        private readonly Get get;
        private readonly Set set;

        public Value wrappedValue {
            get => get();
            set {
                Value oldValue = wrappedValue;
                _willSetFunction?.Invoke(oldValue, ref value);
                set(value);
                _didSetFunction?.Invoke(oldValue, wrappedValue);
            }
        }

        private IChangeObserver<Value>.DidSetCallback _didSetFunction;
        private IChangeObserver<Value>.WillSetCallback _willSetFunction;

        public bool HasWillSetFunction => _willSetFunction != null;
        public bool HasDidSetFunction => _didSetFunction != null;

        /// <summary>
        /// Create a new binding observer.
        /// </summary>
        /// <param name="get">A closure that retrieves the binding value. The closure has no parameters, and returns a value.</param>
        /// <param name="set">
        /// A closure that sets the binding value. The closure has the following parameter:
        /// - newValue: The new value of the binding value.
        /// </param>
        /// <param name="willSet">The function to call when the property is about to be set.</param>
        /// <param name="didSet">The function to call immediately after the property was set.</param>
        public BindingObserver(Get get, Set set, IChangeObserver<Value>.WillSetCallback willSet, IChangeObserver<Value>.DidSetCallback didSet) {
            if (get == null) {
                throw new System.ArgumentNullException("get");
            }
            if (set == null) {
                throw new System.ArgumentNullException("set");
            }

            this.get = get;
            this.set = set;

            this._willSetFunction = null;
            this._didSetFunction = null;

            this.SetWillSetCallback(willSet);
            this.SetDidSetCallback(didSet);
        }

        /// <summary>
        /// Create a new binding observer from an existing property wrapper.
        /// </summary>
        /// <param name="propertyWrapper">The existing property wrapper.</param>
        /// <param name="willSet">The function to call when the property is about to be set.</param>
        /// <param name="didSet">The function to call immediately after the property was set.</param>
        public BindingObserver(IMutablePropertyWrapper<Value> propertyWrapper, IChangeObserver<Value>.WillSetCallback willSet, IChangeObserver<Value>.DidSetCallback didSet) : this(() => propertyWrapper.wrappedValue, value => propertyWrapper.wrappedValue = value, willSet, didSet) { }

        /// <summary>
        /// Create a new binding observer with no callbacks.
        /// </summary>
        /// <param name="get">A closure that retrieves the binding value. The closure has no parameters, and returns a value.</param>
        /// <param name="set">
        /// A closure that sets the binding value. The closure has the following parameter:
        /// - newValue: The new value of the binding value.
        /// </param>
        public BindingObserver(Get get, Set set) : this(get, set, null, null) { }

        /// <summary>
        /// Create a new binding observer from an existing property wrapper.
        /// </summary>
        /// <param name="propertyWrapper">The existing property wrapper.</param>
        public BindingObserver(IMutablePropertyWrapper<Value> propertyWrapper) : this(() => propertyWrapper.wrappedValue, value => propertyWrapper.wrappedValue = value) { }

        /// <summary>
        /// Create a new binding observer with no `willSet` function.
        /// </summary>
        /// <param name="didSet">The function to call after the property was set. The parameter is the property value.</param>
        public BindingObserver(Get get, Set set, IChangeObserver<Value>.DidSetCallback didSet) : this(get, set, null, didSet) { }

        /// <summary>
        /// Create a new binding observer with no `willSet` function.
        /// </summary>
        /// <param name="propertyWrapper">The existing property wrapper.</param>
        /// <param name="didSet">The function to call after the property was set. The parameter is the property value.</param>
        public BindingObserver(IMutablePropertyWrapper<Value> propertyWrapper, IChangeObserver<Value>.DidSetCallback didSet) : this(() => propertyWrapper.wrappedValue, value => propertyWrapper.wrappedValue = value, null, didSet) { }

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

        public static implicit operator Value(BindingObserver<Value> wrapper) => wrapper.wrappedValue;

        public static implicit operator BindingObserver<Value>(Binding<Value> wrapper) => new BindingObserver<Value>(wrapper);
    }
}