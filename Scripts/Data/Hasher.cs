using System;

namespace Foundation {
    /// <summary>
    /// The universal hash function.
    /// </summary>
    /// <remarks>
    /// Hasher can be used to map an arbitrary sequence of bytes to an integer hash value. You can feed data to the hasher using a series of calls to mutating combine methods. When you’ve finished feeding the hasher, the hash value can be retrieved by calling <see cref="Finalize"/>:
    /// </remarks>
    public struct Hasher {
        private HashCode? hashCode;

        /// <summary>
        /// Adds the given value to this hasher, mixing its essential parts into the hasher state.
        /// </summary>
        /// <param name="value">A value to add to the hasher.</param>
        public void Combine<H>(in H value) {
            hashCode.Value.Add(value);
        }

        /// <summary>
        /// Finalizes the hasher state and returns the hash value.
        /// </summary>
        /// <remarks>
        /// Finalizing consumes the hasher: it is illegal to finalize a hasher you don’t own, or to perform operations on a finalized hasher.
        /// <br/>
        /// Hash values are not guaranteed to be equal across different executions of your program. Do not save hash values to use during a future execution.
        /// </remarks>
        /// <returns>The hash value calculated by the hasher.</returns>
        public int Finalize() {
            int result = hashCode.Value.ToHashCode();
            hashCode = null;
            return result;
        }
    }
}