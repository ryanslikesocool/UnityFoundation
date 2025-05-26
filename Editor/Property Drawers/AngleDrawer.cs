// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using System;
using UnityEditor;
using UnityEngine;

namespace Foundation.Editors {
	[CustomPropertyDrawer(typeof(Angle))]
	internal sealed class AngleDrawer : PropertyDrawer {
		private Angle.Mode mode = Angle.Mode.Degrees;

		// MARK: - GUI

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			SerializedProperty storageProperty = property.FindPropertyRelative("_storage");

			using (var scope = new EditorGUI.PropertyScope(position, label, property)) {
				// Draw label
				label = scope.content;
				position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

				using (new EditorGUI.IndentLevelScope(-EditorGUI.indentLevel)) {
					float spacing = EditorGUIUtility.standardVerticalSpacing;

					Rect valueRect = new Rect(position.x, position.y, position.width - (PICKER_WIDTH + spacing), position.height);
					float consumed = valueRect.width + spacing;
					Rect pickerRect = new Rect(position.x + consumed, position.y, PICKER_WIDTH, position.height);

					OnAngleFieldGUI(valueRect, storageProperty, mode);

					OnAngleModePickerGUI(pickerRect, ref mode);
				}
			}
		}

		public static void OnAngleFieldGUI(Rect position, SerializedProperty property, Angle.Mode mode) {
			Func<float, float> convertIn = Angle.ConversionFunction(Angle.Mode.Radians, mode);
			Func<float, float> convertOut = Angle.ConversionFunction(mode, Angle.Mode.Radians);

			float intermediate = convertIn(property.floatValue);
			intermediate = EditorGUI.FloatField(position, intermediate);
			property.floatValue = convertOut(intermediate);
		}

		public static void OnAngleModePickerGUI(Rect position, ref Angle.Mode mode) {
			mode = (Angle.Mode)EditorGUI.EnumPopup(position, mode);
		}

		// MARK: - Constants

		public const float PICKER_WIDTH = 70;
	}
}