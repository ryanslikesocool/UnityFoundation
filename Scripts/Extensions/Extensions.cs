using System;
using UnityEngine;

namespace Foundation {
    public static partial class Extensions {
        public static void DestroySafe(UnityEngine.Object obj) {
#if UNITY_EDITOR
            if (Application.isPlaying) {
                UnityEngine.Object.Destroy(obj);
            } else {
                UnityEngine.Object.DestroyImmediate(obj);
            }
#else
            UnityEngine.Object.Destroy(obj);
#endif
        }
    }
}