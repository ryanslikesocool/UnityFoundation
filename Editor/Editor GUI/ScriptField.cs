// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using System;
using UnityEditor;
using UnityEngine;

namespace Foundation.Editors {
	public static partial class FoundationEditorGUI {
		// based on https://discussions.unity.com/t/how-to-show-the-standard-script-line-with-a-custom-editor/170088/5
		public static void ScriptField<T>(T target, bool disabled = true) where T : UnityEngine.Object {
			MonoScript targetScript = target switch {
				MonoBehaviour monoBehaviour => MonoScript.FromMonoBehaviour(monoBehaviour),
				ScriptableObject scriptableObject => MonoScript.FromScriptableObject(scriptableObject),
				_ => throw new NotImplementedException()
			};

			using (new EditorGUI.DisabledScope(disabled)) {
				EditorGUILayout.ObjectField("Script", targetScript, typeof(T), false);
			}
		}
	}
}