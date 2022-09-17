using System;
using UnityEngine;
#if ODIN_INSPECTOR_3
using Sirenix.OdinInspector;
#endif

namespace Foundation {
    [Serializable]
#if ODIN_INSPECTOR_3
    [InlineProperty]
#endif
    public struct UUID : Hashable, IEquatable<UUID> {
        public static UUID Zero = new UUID(Guid.Empty);

        [SerializeField]
#if ODIN_INSPECTOR_3
        [HideLabel]
#endif
        private Guid value;

        public string uuidString => value.ToString();

        public UUID(string uuidString) {
            value = new Guid(uuidString);
        }

        public UUID(byte[] bytes) {
            value = new Guid(bytes);
        }

        private UUID(Guid value) {
            this.value = value;
        }

        public static UUID Create() => new UUID(Guid.NewGuid());

        public bool Equals(UUID other) => value == other.value;
        public override bool Equals(object other) {
            if (other is UUID) {
                return ((UUID)other).value == value;
            } else {
                return false;
            }
        }

        public override int GetHashCode() => value.GetHashCode();

        public static bool operator ==(UUID a, UUID b) => a.Equals(b);
        public static bool operator !=(UUID a, UUID b) => !a.Equals(b);
    }
}