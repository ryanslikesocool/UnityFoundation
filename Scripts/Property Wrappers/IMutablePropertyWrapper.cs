using System;

namespace Foundation {
    /// <summary>
    /// A property wrapper that support getting and setting a wrapped value.
    /// </summary>
    public interface IMutablePropertyWrapper<Value> : IImmutablePropertyWrapper<Value> {
        public delegate void MutateAction(ref Value value);

        public new Value wrappedValue { get; set; }

        public void Mutate(MutateAction body) {
            Value value = wrappedValue;
            body(ref value);
            wrappedValue = value;
        }
    }
}