#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Foundation.Editors {
	/// <summary>
	/// Draws a Vector3Int field for vector properties.
	/// Usage: [Vector3Int] _Vector3Int("Vector 3 Int", Vector) = (1, 1, 1, 0)
	/// </summary>
	internal sealed class Vector3IntDrawer : MaterialPropertyDrawer {
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

				Vector3Int propValue = new Vector3Int((int)prop.vectorValue.x, (int)prop.vectorValue.y, (int)prop.vectorValue.z);
				propValue = EditorGUI.Vector3IntField(position, label, propValue);

				if (EditorGUI.EndChangeCheck()) {
					prop.vectorValue = new Vector4(propValue.x, propValue.y, propValue.z, 0);
				}
			} else {
				editor.DefaultShaderProperty(prop, label.text);
			}
		}
	}
}
#endif