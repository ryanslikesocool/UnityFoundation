// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;

namespace Foundation.Editors {
	public static partial class FoundationEditorGUI {
		public static void HorizontalLineShape() {
			GUILayout.Box(GUIContent.none, FoundationEditorStyles.horizontalLine);
		}

		public static void HorizontalLine(Color color) {
			Color originalColor = GUI.color;
			GUI.color = color;
			HorizontalLineShape();
			GUI.color = originalColor;
		}

		public static void HorizontalLine()
			=> HorizontalLine(Color.gray.WithOpacity(0.5f));
	}
}