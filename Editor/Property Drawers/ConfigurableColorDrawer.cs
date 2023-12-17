#if UNITY_EDITOR
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

namespace Foundation.Editors {
	[CustomPropertyDrawer(typeof(ConfigurableColorAttribute))]
	internal sealed class ConfigurableColorDrawer : PropertyDrawer {
		private const float SPACING = 4;
		private const float OPTIONS_WIDTH = 20;

		private bool isCreated = false;
		private ConfigurableColorAttribute.Options options = ConfigurableColorAttribute.Options.Eyedropper;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			if (!isCreated) {
				float4 float4Color = property.colorValue.AsFloat4();
				if (float4Color.w != 0f && float4Color.w != 1f) {
					options |= ConfigurableColorAttribute.Options.Alpha;
				}
				if (math.any(float4Color < 0f) || math.any(float4Color > 1f)) {
					options |= ConfigurableColorAttribute.Options.HDR;
				}
				isCreated = true;
			}

			using (var scope = new EditorGUI.PropertyScope(position, label, property)) {
				// Draw label
				label = scope.content;
				position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

				using (new EditorGUI.IndentLevelScope(-EditorGUI.indentLevel)) {
					// Calculate rects
					Rect colorRect = new Rect(position.x, position.y, position.width - (OPTIONS_WIDTH + SPACING), position.height);
					float consumed = colorRect.width + SPACING;
					Rect optionsRect = new Rect(position.x + consumed, position.y, OPTIONS_WIDTH, position.height);

					// Draw
					property.colorValue = EditorGUI.ColorField(
						colorRect,
						GUIContent.none,
						property.colorValue,
						showEyedropper: options.HasFlag(ConfigurableColorAttribute.Options.Eyedropper),
						showAlpha: options.HasFlag(ConfigurableColorAttribute.Options.Alpha),
						hdr: options.HasFlag(ConfigurableColorAttribute.Options.HDR)
					);
					options = (ConfigurableColorAttribute.Options)EditorGUI.EnumFlagsField(optionsRect, options);
				}
			}
		}
	}
}
#endif