namespace Foundation {
    public interface IFileSerializable : ISerializable {
        static string FileURL { get; }
    }
}