using UnityEngine;

namespace Foundation {
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
        internal static T _shared = null;

        public static T Shared {
            get {
                if (_shared == null) {
                    _shared = FindObjectOfType<T>();
                }
                return _shared;
            }
        }

        [SerializeField] private bool persistent = false;

        protected virtual void Awake() {
            if (_shared != null && _shared != this) {
                Destroy(this);
                return;
            }

            _shared = this as T;

            if (persistent) {
                DontDestroyOnLoad(gameObject);
            }
        }

        protected virtual void OnApplicationQuit() {
            _shared = null;
        }
    }
}