using UnityEditor;
using UnityEngine;

namespace Foundation.Editors {
    [CustomPropertyDrawer(typeof(EditorMutable<>))]
    internal sealed class EditorMutableDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            SerializedProperty valueProperty = property.FindPropertyRelative("_value");

            EditorGUI.BeginProperty(position, label, property);

            EditorGUI.PropertyField(position, valueProperty, label);

            EditorGUI.EndProperty();
        }
    }
}