// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEditor;
using UnityEngine;

namespace Foundation.Editor {
	[CustomPropertyDrawer(typeof(ClosedRange<Angle>))]
	internal sealed class ClosedRangeAngleDrawer : PropertyDrawer {
		private Angle.Mode mode = Angle.Mode.Degrees;

		// MARK: - GUI

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			SerializedProperty lowerBoundProperty = property.FindPropertyRelative(LOWER_BOUND_PROPERTY_NAME).FindPropertyRelative("_storage");
			SerializedProperty upperBoundProperty = property.FindPropertyRelative(UPPER_BOUND_PROPERTY_NAME).FindPropertyRelative("_storage");

			using (var scope = new EditorGUI.PropertyScope(position, label, property)) {
				// Draw label
				label = scope.content;
				position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

				using (new EditorGUI.IndentLevelScope(-EditorGUI.indentLevel)) {
					float spacing = EditorGUIUtility.standardVerticalSpacing;

					// Calculate rects
					Rect rangeFieldRect = position;
					rangeFieldRect.width -= PICKER_WIDTH + spacing;
					Root_RangeDrawer.CalculateRects(rangeFieldRect, ClosedRangeDrawer.InfixLabelContent, out Rect infixLabelRect, out Rect lowerBoundRect, out Rect upperBoundRect);

					Rect pickerRect = position;
					pickerRect.width = PICKER_WIDTH;
					pickerRect.x += rangeFieldRect.width + spacing;

					// Draw
					AngleDrawer.OnAngleFieldGUI(lowerBoundRect, lowerBoundProperty, mode);
					ClosedRangeDrawer.DrawInfixLabel(infixLabelRect);
					AngleDrawer.OnAngleFieldGUI(upperBoundRect, upperBoundProperty, mode);

					AngleDrawer.OnAngleModePickerGUI(pickerRect, ref mode);
				}
			}
		}

		// MARK: - Constants

		private const float PICKER_WIDTH = AngleDrawer.PICKER_WIDTH;

		private const string LOWER_BOUND_PROPERTY_NAME = ClosedRangeDrawer.LOWER_BOUND_PROPERTY_NAME;
		private const string UPPER_BOUND_PROPERTY_NAME = ClosedRangeDrawer.UPPER_BOUND_PROPERTY_NAME;
	}
}