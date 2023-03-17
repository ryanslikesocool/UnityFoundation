using UnityEngine;

namespace Foundation {
    public static partial class Extensions {
        public static bool Raycast<T>(UnityEngine.Ray ray, out T component) where T : MonoBehaviour {
            if (Physics.Raycast(ray, out UnityEngine.RaycastHit hit)) {
                if (hit.collider.TryGetComponent<T>(out component)) {
                    return true;
                }
            }
            component = null;
            return false;
        }
    }
}