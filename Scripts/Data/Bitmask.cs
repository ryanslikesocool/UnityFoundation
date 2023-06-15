using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Foundation {
    [Serializable]
    public struct Bitmask : IEquatable<Bitmask> {
        public uint backing;

        public bool this[int index] {
            get => Contains((uint)(1 << index));
            set {
                uint flag = (uint)(1 << index);
                switch ((value: value, flag: Contains(flag))) {
                    case { value: true, flag: false }:
                        Insert(flag);
                        break;
                    case { value: false, flag: true }:
                        Remove(flag);
                        break;
                    default: // no change
                        break;
                }
            }
        }

        public Bitmask(uint backing) {
            this.backing = backing;
        }

        public Bitmask(IEnumerable<bool> collection) {
            backing = 0;

            int counter = 0;
            foreach (bool item in collection) {
                if (item) {
                    backing |= (uint)(1 << counter);
                }
                counter++;
            }
        }

        public static readonly Bitmask Zero = new Bitmask(0);


        [MethodImpl(MethodImplOptions.AggressiveInlining)] public bool Contains(Bitmask flag) => this.Contains(flag.backing);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Insert(Bitmask flag) => this.Insert(flag.backing);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Remove(Bitmask flag) => this.Remove(flag.backing);

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public bool Contains(uint flag) => (backing & flag) != 0u;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Insert(uint flag) => backing |= flag;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Remove(uint flag) => backing &= ~flag;

        public static explicit operator uint(Bitmask mask) => mask.backing;
        public static explicit operator Bitmask(uint backing) => new Bitmask(backing);

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public bool Equals(Bitmask other) => backing == other.backing;

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator ==(Bitmask lhs, Bitmask rhs) => lhs.Equals(rhs);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator !=(Bitmask lhs, Bitmask rhs) => !lhs.Equals(rhs);

        public IEnumerable<uint> AsActiveFlags() {
            for (int i = 0; i < (sizeof(uint) * 8); i++) {
                uint flag = (uint)(1 << i);
                if (Contains(flag)) {
                    yield return flag;
                }
            }
        }

        public IEnumerable<int> AsActiveIndices() {
            for (int i = 0; i < (sizeof(uint) * 8); i++) {
                if (this[i]) {
                    yield return i;
                }
            }
        }

        public IEnumerable<bool> AsBools() {
            for (int i = 0; i < (sizeof(uint) * 8); i++) {
                yield return this[i];
            }
        }

        // MARK: - Override

        public override int GetHashCode() {
            return base.GetHashCode();
        }

        public override bool Equals(object obj) => obj switch {
            Bitmask other => this.Equals(other),
            uint otherBacking => this.backing == otherBacking,
            _ => false
        };
    }
}