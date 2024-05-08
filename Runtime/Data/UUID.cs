using System;
using UnityEngine;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace Foundation {
	/// <summary>
	/// A universally unique value to identify types, interfaces, and other items.
	/// </summary>
	[Serializable, StructLayout(LayoutKind.Sequential)]
	public struct UUID : IEquatable<UUID> {
		[SerializeField, HideInInspector] private byte a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p;

		/// <summary>
		/// The <see cref="string"/> representation of a <see cref="UUID"/>, such as <c>E621E1F8-C36C-495A-93FC-0C247A3E6E5F</c>.
		/// </summary>
		public readonly string uuidString => string.Format(STRING_FORMAT, a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p);

		// MARK: - Initialization

		[MethodImpl(AggressiveInlining)]
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

		[MethodImpl(AggressiveInlining)]
		public UUID((byte, byte, byte, byte, byte, byte, byte, byte, byte, byte, byte, byte, byte, byte, byte, byte) bytes) : this(
			bytes.Item1, bytes.Item2, bytes.Item3, bytes.Item4,
			bytes.Item5, bytes.Item6, bytes.Item7, bytes.Item8,
			bytes.Item9, bytes.Item10, bytes.Item11, bytes.Item12,
			bytes.Item13, bytes.Item14, bytes.Item15, bytes.Item16
		) { }

		[MethodImpl(AggressiveInlining)]
		public UUID(in byte[] bytes) : this(
			bytes[0], bytes[1], bytes[2], bytes[3],
			bytes[4], bytes[5], bytes[6], bytes[7],
			bytes[8], bytes[9], bytes[10], bytes[11],
			bytes[12], bytes[13], bytes[14], bytes[15]
		) { }

		[MethodImpl(AggressiveInlining)]
		public UUID(in Guid value) : this(value.ToByteArray()) { }

		/// <summary>
		/// Creates a <see cref="UUID"/> from a string representation.
		/// </summary>
		/// <param name="uuidString">The <see cref="string"/> representation of a <see cref="UUID"/>, such as <c>E621E1F8-C36C-495A-93FC-0C247A3E6E5F</c>.</param>
		[MethodImpl(AggressiveInlining)]
		public UUID(in string uuidString) : this(new Guid(uuidString)) { }

		[MethodImpl(AggressiveInlining)]
		public static UUID Create() => new UUID(Guid.NewGuid());

		// MARK: -

		[MethodImpl(AggressiveInlining)]
		public readonly byte[] ToByteArray() => new byte[16] { a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p };

		// MARK: - Override

		[MethodImpl(AggressiveInlining)]
		public readonly override bool Equals(object other) => other switch {
			UUID uuid => Equals(uuid),
			_ => false
		};

		[MethodImpl(AggressiveInlining)]
		public readonly override int GetHashCode() => (a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p).GetHashCode();

		// MARK: - IEquatable

		[MethodImpl(AggressiveInlining)]
		public readonly bool Equals(UUID other)
			=> a == other.a
			&& b == other.b
			&& c == other.c
			&& d == other.d
			&& e == other.e
			&& f == other.f
			&& g == other.g
			&& h == other.h
			&& i == other.i
			&& j == other.j
			&& k == other.k
			&& l == other.l
			&& m == other.m
			&& n == other.n
			&& o == other.o
			&& p == other.p;

		[MethodImpl(AggressiveInlining)]
		public static bool operator ==(UUID a, UUID b)
			=> a.Equals(b);

		[MethodImpl(AggressiveInlining)]
		public static bool operator !=(UUID a, UUID b)
			=> !a.Equals(b);

		// MARK: - Constants

		/// <summary>
		/// A constant UUID with all bytes set to zero.
		/// </summary>
		public static readonly UUID Zero = new UUID(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

		private const string STRING_FORMAT = "{0:X02}{1:X02}{2:X02}{3:X02}-{4:X02}{5:X02}-{6:X02}{7:X02}-{8:X02}{9:X02}-{10:X02}{11:X02}{12:X02}{13:X02}{14:X02}{15:X02}";
	}
}