// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using System;
using UnityEngine;

namespace Foundation.Editor {
	public static partial class FoundationGizmoUtility {
		/// <summary>
		/// Disposable helper struct for automatically setting and reverting <see cref="Gizmos.color"/> and/or <see cref="Gizmos.matrix"/>.
		/// </summary>
		public struct DrawingScope : IDisposable {
			private bool isDisposed;

			/// <summary>
			/// The value of <see cref="Gizmos.color"/> at the time this <see cref="DrawingScope"/> was created.
			/// </summary>
			public readonly Color originalColor;

			/// <summary>
			/// The value of <see cref="Gizmos.matrix"/> at the time this <see cref="DrawingScope"/> was created.
			/// </summary>
			public readonly Matrix4x4 originalMatrix;

			/// <summary>
			/// Create a new <see cref="DrawingScope"/> and set <see cref="Gizmos.color"/> and <see cref="Gizmos.matrix"/> to the specified values.
			/// </summary>
			/// <param name="color">The color to use for displaying Gizmos inside the scope block.</param>
			/// <param name="matrix">The matrix to use for displaying Gizmos inside the scope block.</param>
			public DrawingScope(Color color, Matrix4x4 matrix) {
				isDisposed = false;
				this.originalColor = Gizmos.color;
				this.originalMatrix = Gizmos.matrix;
				Gizmos.color = color;
				Gizmos.matrix = matrix;
			}

			/// <summary>
			/// Create a new <see cref="DrawingScope"/> and set <see cref="Gizmos.color"/> to the specified value.
			/// </summary>
			/// <param name="color">The color to use for displaying Gizmos inside the scope block.</param>
			public DrawingScope(Color color) : this(color, Gizmos.matrix) { }

			/// <summary>
			/// Create a new <see cref="DrawingScope"/> and set <see cref="Gizmos.matrix"/> to the specified value.
			/// </summary>
			/// <param name="matrix">The matrix to use for displaying Gizmos inside the scope block.</param>
			public DrawingScope(Matrix4x4 matrix) : this(Gizmos.color, matrix) { }

			/// <summary>
			/// Automatically reverts <see cref="Gizmos.color"/> and <see cref="Gizmos.matrix"/> to their values prior to entering the scope, when the scope is exited.
			/// You do not need to call this method manually.
			/// </summary>
			public void Dispose() {
				if (!isDisposed) {
					isDisposed = true;
					Gizmos.color = originalColor;
					Gizmos.matrix = originalMatrix;
				}
			}
		}
	}
}