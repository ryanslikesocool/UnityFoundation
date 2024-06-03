// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Foundation.Editors {
	/// <summary>
	/// Draws a Vector2Int field for vector properties.
	/// Usage: [Vector2Int] _Vector2Int("Vector 2 Int", Vector) = (1, 1, 0, 0)
	/// </summary>
	internal sealed class Vector2IntDrawer : MaterialPropertyDrawer {
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

				Vector2Int propValue = new Vector2Int((int)prop.vectorValue.x, (int)prop.vectorValue.y);
				propValue = EditorGUI.Vector2IntField(position, label, propValue);

				if (EditorGUI.EndChangeCheck()) {
					prop.vectorValue = new Vector4(propValue.x, propValue.y, 0, 0);
				}
			} else {
				editor.DefaultShaderProperty(prop, label.text);
			}
		}
	}
}
#endif