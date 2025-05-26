// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;
using UnityEditor;
//using UnityEngine.UIElements;
//using UnityEditor.UIElements;

namespace Foundation.Editors {
	internal abstract class Root_RangeDrawer : PropertyDrawer {
		protected abstract GUIContent InfixLabel { get; }
		protected abstract string LowerBoundPropertyName { get; }
		protected abstract string UpperBoundPropertyName { get; }

		// MARK: - IMGUI

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			SerializedProperty lowerBoundProperty = property.FindPropertyRelative(LowerBoundPropertyName);
			SerializedProperty upperBoundProperty = property.FindPropertyRelative(UpperBoundPropertyName);

			using (var scope = new EditorGUI.PropertyScope(position, label, property)) {
				// Draw label
				label = scope.content;
				position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

				using (new EditorGUI.IndentLevelScope(-EditorGUI.indentLevel)) {
					OnFieldGUI(position, InfixLabel, lowerBoundProperty, upperBoundProperty);
				}
			}
		}

		public static void OnFieldGUI(Rect position, GUIContent infixLabel, SerializedProperty lowerBound, SerializedProperty upperBound) {
			CalculateRects(position, infixLabel, out Rect infixLabelRect, out Rect lowerBoundRect, out Rect upperBoundRect);

			// Draw
			EditorGUI.PropertyField(lowerBoundRect, lowerBound, GUIContent.none);
			EditorGUI.LabelField(infixLabelRect, infixLabel, InfixLabelStyle);
			EditorGUI.PropertyField(upperBoundRect, upperBound, GUIContent.none);
		}

		public static void CalculateRects(Rect position, GUIContent infixLabel, out Rect infixLabelRect, out Rect lowerBoundRect, out Rect upperBoundRect) {
			float spacing = EditorGUIUtility.standardVerticalSpacing;
			float infixLabelWidth = InfixLabelStyle.CalcSize(infixLabel).x;

			float fieldWidth = (position.width - infixLabelWidth) * 0.5f - spacing;

			lowerBoundRect = new Rect(position.x, position.y, fieldWidth, position.height);
			float consumed = fieldWidth + spacing;

			infixLabelRect = new Rect(position.x + consumed, position.y, infixLabelWidth, position.height);
			consumed += infixLabelWidth + spacing;

			upperBoundRect = new Rect(position.x + consumed, position.y, fieldWidth, position.height);
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

		protected static GUIStyle InfixLabelStyle => EditorStyles.label;
	}
}