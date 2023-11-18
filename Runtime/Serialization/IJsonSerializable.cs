namespace Foundation {
    public interface IJsonSerializable : ISerializable {
        bool PrettyPrint { get; }
    }
}