// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEditor;
using UnityEngine;

namespace Foundation.Editors {
	/// <summary>
	/// Draws a Vector2 field for vector properties.
	/// Usage: [Vector2] _Vector2("Vector 2", Vector) = (1, 1, 0, 0)
	/// </summary>
	internal sealed class Vector2Drawer : MaterialPropertyDrawer {
		public override void OnGUI(Rect position, MaterialProperty prop, GUIContent label, MaterialEditor editor) {
			if (prop.type == MaterialProperty.PropType.Vector) {
				EditorGUIUtility.labelWidth = 0f;
				EditorGUIUtility.fieldWidth = 0f;

				if (!EditorGUIUtility.wideMode) {
					EditorGUIUtility.wideMode = true;
					EditorGUIUtility.labelWidth = EditorGUIUtility.currentViewWidth - 212;
				}

				EditorGUI.BeginChangeCheck();
				EditorGUI.showMixedValue = prop.hasMixedValue;

				Vector4 vec = EditorGUI.Vector2Field(position, label, prop.vectorValue);

				if (EditorGUI.EndChangeCheck()) {
					prop.vectorValue = vec;
				}
			} else {
				editor.DefaultShaderProperty(prop, label.text);
			}
		}
	}
}