// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

#if UNITY_EDITOR
using UnityEditor;

namespace Foundation.Editors {
	public class BoxGroupScope : EditorGUILayout.VerticalScope {
		public BoxGroupScope(string title = null) : base("HelpBox") {
			if (!string.IsNullOrEmpty(title)) {
				EditorGUILayout.LabelField(title, EditorStyles.boldLabel);
			}

			EditorGUI.indentLevel += 1;
		}

		protected override void CloseScope() {
			EditorGUI.indentLevel -= 1;
			base.CloseScope();
		}
	}
}
#endif