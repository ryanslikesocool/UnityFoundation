namespace Foundation {
    /// <summary>
    /// A type with a customized textual representation.
    /// </summary>
    public interface ICustomStringConvertible {
        /// <summary>
        /// A textual representation of this instance.
        /// </summary>
        public string description { get; }
    }
}