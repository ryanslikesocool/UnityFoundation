namespace Foundation {
    /// <summary>
    /// A property wrapper that support getting, but not setting, a wrapped value.
    /// </summary>
    public interface IImmutablePropertyWrapper<Value> {
        /// <summary>
        /// Read-only access to the wrapper's underlying value.
        /// </summary>
        Value wrappedValue { get; }

        /// <summary>
        /// A projection of the wrapper that returns a binding.
        /// </summary>
        ImmutableBinding<Value> projectedValue => new ImmutableBinding<Value>(propertyWrapper: this);
    }
}