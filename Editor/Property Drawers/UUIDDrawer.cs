// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEditor;
using UnityEngine;

namespace Foundation.Editors {
	[CustomPropertyDrawer(typeof(UUID))]
	internal sealed class UUIDDrawer : PropertyDrawer {
		// MARK: - IMGUI

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			if (property.boxedValue is not UUID uuid) {
				return;
			}

			using (var scope = new EditorGUI.PropertyScope(position, label, property)) {
				// Draw label
				label = scope.content;
				position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

				using (new EditorGUI.IndentLevelScope(-EditorGUI.indentLevel)) {
					// Calculate rects
					Rect stringRect = new Rect(position.x, position.y, position.width - (MINI_BUTTON_WIDTH + SPACING) * 2, position.height);
					float consumed = stringRect.width + SPACING;
					Rect clipboardButtonRect = new Rect(position.x + consumed, position.y, MINI_BUTTON_WIDTH, position.height);
					consumed += MINI_BUTTON_WIDTH + SPACING;
					Rect refreshButtonRect = new Rect(position.x + consumed, position.y, MINI_BUTTON_WIDTH, position.height);

					// Draw
					using (new EditorGUI.DisabledGroupScope(true)) {
						EditorGUI.TextField(stringRect, uuid.uuidString);
					}

					if (GUI.Button(clipboardButtonRect, new GUIContent(EditorGUIUtility.FindTexture(COPY_TEXTURE), COPY_LABEL))) {
						EditorGUIUtility.systemCopyBuffer = uuid.uuidString;
					}

					if (GUI.Button(refreshButtonRect, new GUIContent(EditorGUIUtility.FindTexture(RECREATE_TEXTURE), RECREATE_LABEL))) {
						property.boxedValue = UUID.Create();
					}
				}
			}
		}

		// MARK: - Constants

		private const float SPACING = 4;
		private const float MINI_BUTTON_WIDTH = 20;

		private const string COPY_LABEL = "Copy";
		private const string RECREATE_LABEL = "Recreate";

		private const string COPY_TEXTURE = "Clipboard";
		private const string RECREATE_TEXTURE = "Refresh";
	}
}