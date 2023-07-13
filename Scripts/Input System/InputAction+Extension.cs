#if UNITY_INPUT_SYSTEM
using UnityEngine.InputSystem;

namespace Foundation {
    public static class InputActionExtensions {
        public static void SetActive(this InputAction inputAction, bool state) {
            if (inputAction.enabled == state) {
                return;
            }

            if (state) {
                inputAction.Enable();
            } else {
                inputAction.Disable();
            }
        }
    }
}
#endif