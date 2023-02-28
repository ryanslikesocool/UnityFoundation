using UnityEditor;
using UnityEngine;

namespace Foundation.Editors {
    [CustomPropertyDrawer(typeof(Range<>))]
    internal sealed class RangeDrawer : PropertyDrawer {
        private const float INFIX_WIDTH = 25;
        private const float SPACING = 5;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            SerializedProperty lowerBoundProperty = property.FindPropertyRelative("lowerBound");
            SerializedProperty upperBoundProperty = property.FindPropertyRelative("upperBound");

            EditorGUI.BeginProperty(position, label, property);

            // Draw label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            // Clear indent
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            // Calculate rects
            float fieldWidth = (position.width - INFIX_WIDTH) * 0.5f - SPACING;

            Rect lowerBoundRect = new Rect(position.x, position.y, fieldWidth, position.height);
            float consumed = fieldWidth + SPACING;

            Rect infixRect = new Rect(position.x + consumed, position.y, INFIX_WIDTH, position.height);
            consumed += INFIX_WIDTH + SPACING;

            Rect upperBoundRect = new Rect(position.x + consumed, position.y, fieldWidth, position.height);

            // Draw
            EditorGUI.PropertyField(lowerBoundRect, lowerBoundProperty, GUIContent.none);
            EditorGUI.LabelField(infixRect, ". . <", EditorStyles.boldLabel);
            EditorGUI.PropertyField(upperBoundRect, upperBoundProperty, GUIContent.none);

            // Restore indent
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
}