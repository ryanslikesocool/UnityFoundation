namespace Foundation {
    /// <summary>
    /// A property wrapper type that can read and write a value owned by a source of truth.
    /// </summary>
    public readonly struct Binding<Value> : IMutablePropertyWrapper<Value> {
        public delegate Value Get();
        public delegate void Set(Value value);

        private readonly Get get;
        private readonly Set set;

        /// <summary>
        /// The underlying value referenced by the binding variable.
        /// </summary>
        public Value wrappedValue {
            get => get();
            set => set(value);
        }

        /// <summary>
        /// A projection of the binding value that returns a binding.
        /// </summary>
        public Binding<Value> projectedValue => this;

        /// <summary>
        /// Creates a binding from an existing property wrapper.
        /// </summary>
        /// <param name="propertyWrapper">The existing property wrapper.</param>
        public Binding(IMutablePropertyWrapper<Value> propertyWrapper) {
            this.get = () => propertyWrapper.wrappedValue;
            this.set = value => propertyWrapper.wrappedValue = value;
        }

        /// <summary>
        /// Creates a binding with closures that read and write the binding value.
        /// </summary>
        /// <param name="get">A closure that retrieves the binding value. The closure has no parameters, and returns a value.</param>
        /// <param name="set">
        /// A closure that sets the binding value. The closure has the following parameter:
        /// - newValue: The new value of the binding value.
        /// </param>
        public Binding(Get get, Set set) {
            this.get = get;
            this.set = set;
        }

        public static implicit operator Value(Binding<Value> binding) => binding.wrappedValue;

        public static implicit operator ImmutableBinding<Value>(Binding<Value> binding) => new ImmutableBinding<Value>(binding);
    }
}