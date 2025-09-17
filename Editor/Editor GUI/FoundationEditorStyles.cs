// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEditor;
using UnityEngine;

namespace Foundation.Editor {
	public static partial class FoundationEditorStyles {
		internal static readonly GUIStyle horizontalLine;

		static FoundationEditorStyles() {
			horizontalLine = CreateHorizontalLine();

			static GUIStyle CreateHorizontalLine() {
				GUIStyle style = new();
				style.normal.background = EditorGUIUtility.whiteTexture;
				style.margin = new(0, 0, 4, 4);
				style.fixedHeight = 1;
				return style;
			}
		}
	}
}