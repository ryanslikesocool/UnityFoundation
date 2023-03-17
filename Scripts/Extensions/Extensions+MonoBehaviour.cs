using UnityEngine;

namespace Foundation {
    public static partial class Extensions {
        public static void StopCoroutineSafe(this MonoBehaviour mb, Coroutine coroutine) {
            if (coroutine != null) {
                mb.StopCoroutine(coroutine);
                coroutine = null;
            }
        }
    }
}