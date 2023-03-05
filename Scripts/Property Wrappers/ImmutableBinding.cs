namespace Foundation {
    /// <summary>
    /// A property wrapper type that can read a value owned by a source of truth.
    /// </summary>
    public readonly struct ImmutableBinding<Value> : IImmutablePropertyWrapper<Value> {
        public delegate Value Get();

        private readonly Get get;

        public Value wrappedValue => get();

        /// <summary>
        /// Creates a binding from an existing property wrapper.
        /// </summary>
        /// <param name="propertyWrapper">The existing property wrapper.</param>
        public ImmutableBinding(IImmutablePropertyWrapper<Value> propertyWrapper) {
            this.get = () => propertyWrapper.wrappedValue;
        }

        /// <summary>
        /// Creates a binding with closures that read the binding value.
        /// </summary>
        /// <param name="get">A closure that retrieves the binding value. The closure has no parameters, and returns a value.</param>
        public ImmutableBinding(Get get) {
            this.get = get;
        }

        public static implicit operator Value(ImmutableBinding<Value> v) => v.wrappedValue;

        //public static implicit operator ImmutableBinding<Value>(IImutablePropertyWrapper<Value> wrapper) => wrapper.projectedValue;
    }
}