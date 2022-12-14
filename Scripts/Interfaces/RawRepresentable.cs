namespace Foundation {
    /// <summary>
    /// A type that can be converted to and from an associated raw value.
    /// </summary>
    /// <typeparam name="RawValue">The raw type that can be used to represent all values of the conforming type.</typeparam>
    public interface RawRepresentable<RawValue> {
        public RawValue rawValue { get; }
    }
}