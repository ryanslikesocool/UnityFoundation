using System;

namespace Foundation {
    /// <summary>
    /// A universally unique value to identify types, interfaces, and other items.
    /// </summary>
    [Serializable]
#if ODIN_INSPECTOR_3
    [Sirenix.OdinInspector.InlineProperty]
#endif
    public struct UUID : IEquatable<UUID>, IHashable {
        public static UUID Empty = new UUID(Guid.Empty);

        [UnityEngine.SerializeField, UnityEngine.HideInInspector]
#if ODIN_INSPECTOR_3
        [Sirenix.OdinInspector.HideLabel]
#endif
        private byte[] value;

#if ODIN_INSPECTOR_3
        [Sirenix.OdinInspector.ShowInInspector, Sirenix.OdinInspector.ReadOnly, Sirenix.OdinInspector.HideLabel]
#endif
        public string uuidString => new Guid(value).ToString();

        public UUID(byte a, byte b, byte c, byte d, byte e, byte f, byte g, byte h, byte i, byte j, byte k, byte l, byte m, byte n, byte o, byte p) {
            value = new byte[] { a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p };
        }

        public UUID(in byte[] bytes) {
            value = bytes;
        }

        public UUID(in Guid value) {
            this.value = value.ToByteArray();
        }

        public static UUID Create() => new UUID(Guid.NewGuid());

        /// <summary>
        /// Creates a <see cref="UUID"/> from a string representation.
        /// </summary>
        /// <param name="uuidString">The <see langword="string"/> representation of a <see cref="UUID"/>, such as <c>E621E1F8-C36C-495A-93FC-0C247A3E6E5F</c>.</param>
        public UUID(in string uuidString) {
            value = new Guid(uuidString).ToByteArray();
        }

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

        public void Hash(ref Hasher value) {
            value.Combine(this.value);
        }
    }
}