using System;
using Random = UnityEngine.Random;

namespace Foundation {
    [Serializable]
    public struct UUID : Hashable, IEquatable<UUID> {
        public static UUID Zero = new UUID();

        public readonly byte[] bytes;

        public string uuidString {
            get {
                string strHex = BitConverter.ToString(bytes).Replace("-", "");
                return strHex.Substring(0, 8) + "-" + strHex.Substring(8, 4) + "-" + strHex.Substring(12, 4) + "-" + strHex.Substring(16, 4) + "-" + strHex.Substring(20, 12);
            }
        }

        public UUID(byte a, byte b, byte c, byte d, byte e, byte f, byte g, byte h, byte i, byte j, byte k, byte l, byte m, byte n, byte o, byte p) {
            this.bytes = new byte[16] { a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p };
        }

        public UUID(byte[] bytes) {
            if (bytes.Length != 16) {
                throw new System.ArgumentException("The provided byte array is the wrong size.  The byte array must contain exactly 16 bytes");
            }
            this.bytes = bytes;
        }

        public static UUID Create() {
            byte[] bytes = new byte[16];
            for (int i = 0; i < 16; i++) {
                bytes[i] = (byte)Random.Range(0, 256);
            }

            bytes[6] = (byte)(0x40 | ((int)bytes[6] & 0xf));
            bytes[8] = (byte)(0x80 | ((int)bytes[8] & 0x3f));

            return new UUID(bytes);
        }

        /*
        public UUID(string uuidString) {
            value = new Guid(uuidString);
        }
        */

        public bool Equals(UUID other) => bytes == other.bytes;

        public override bool Equals(object other) {
            if (other is UUID) {
                return ((UUID)other).bytes == bytes;
            } else {
                return false;
            }
        }

        public override int GetHashCode() => bytes.GetHashCode();

        public static bool operator ==(UUID a, UUID b) => a.Equals(b);
        public static bool operator !=(UUID a, UUID b) => !a.Equals(b);

        public void Hash(ref Hasher value) {
            value.Combine(bytes);
        }
    }
}