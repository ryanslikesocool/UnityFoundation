// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using System;
using UnityEditor;
using UnityEngine;

namespace Foundation.Editors {
	/// <summary>
	/// Draws a ColorMask enum field.
	/// Usage: [ColorMask] _ColorMask("Color Mask", Int) = 15
	/// </summary>
	internal sealed class ColorMaskDrawer : MaterialPropertyDrawer {
		internal enum ColorMask : int {
			[InspectorName("0")] Nothing = 0,
			R = 1 << 3, // 8
			G = 1 << 2, // 4
			B = 1 << 1, // 2
			A = 1 << 0, // 1 or 16, depending on value on the right
			[InspectorName("RGBA")] Everything = ~0,
			[Obsolete] RGBA = R | G | B | A, // not actually obsolete.  this is the only way to hide the value in the inspector
		}

		public override void OnGUI(Rect position, MaterialProperty prop, GUIContent label, MaterialEditor editor) {
			if (
				prop.type == MaterialProperty.PropType.Float
				|| prop.type == MaterialProperty.PropType.Range
				|| prop.type == MaterialProperty.PropType.Int
			) {
				EditorGUIUtility.labelWidth = 0f;
				EditorGUIUtility.fieldWidth = 0f;

				if (!EditorGUIUtility.wideMode) {
					EditorGUIUtility.wideMode = true;
					EditorGUIUtility.labelWidth = EditorGUIUtility.currentViewWidth - 212;
				}

				EditorGUI.BeginChangeCheck();
				EditorGUI.showMixedValue = prop.hasMixedValue;


				ColorMask mask = (ColorMask)prop.floatValue;
				mask = (ColorMask)EditorGUI.EnumFlagsField(position, label, mask);

				// fix for Unity's EnumFlagsField using ~0 for FlagEnum.Everything.
				if ((int)mask == ~0) {
					// the value isn't actually obsolete, as explained at its declaration
#pragma warning disable CS0612
					mask = ColorMask.RGBA;
#pragma warning restore CS0612
				}

				if (EditorGUI.EndChangeCheck()) {
					prop.floatValue = (float)mask;
				}
			} else {
				editor.DefaultShaderProperty(prop, label.text);
			}
		}
	}
}