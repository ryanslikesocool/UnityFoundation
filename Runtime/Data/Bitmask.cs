using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Foundation {
	[Serializable]
	public struct Bitmask : IEquatable<Bitmask> {
		public uint rawValue;

		public bool this[int index] {
			readonly get => Contains((uint)(1 << index));
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

		public Bitmask(uint rawValue) {
			this.rawValue = rawValue;
		}

		public Bitmask(in IEnumerable<bool> collection) {
			rawValue = 0;

			int counter = 0;
			foreach (bool item in collection) {
				if (item) {
					rawValue |= (uint)(1 << counter);
				}
				counter++;
			}
		}

		public static readonly Bitmask None = new Bitmask(0);

		[MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly bool Contains(Bitmask flag) => this.Contains(flag.rawValue);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public void Insert(Bitmask flag) => this.Insert(flag.rawValue);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public void Remove(Bitmask flag) => this.Remove(flag.rawValue);

		[MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly bool Contains(uint flag) => (rawValue & flag) != 0u;
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public void Insert(uint flag) => rawValue |= flag;
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public void Remove(uint flag) => rawValue &= ~flag;

		public static explicit operator uint(Bitmask mask) => mask.rawValue;
		public static explicit operator Bitmask(uint rawValue) => new Bitmask(rawValue);

		[MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly bool Equals(Bitmask other) => rawValue == other.rawValue;

		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator ==(Bitmask lhs, Bitmask rhs) => lhs.Equals(rhs);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator !=(Bitmask lhs, Bitmask rhs) => !lhs.Equals(rhs);

		public readonly IEnumerable<uint> AsActiveFlags() {
			for (int i = 0; i < (sizeof(uint) * 8); i++) {
				uint flag = (uint)(1 << i);
				if (Contains(flag)) {
					yield return flag;
				}
			}
		}

		public readonly IEnumerable<int> AsActiveIndices() {
			for (int i = 0; i < (sizeof(uint) * 8); i++) {
				if (this[i]) {
					yield return i;
				}
			}
		}

		public readonly IEnumerable<bool> AsBools() {
			for (int i = 0; i < (sizeof(uint) * 8); i++) {
				yield return this[i];
			}
		}

		// MARK: - Override

		public readonly override int GetHashCode() {
			return base.GetHashCode();
		}

		public readonly override bool Equals(object obj) => obj switch {
			Bitmask other => this.Equals(other),
			uint otherRawValue => this.rawValue == otherRawValue,
			_ => false
		};
	}
}