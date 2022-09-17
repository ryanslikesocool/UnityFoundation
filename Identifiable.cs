namespace Foundation {
    public interface Identifiable<ID> where ID : Hashable {
        public ID id { get; }
    }
}