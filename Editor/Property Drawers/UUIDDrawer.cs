#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Foundation.Editors {
	[CustomPropertyDrawer(typeof(UUID))]
	internal sealed class UUIDDrawer : PropertyDrawer {
		private const float SPACING = 4;
		private const float MINI_BUTTON_WIDTH = 20;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			if (property.boxedValue is not UUID uuid) {
				return;
			}

			EditorGUI.BeginProperty(position, label, property);

			// Draw label
			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

			// Clear indent
			int indent = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;

			// Calculate rects
			Rect stringRect = new Rect(position.x, position.y, position.width - (MINI_BUTTON_WIDTH + SPACING) * 2, position.height);
			float consumed = stringRect.width + SPACING;
			Rect clipboardButtonRect = new Rect(position.x + consumed, position.y, MINI_BUTTON_WIDTH, position.height);
			consumed += MINI_BUTTON_WIDTH + SPACING;
			Rect refreshButtonRect = new Rect(position.x + consumed, position.y, MINI_BUTTON_WIDTH, position.height);

			// Draw
			EditorGUI.BeginDisabledGroup(true);
			EditorGUI.TextField(stringRect, uuid.uuidString);
			EditorGUI.EndDisabledGroup();

			if (GUI.Button(clipboardButtonRect, new GUIContent(EditorGUIUtility.FindTexture("Clipboard"), "Copy"))) {
				EditorGUIUtility.systemCopyBuffer = uuid.uuidString;
			}

			if (GUI.Button(refreshButtonRect, new GUIContent(EditorGUIUtility.FindTexture("Refresh"), "Recreate"))) {
				property.boxedValue = UUID.Create();
			}

			// Restore indent
			EditorGUI.indentLevel = indent;

			EditorGUI.EndProperty();
		}
	}
}
#endif