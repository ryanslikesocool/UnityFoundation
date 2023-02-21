namespace Foundation {
    /// <summary>
    /// A property wrapper that support getting and setting a wrapped value.
    /// </summary>
    public interface IMutablePropertyWrapper<Value> : IImmutablePropertyWrapper<Value> {
        new Value wrappedValue { get; set; }
    }
}