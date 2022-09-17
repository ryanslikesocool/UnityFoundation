using UnityEngine;

namespace Foundation {
    public class Foundation : MonoBehaviour {
        private static Foundation shared = null;
        public static Foundation Shared {
            get {
                if (shared == null) {
                    GameObject container = new GameObject("Foundation Container");
                    shared = container.AddComponent<Foundation>();
                    container.hideFlags = HideFlags.HideAndDontSave;
                }
                return shared;
            }
        }

        private void OnApplicationQuit() {
            shared = null;
        }
    }
}