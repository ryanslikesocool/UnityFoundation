namespace Foundation {
    public interface Hashable {
        public int GetHashCode();

        /*
        public void Hash(ref Hasher hasher);

        public int hashValue {
            get {
                Hasher hasher = new Hasher();
                this.Hash(ref hasher);
                return hasher.Finalize();
            }
        }
        */
    }
}