using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace Foundation {
	public readonly struct FourCharacterCode : IEquatable<FourCharacterCode> {
		public readonly uint rawValue;

		// MARK: - Lifecycle

		[MethodImpl(AggressiveInlining)]
		public FourCharacterCode(uint rawValue) {
			this.rawValue = rawValue;
		}

		public FourCharacterCode(byte a, byte b, byte c, byte d) {
			const int BYTE_BIT_WIDTH = 8;

			uint partialRawValue = uint.MinValue;
			partialRawValue = Reduce(partialRawValue, a);
			partialRawValue = Reduce(partialRawValue, b);
			partialRawValue = Reduce(partialRawValue, c);
			partialRawValue = Reduce(partialRawValue, d);

			this = new FourCharacterCode(rawValue: partialRawValue);

			[MethodImpl(AggressiveInlining)]
			static uint Reduce(uint partialResult, byte character)
				=> (partialResult << BYTE_BIT_WIDTH) + (uint)character;
		}

		[MethodImpl(AggressiveInlining)]
		public FourCharacterCode(char a, char b, char c, char d) : this(a: (byte)a, b: (byte)b, c: (byte)c, d: (byte)d) { }

		[MethodImpl(AggressiveInlining)]
		public FourCharacterCode(in IList<byte> bytes) {
			AssertCharacterCount(bytes.Count);
			this = new FourCharacterCode(a: bytes[0], b: bytes[1], c: bytes[2], d: bytes[3]);
		}

		[MethodImpl(AggressiveInlining)]
		public FourCharacterCode(in ReadOnlySpan<byte> byteSpan) {
			AssertCharacterCount(byteSpan.Length);
			this = new FourCharacterCode(a: byteSpan[0], b: byteSpan[1], c: byteSpan[2], d: byteSpan[3]);
		}

		[MethodImpl(AggressiveInlining)]
		public FourCharacterCode(in IList<char> chars) {
			AssertCharacterCount(chars.Count);
			this = new FourCharacterCode(a: chars[0], b: chars[1], c: chars[2], d: chars[3]);
		}

		[MethodImpl(AggressiveInlining)]
		public FourCharacterCode(in ReadOnlySpan<char> charSpan) {
			AssertCharacterCount(charSpan.Length);
			this = new FourCharacterCode(a: charSpan[0], b: charSpan[1], c: charSpan[2], d: charSpan[3]);
		}

		[MethodImpl(AggressiveInlining)]
		public FourCharacterCode(in string characters) {
			AssertCharacterCount(characters.Length);
			this = new FourCharacterCode(charSpan: characters.AsSpan());
		}

		// MARK: - IEquatable

		[MethodImpl(AggressiveInlining)]
		public bool Equals(FourCharacterCode other)
			=> other.rawValue == rawValue;

		// MARK: - Operators

		[MethodImpl(AggressiveInlining)]
		public static bool operator ==(FourCharacterCode lhs, FourCharacterCode rhs)
			=> lhs.Equals(rhs);

		[MethodImpl(AggressiveInlining)]
		public static bool operator !=(FourCharacterCode lhs, FourCharacterCode rhs)
			=> !lhs.Equals(rhs);

		[MethodImpl(AggressiveInlining)]
		public static explicit operator FourCharacterCode(uint rawValue)
			=> new FourCharacterCode(rawValue: rawValue);

		[MethodImpl(AggressiveInlining)]
		public static explicit operator uint(FourCharacterCode fourCharacterCode)
			=> fourCharacterCode.rawValue;

		// MARK: - Overrides

		[MethodImpl(AggressiveInlining)]
		public readonly override bool Equals(object other) => other switch {
			FourCharacterCode otherFourCharacterCode => this.Equals(otherFourCharacterCode),
			_ => false
		};

		[MethodImpl(AggressiveInlining)]
		public readonly override int GetHashCode()
			=> (typeof(FourCharacterCode), rawValue).GetHashCode();

		[MethodImpl(AggressiveInlining)]
		public readonly override string ToString() {
			(char a, char b, char c, char d) = GetCharacters();
			return string.Format(FORMAT_TO_STRING, a, b, c, d);
		}

		// MARK: - Constants

		private const int CHARACTER_COUNT = 4;

		private const string FORMAT_TO_STRING = "{0}{1}{2}{3}";

		// MARK: - Utility

		[MethodImpl(AggressiveInlining)]
		private static void AssertCharacterCount(int givenCount) {
			if (givenCount != CHARACTER_COUNT) {
				throw new ArgumentException();
			}
		}

		private readonly (byte a, byte b, byte c, byte d) GetBytes() {
			throw new NotImplementedException();
		}

		[MethodImpl(AggressiveInlining)]
		private readonly (char a, char b, char c, char d) GetCharacters() {
			(byte a, byte b, byte c, byte d) = GetBytes();
			return ((char)a, (char)b, (char)c, (char)d);
		}
	}
}