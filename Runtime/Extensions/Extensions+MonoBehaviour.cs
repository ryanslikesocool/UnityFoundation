using UnityEngine;

namespace Foundation {
    public static partial class Extensions {
        public static void StopCoroutineSafe(this MonoBehaviour mb, Coroutine coroutine) {
            if (coroutine != null) {
                mb.StopCoroutine(coroutine);
                coroutine = null;
            }
        }

        public static void SetLayerRecursively(this GameObject obj, int newLayer) {
            if (null == obj) {
                return;
            }

            obj.layer = newLayer;

            foreach (Transform child in obj.transform) {
                if (null == child) {
                    continue;
                }
                SetLayerRecursively(child.gameObject, newLayer);
            }
        }
    }
}