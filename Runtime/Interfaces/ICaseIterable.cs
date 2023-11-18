namespace Foundation {
    /// <summary>
    /// A type that provides a collection of all of its values.
    /// </summary>
    public interface ICaseIterable<Value> {
        /// <summary>
        /// A collection of all values of this type.
        /// </summary>
        public static Value[] allCases { get; }
    }
}