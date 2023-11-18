//namespace Foundation {
//    public interface ISetAlgebra<Element> where Element : struct {
//        bool isEmpty { get; }
//
//        bool Contains(Element member);
//
//        (bool, Element) Insert(Element member);
//
//        Element? Update(Element newMember);
//
//        Element? Remove(Element member);
//
//        Element Union(Element other);
//
//        void FormUnion(Element other);
//
//        Element Intersection(Element other);
//
//        void FormIntersection(Element other);
//
//        ISetAlgebra<Element> SymmetricDifference(Element other);
//
//        void FormSymmetricDifference(Element other);
//
//        bool IsStrictSubsetOf(Element other);
//
//        bool IsStrictSupersetOf(Element other);
//
//        void Subtract(Element other);
//
//        Element Subtracting(Element other);
//    }
//}