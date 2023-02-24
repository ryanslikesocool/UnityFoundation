using UnityEditor;
using UnityEngine;

namespace Foundation.Editors {
    [CustomPropertyDrawer(typeof(Optional<>))]
    internal sealed class OptionalDrawer : PropertyDrawer {
        private SerializedProperty hasValueProperty = null;
        private SerializedProperty valueProperty = null;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            if (hasValueProperty == null) {
                hasValueProperty = property.FindPropertyRelative("_hasValue");
            }
            if (valueProperty == null) {
                valueProperty = property.FindPropertyRelative("_value");
            }
            return base.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginProperty(position, label, property);

            // Draw label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            // Clear indent
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            // Calculate rects
            Rect toggleRect = new Rect(position.x, position.y, 15, position.height);
            float consumed = toggleRect.width + 5;
            Rect valueRect = new Rect(position.x + consumed, position.y, position.width - consumed, position.height);

            // Draw fields - pass GUIContent.none to each so they are drawn without labels
            EditorGUI.PropertyField(toggleRect, hasValueProperty, GUIContent.none);
            bool guiEnabled = GUI.enabled;

            GUI.enabled = guiEnabled && hasValueProperty.boolValue;
            EditorGUI.PropertyField(valueRect, valueProperty, GUIContent.none);
            GUI.enabled = guiEnabled;

            // Restore indent
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
}