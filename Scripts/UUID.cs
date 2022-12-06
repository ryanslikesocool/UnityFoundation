using System;

namespace Foundation {
    [Serializable]
#if ODIN_INSPECTOR_3
    [Sirenix.OdinInspector.InlineProperty]
#endif
    public readonly struct UUID : Hashable, IEquatable<UUID> {
        public static UUID Empty = new UUID(Guid.Empty);

        [UnityEngine.SerializeField]
#if ODIN_INSPECTOR_3
        [Sirenix.OdinInspector.HideLabel]
#endif
        private readonly Guid value;

        public string uuidString => value.ToString();

        public UUID(byte a, byte b, byte c, byte d, byte e, byte f, byte g, byte h, byte i, byte j, byte k) {
            value = new Guid(a, b, c, d, e, f, g, h, i, j, k);
        }

        public UUID(in byte[] bytes) {
            value = new Guid(bytes);
        }

        public UUID(in Guid value) {
            this.value = value;
        }

        public static UUID Create() => new UUID(Guid.NewGuid());

        public UUID(in string uuidString) {
            value = new Guid(uuidString);
        }

        public bool Equals(UUID other) => value == other.value;

        public override bool Equals(object other) {
            if (other is UUID) {
                return ((UUID)other).value == value;
            } else {
                return false;
            }
        }

        public override int GetHashCode() => ((Hashable)this).hashValue;

        public static bool operator ==(UUID a, UUID b) => a.Equals(b);
        public static bool operator !=(UUID a, UUID b) => !a.Equals(b);

        public void Hash(ref Hasher value) {
            value.Combine(value);
        }
    }
}