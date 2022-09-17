namespace Foundation {
    public interface Hashable {
        public int GetHashCode();
        /*
        void Hash(ref Hasher hasher);

        int HashValue {
            get {
                Hasher hasher = new Hasher();
                this.Hash(ref hasher);
                return hasher.Finalize();
            }
        }
        */
    }
}