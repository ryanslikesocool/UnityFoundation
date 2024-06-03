// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

#if UNITY_EDITOR
using System;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

namespace Foundation.Editors {
	[CustomPropertyDrawer(typeof(ClosedRange<Angle>))]
	internal sealed class ClosedRangeAngleDrawer : PropertyDrawer {
		private const float INFIX_WIDTH = 25;
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
			SerializedProperty lowerBoundProperty = property.FindPropertyRelative("lowerBound").FindPropertyRelative("_storage");
			SerializedProperty upperBoundProperty = property.FindPropertyRelative("upperBound").FindPropertyRelative("_storage");

			using (var scope = new EditorGUI.PropertyScope(position, label, property)) {
				// Draw label
				label = scope.content;
				position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

				using (new EditorGUI.IndentLevelScope(-EditorGUI.indentLevel)) {
					// Calculate rects
					float fieldWidth = (position.width - (INFIX_WIDTH + PICKER_WIDTH + SPACING * 3)) * 0.5f;

					Rect lowerBoundRect = new Rect(position.x, position.y, fieldWidth, position.height);
					float consumed = fieldWidth + SPACING;

					Rect infixRect = new Rect(position.x + consumed, position.y, INFIX_WIDTH, position.height);
					consumed += INFIX_WIDTH + SPACING;

					Rect upperBoundRect = new Rect(position.x + consumed, position.y, fieldWidth, position.height);

					consumed += upperBoundRect.width + SPACING;
					Rect pickerRect = new Rect(position.x + consumed, position.y, PICKER_WIDTH, position.height);

					// Draw fields - pass GUIContent.none to each so they are drawn without labels

					float intermediate = convertIn(lowerBoundProperty.floatValue);
					intermediate = EditorGUI.FloatField(lowerBoundRect, intermediate);
					lowerBoundProperty.floatValue = convertOut(intermediate);

					EditorGUI.LabelField(infixRect, ". . .", EditorStyles.boldLabel);

					intermediate = convertIn(upperBoundProperty.floatValue);
					intermediate = EditorGUI.FloatField(upperBoundRect, intermediate);
					upperBoundProperty.floatValue = convertOut(intermediate);

					mode = (Angle.Mode)EditorGUI.EnumPopup(pickerRect, mode);
				}
			}
		}
	}
}
#endif