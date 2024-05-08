using UnityEditor;
using UnityEngine;
//using UnityEditor.UIElements;
//using UnityEngine.UIElements;

namespace Foundation.Editors {
	[CustomPropertyDrawer(typeof(Range<>))]
	internal sealed class RangeDrawer : PropertyDrawer {

		// MARK: - IMGUI

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			SerializedProperty lowerBoundProperty = property.FindPropertyRelative(PROPERTY_LOWER_BOUND);
			SerializedProperty upperBoundProperty = property.FindPropertyRelative(PROPERTY_UPPER_BOUND);

			using (var scope = new EditorGUI.PropertyScope(position, label, property)) {
				// Draw label
				label = scope.content;
				position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

				using (new EditorGUI.IndentLevelScope(-EditorGUI.indentLevel)) {
					// Calculate rects
					float fieldWidth = (position.width - INFIX_WIDTH) * 0.5f - SPACING;

					Rect lowerBoundRect = new Rect(position.x, position.y, fieldWidth, position.height);
					float consumed = fieldWidth + SPACING;

					Rect infixRect = new Rect(position.x + consumed, position.y, INFIX_WIDTH, position.height);
					consumed += INFIX_WIDTH + SPACING;

					Rect upperBoundRect = new Rect(position.x + consumed, position.y, fieldWidth, position.height);

					// Draw
					EditorGUI.PropertyField(lowerBoundRect, lowerBoundProperty, GUIContent.none);
					EditorGUI.LabelField(infixRect, ". . <");
					EditorGUI.PropertyField(upperBoundRect, upperBoundProperty, GUIContent.none);
				}
			}
		}

		// MARK: - UITK

		//		public override VisualElement CreatePropertyGUI(SerializedProperty property) {
		//			VisualElement container = new VisualElement();
		//			container.style.flexDirection = FlexDirection.Row;
		//			container.style.alignItems = Align.Stretch;
		//
		//			Label label = new Label(property.displayName);
		//			label.style.flexGrow = 1f;
		//
		//			VisualElement fieldContent = new VisualElement();
		//			fieldContent.style.flexDirection = FlexDirection.Row;
		//			fieldContent.style.flexGrow = 1f;
		//
		//			PropertyField lowerBoundField = new PropertyField(property.FindPropertyRelative(PROPERTY_LOWER_BOUND), string.Empty);
		//			lowerBoundField.style.flexGrow = 1f;
		//
		//			PropertyField upperBoundField = new PropertyField(property.FindPropertyRelative(PROPERTY_UPPER_BOUND), string.Empty);
		//			upperBoundField.style.flexGrow = 1f;
		//
		//			Label infix = new Label(". . <");
		//
		//			fieldContent.Add(lowerBoundField);
		//			fieldContent.Add(infix);
		//			fieldContent.Add(upperBoundField);
		//
		//			container.Add(label);
		//			container.Add(fieldContent);
		//
		//			return container;
		//		}

		// MARK: - Constants

		private const float INFIX_WIDTH = 25;
		private const float SPACING = 5;

		private const string PROPERTY_LOWER_BOUND = "lowerBound";
		private const string PROPERTY_UPPER_BOUND = "upperBound";
	}
}