// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEditor;
using UnityEngine;

namespace Foundation.Editors {
	public static partial class FoundationEditorGUI {
		public class BoxGroupScope : EditorGUILayout.VerticalScope {
			public BoxGroupScope(GUIContent title = null) : base("HelpBox") {
				if (title != null) {
					EditorGUILayout.LabelField(title, EditorStyles.boldLabel);
				}
				HorizontalLine();

				EditorGUI.indentLevel += 1;
			}

			public BoxGroupScope(string title) : this(new GUIContent(title)) { }

			protected override void CloseScope() {
				EditorGUI.indentLevel -= 1;
				base.CloseScope();
			}
		}
	}
}