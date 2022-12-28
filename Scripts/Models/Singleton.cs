using UnityEngine;

namespace Foundation {
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
        private static T _shared = null;

        public static T Shared {
            get {
                if (_shared == null) {
                    _shared = GameObject.FindObjectOfType<T>();
                    if (_shared == null) {
                        GameObject singletonObject = new GameObject();
                        singletonObject.name = string.Format("Singleton<{0}>", typeof(T));
                        _shared = singletonObject.AddComponent<T>();
                    }
                }
                return _shared;
            }
        }

        [SerializeField] private bool persistent = false;

        protected virtual void Awake() {
            if (_shared != null) {
                Destroy(gameObject);
                return;
            }

            _shared = this as T;//GetComponent<T>();

            if (persistent) {
                DontDestroyOnLoad(gameObject);
            }
        }

        protected virtual void OnApplicationQuit() {
            _shared = null;
        }
    }
}