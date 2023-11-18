namespace Foundation {
    /// <summary>
    /// A type that can be hashed into a Hasher to produce an integer hash value.
    /// </summary>
    public interface IHashable {
        /// <summary>
        /// Hashes the essential components of this value by feeding them into the given hasher.
        /// </summary>
        /// <param name="hasher">The hasher to use when combining the components of this instance.</param>
        public void Hash(ref Hasher hasher);

        /// <summary>
        /// The hash value.
        /// </summary>
        /// <remarks>
        /// Hash values are not guaranteed to be equal across different executions of your program. Do not save hash values to use during a future execution.
        /// </remarks>
        public int hashValue {
            get {
                Hasher hasher = new Hasher();
                this.Hash(ref hasher);
                return hasher.Finalize();
            }
        }

        public int GetHashCode() => hashValue;
    }
}
