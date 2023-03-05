using System;

namespace Foundation {
    /// <summary>
    /// A property wrapper that support getting and setting a wrapped value.
    /// </summary>
    public interface IMutablePropertyWrapper<Value> : IImmutablePropertyWrapper<Value> {
        delegate void MutateAction(ref Value value);

        /// <summary>
        /// Access to the wrapper's underlying value.
        /// </summary>
        new Value wrappedValue { get; set; }

        /// <summary>
        /// A projection of the wrapper that returns a binding.
        /// </summary>
        new Binding<Value> projectedValue => new Binding<Value>(propertyWrapper: this);

        void Mutate(MutateAction body) {
            Value value = wrappedValue;
            body(ref value);
            wrappedValue = value;
        }
    }
}