namespace Foundation {
    /// <summary>
    /// A class of types whose instances hold the value of an entity with stable identity.
    /// </summary>
    /// <typeparam name="ID">A type representing the stable identity of the entity associated with an instance.</typeparam>
    public interface IIdentifiable<ID> where ID : IHashable {
        public ID id { get; }
    }
}