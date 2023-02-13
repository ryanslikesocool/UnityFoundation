namespace Foundation {
    public sealed class Lazy<Value> : IPropertyWrapper<Value> where Value : struct {
        public delegate Value Initializer();

        private Value? _value;

        /// <summary>
        /// The underlying value referenced by the state variable.
        /// </summary>
        public Value wrappedValue {
            get {
                if (!_value.HasValue) {
                    _value = initializer();
                }
                return _value.Value;
            }
            set => _value = value;
        }

        private readonly Initializer initializer;

        /// <summary>
        /// Create a new lazy property.
        /// </summary>
        /// <param name="initializer">The property's initializer.</param>
        public Lazy(Initializer initializer) {
            if (initializer == null) {
                throw new System.NullReferenceException("initializer");
            }
            this.initializer = initializer;
        }

        public static implicit operator Value(Lazy<Value> v) => v.wrappedValue;
        public static implicit operator Lazy<Value>(Initializer init) => new Lazy<Value>(init);
    }
}