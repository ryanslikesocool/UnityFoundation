namespace Foundation {
    public interface IPropertyWrapper<Value> {
        Value wrappedValue { get; set; }
    }
}