namespace Foundation {
    /// <summary>
    /// A property wrapper that support getting, but not setting, a wrapped value.
    /// </summary>
    public interface IImmutablePropertyWrapper<Value> {
        Value wrappedValue { get; }
    }
}