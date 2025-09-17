// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using System;
using UnityEditor;

namespace Foundation.Editor {
	public static partial class FoundationEditorGUI {
		public readonly struct LabelWidthScope : IDisposable {
			private readonly float initialLabelWidth;

			public LabelWidthScope(float width) {
				initialLabelWidth = EditorGUIUtility.labelWidth;
				EditorGUIUtility.labelWidth = width;
			}

			public readonly void Dispose() {
				EditorGUIUtility.labelWidth = initialLabelWidth;
			}
		}
	}
}