using System;
using UnityEngine;
using System.Runtime.InteropServices;

//#if ODIN_INSPECTOR_3
//using Sirenix.OdinInspector;
//#endif

namespace Foundation {
	/// <summary>
	/// A universally unique value to identify types, interfaces, and other items.
	/// </summary>
	[Serializable, StructLayout(LayoutKind.Sequential)]
	//#if ODIN_INSPECTOR_3
	//	[InlineProperty]
	//#endif
	public struct UUID : IEquatable<UUID>, IHashable {
		[SerializeField, HideInInspector]
		private byte a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p;

		//#if ODIN_INSPECTOR_3
		//		[ShowInInspector, ReadOnly, HideLabel]
		//#endif
		public readonly string uuidString => String.Format(UUID.STRING_FORMAT, a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p);

		public UUID(byte a, byte b, byte c, byte d, byte e, byte f, byte g, byte h, byte i, byte j, byte k, byte l, byte m, byte n, byte o, byte p) {
			this.a = a;
			this.b = b;
			this.c = c;
			this.d = d;
			this.e = e;
			this.f = f;
			this.g = g;
			this.h = h;
			this.i = i;
			this.j = j;
			this.k = k;
			this.l = l;
			this.m = m;
			this.n = n;
			this.o = o;
			this.p = p;
		}

		public UUID(in byte[] bytes) : this(
			bytes[0], bytes[1], bytes[2], bytes[3],
			bytes[4], bytes[5], bytes[6], bytes[7],
			bytes[8], bytes[9], bytes[10], bytes[11],
			bytes[12], bytes[13], bytes[14], bytes[15]
		) { }

		public UUID(in Guid value) : this(value.ToByteArray()) { }

		public static UUID Create() => new UUID(Guid.NewGuid());

		/// <summary>
		/// Creates a <see cref="UUID"/> from a string representation.
		/// </summary>
		/// <param name="uuidString">The <see langword="string"/> representation of a <see cref="UUID"/>, such as <c>E621E1F8-C36C-495A-93FC-0C247A3E6E5F</c>.</param>
		public UUID(in string uuidString) : this(new Guid(uuidString)) { }

		public byte[] ToByteArray() => new byte[16] { a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p };

		// MARK: - Override

		public readonly override bool Equals(object other) => other switch {
			UUID uuid => this.Equals(uuid),
			_ => false
		};

		public readonly override int GetHashCode() => (a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p).GetHashCode();

		// MARK: - IEquatable

		public readonly bool Equals(UUID other)
			=> this.a == other.a
			&& this.b == other.b
			&& this.c == other.c
			&& this.d == other.d
			&& this.e == other.e
			&& this.f == other.f
			&& this.g == other.g
			&& this.h == other.h
			&& this.i == other.i
			&& this.j == other.j
			&& this.k == other.k
			&& this.l == other.l
			&& this.m == other.m
			&& this.n == other.n
			&& this.o == other.o
			&& this.p == other.p;

		public static bool operator ==(UUID a, UUID b) => a.Equals(b);
		public static bool operator !=(UUID a, UUID b) => !a.Equals(b);

		public readonly void Hash(ref Hasher value) {
			value.Combine(this.a);
			value.Combine(this.b);
			value.Combine(this.c);
			value.Combine(this.d);
			value.Combine(this.e);
			value.Combine(this.f);
			value.Combine(this.g);
			value.Combine(this.h);
			value.Combine(this.i);
			value.Combine(this.j);
			value.Combine(this.k);
			value.Combine(this.l);
			value.Combine(this.m);
			value.Combine(this.n);
			value.Combine(this.o);
			value.Combine(this.p);
		}

		// MARK: - Constants

		public static UUID Empty = new UUID(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
		private const string STRING_FORMAT = "{0:X02}{1:X02}{2:X02}{3:X02}-{4:X02}{5:X02}-{6:X02}{7:X02}-{8:X02}{9:X02}-{10:X02}{11:X02}{12:X02}{13:X02}{14:X02}{15:X02}";
	}
}