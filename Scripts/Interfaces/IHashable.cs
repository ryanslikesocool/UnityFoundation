namespace Foundation {
    public interface IHashable {
        //int GetHashCode();

        void Hash(ref Hasher hasher);

        int hashValue {
            get {
                Hasher hasher = new Hasher();
                this.Hash(ref hasher);
                return hasher.Finalize();
            }
        }
    }
}
