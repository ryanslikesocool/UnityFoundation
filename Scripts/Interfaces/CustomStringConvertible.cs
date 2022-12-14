namespace Foundation {
    /// <summary>
    /// A type with a customized textual representation.
    /// </summary>
    public interface CustomStringConvertible {
        /// <summary>
        /// A textual representation of this instance.
        /// </summary>
        public string description { get; }
    }
}