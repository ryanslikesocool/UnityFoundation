// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;
using UnityEditor;

namespace Foundation.Editors {
	public static class FoundationEditorStyles {
		internal static readonly GUIStyle horizontalLine;

		static FoundationEditorStyles() {
			horizontalLine = new GUIStyle();
			horizontalLine.normal.background = EditorGUIUtility.whiteTexture;
			horizontalLine.margin = new RectOffset(0, 0, 4, 4);
			horizontalLine.fixedHeight = 1;
		}
	}
}