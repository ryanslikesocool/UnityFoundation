namespace Foundation {
    public interface IOptionSet<Element> : IRawRepresentable<Element>, ISetAlgebra<Element> where Element : struct { }
}
