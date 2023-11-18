namespace Foundation {
    /// <summary>
    /// A type that can be converted to and from an associated raw value.
    /// </summary>
    /// <typeparam name="RawValue">The raw type that can be used to represent all values of the conforming type.</typeparam>
    public interface IRawRepresentable<RawValue> {
        /// <summary>
        /// The corresponding value of the raw type.
        /// </summary>
        public RawValue rawValue { get; }
    }
}