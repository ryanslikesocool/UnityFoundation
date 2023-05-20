using System;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

namespace Foundation.Editors {
    [CustomPropertyDrawer(typeof(Angle))]
    internal sealed class AngleDrawer : PropertyDrawer {
        private const float SPACING = 4;
        private const float PICKER_WIDTH = 70;

        private Angle.Mode mode = Angle.Mode.Degrees;

        private Func<float, float> convertIn => mode switch {
            Angle.Mode.Radians => (v) => v,
            Angle.Mode.Degrees => (v) => math.degrees(v),
            Angle.Mode.Turns => (v) => v / (math.PI * 2.0f),
            _ => null
        };
        private Func<float, float> convertOut => mode switch {
            Angle.Mode.Radians => (v) => v,
            Angle.Mode.Degrees => (v) => math.radians(v),
            Angle.Mode.Turns => (v) => v * (math.PI * 2.0f),
            _ => null
        };

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            SerializedProperty storageProperty = property.FindPropertyRelative("_storage");

            EditorGUI.BeginProperty(position, label, property);

            // Draw label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            // Clear indent
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            // Calculate rects
            Rect valueRect = new Rect(position.x, position.y, position.width - (PICKER_WIDTH + SPACING), position.height);
            float consumed = valueRect.width + SPACING;
            Rect pickerRect = new Rect(position.x + consumed, position.y, PICKER_WIDTH, position.height);

            // Draw fields - pass GUIContent.none to each so they are drawn without labels
            float intermediate = convertIn(storageProperty.floatValue);
            intermediate = EditorGUI.FloatField(valueRect, intermediate);
            storageProperty.floatValue = convertOut(intermediate);

            mode = (Angle.Mode)EditorGUI.EnumPopup(pickerRect, mode);

            // Restore indent
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
}