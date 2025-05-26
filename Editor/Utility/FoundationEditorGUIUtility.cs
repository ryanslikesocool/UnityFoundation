using UnityEditor;
using UnityEngine;

namespace Foundation.Editors {
	public static partial class FoundationEditorGUIUtility {
		/// <summary>
		/// Calculate the height required to accomodate property spanning a number of lines.
		/// </summary>
		/// <param name="lineCount">The number of lines the property requires.</param>

		// float totalSpacing = EditorGUIUtility.standardVerticalSpacing * (lineCount - 1);
		// float totalLines = EditorGUIUtility.singleLineHeight * lineCount;
		public static float PropertyHeight(int lineCount) {
			float lineHeight = EditorGUIUtility.singleLineHeight;
			float lineSpacing = EditorGUIUtility.standardVerticalSpacing;
			return (lineHeight + lineSpacing) * lineCount - lineSpacing;
		}

		public static Rect IncrementingLine(Rect position, int count = 1) {
			position.y += (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * count;
			return position;
		}
	}
}