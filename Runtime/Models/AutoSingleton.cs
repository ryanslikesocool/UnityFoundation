using UnityEngine;

namespace Foundation {
    public abstract class AutoSingleton<T> : Singleton<T> where T : MonoBehaviour {
        public static new T Shared {
            get {
                if (_shared == null) {
                    _shared = FindObjectOfType<T>();
                }
                if (_shared == null) {
                    GameObject singletonObject = new GameObject();
                    singletonObject.name = string.Format("Singleton<{0}>", typeof(T));
                    _shared = singletonObject.AddComponent<T>();
                }
                return _shared;
            }
        }
    }
}