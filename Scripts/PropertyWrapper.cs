using System;

namespace Foundation {
    public class PropertyWrapper<Value> {
        private Value _wrappedValue;
        public Value WrappedValue {
            get => _wrappedValue;
            set {
                if (willSet != null) {
                    value = willSet.Invoke(value);
                }
                _wrappedValue = value;
                didSet?.Invoke(_wrappedValue);
            }
        }

        public Func<Value, Value> willSet = null;
        public Action<Value> didSet = null;

        public PropertyWrapper(in Value initialValue) {
            this._wrappedValue = initialValue;
        }

        public PropertyWrapper(in Value initialValue, in Func<Value, Value> willSet, in Action<Value> didSet) {
            this._wrappedValue = initialValue;
            this.willSet = willSet;
            this.didSet = didSet;
        }

        public static implicit operator Value(PropertyWrapper<Value> v) => v.WrappedValue;
    }
}