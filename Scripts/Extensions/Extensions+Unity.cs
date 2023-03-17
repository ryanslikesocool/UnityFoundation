using UnityEngine;

namespace Foundation {
    public static partial class Extensions {
        public static void DestroySafe(this Object _, Object obj) {
#if UNITY_EDITOR
            if (Application.isPlaying) {
                Object.Destroy(obj);
            } else {
                Object.DestroyImmediate(obj);
            }
#else
            Object.Destroy(obj);
#endif
        }
    }
}