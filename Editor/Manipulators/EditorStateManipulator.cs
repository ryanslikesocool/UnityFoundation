using UnityEditor;
using UnityEngine.UIElements;

namespace Foundation.Editor {
	public sealed class EditorStateManipulator : Manipulator {
		public bool activeInEditMode;
		public bool activeInPlayMode;

		// MARK: - Lifecycle

		public EditorStateManipulator(bool activeInEditMode, bool activeInPlayMode) {
			this.activeInEditMode = activeInEditMode;
			this.activeInPlayMode = activeInPlayMode;
		}

		// MARK: - Manipulator

		protected override void RegisterCallbacksOnTarget() {
			EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
		}

		protected override void UnregisterCallbacksFromTarget() {
			EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
		}

		// MARK: - Events

		private void OnPlayModeStateChanged(PlayModeStateChange state) {
			bool isEnabled;
			switch (state) {
				case PlayModeStateChange.EnteredEditMode:
					isEnabled = activeInEditMode;
					break;
				case PlayModeStateChange.EnteredPlayMode:
					isEnabled = activeInPlayMode;
					break;
				default:
					return;
			}

			target.SetEnabled(isEnabled);
		}
	}
}