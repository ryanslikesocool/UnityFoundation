using System.Collections.Generic;

namespace Foundation {
	/// <summary>
	/// Describes an object that tracks regisration for elements.
	/// </summary>
	/// <remarks>
	/// In most cases, a registry is backed by a non-indexed collection.
	/// </remarks>
	/// <typeparam name="Element">The element type to manage.</typeparam>
	public interface IRegistry<Element> {
		void Register(Element element);

		void Unregister(Element element);
	}

	public static class RegistrarExtensions {
		public static void Register<Element>(this IRegistry<Element> registrar, IEnumerable<Element> elements) {
			foreach (Element element in elements) {
				registrar.Register(element);
			}
		}

		public static void Unregister<Element>(this IRegistry<Element> registrar, IEnumerable<Element> elements) {
			foreach (Element element in elements) {
				registrar.Unregister(element);
			}
		}
	}
}