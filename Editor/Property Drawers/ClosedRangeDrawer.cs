// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEditor;
using UnityEngine;

namespace Foundation.Editors {
	[CustomPropertyDrawer(typeof(ClosedRange<>))]
	internal sealed class ClosedRangeDrawer : Root_RangeDrawer {
		protected override GUIContent InfixLabel => InfixLabelContent;
		protected override string LowerBoundPropertyName => LOWER_BOUND_PROPERTY_NAME;
		protected override string UpperBoundPropertyName => UPPER_BOUND_PROPERTY_NAME;

		// MARK: - Utility

		public static void DrawInfixLabel(Rect position, GUIStyle style)
			=> EditorGUI.LabelField(position, InfixLabelContent, style);

		public static void DrawInfixLabel(Rect position)
			=> DrawInfixLabel(position, InfixLabelStyle);

		public static float CalcInfixLabelWidth(GUIStyle style)
			=> style.CalcSize(InfixLabelContent).x;

		public static float CalcInfixLabelWidth()
			=> CalcInfixLabelWidth(InfixLabelStyle);

		// MARK: - Constants

		public static readonly GUIContent InfixLabelContent = new GUIContent(". . .");

		public const string LOWER_BOUND_PROPERTY_NAME = nameof(ClosedRange<byte>.lowerBound);
		public const string UPPER_BOUND_PROPERTY_NAME = nameof(ClosedRange<byte>.upperBound);
	}
}