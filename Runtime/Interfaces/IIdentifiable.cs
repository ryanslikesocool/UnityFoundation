namespace Foundation {
	/// <summary>
	/// A class of types whose instances hold the value of an entity with stable identity.
	/// </summary>
	/// <typeparam name="ID">A type representing the stable identity of the entity associated with an instance.</typeparam>
	public interface IIdentifiable<ID> {
		/// <summary>
		/// The stable identity of the entity associated with this instance.
		/// </summary>
		public ID id { get; }
	}
}