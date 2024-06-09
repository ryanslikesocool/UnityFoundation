using System;
using Unity.Mathematics;
using UnityEngine;
using System.Runtime.CompilerServices;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace Foundation {
	[Serializable]
	public struct Angle : IEquatable<Angle>, IComparable<Angle> {
		[SerializeField, HideInInspector] private float _storage;

		/// <summary>
		/// The angle, expressed in radians.
		/// </summary>
		/// <remarks>
		/// tau (~6.28) radians is equivalent to one full rotation.
		/// </remarks>
		public float radians {
			readonly get => _storage;
			set => _storage = value;
		}

		/// <summary>
		/// The angle, expressed in degrees.
		/// </summary>
		/// <remarks>
		/// 360 degrees is equivalent to one full rotation.
		/// </remarks>
		public float degrees {
			readonly get => math.degrees(_storage);
			set => _storage = math.radians(value);
		}

		/// <summary>
		/// The angle, expressed in turns.
		/// </summary>
		/// <remarks>
		/// 1 turn is equivalent to one full rotation.
		/// </remarks>
		public float turns {
			readonly get => _storage * TAU_RCP;
			set => _storage = value * math.TAU;
		}

		// MARK: - Lifecycle

		public Angle(Mode mode, float value) {
			_storage = 0;
			switch (mode) {
				case Mode.Radians:
					this.radians = value;
					break;
				case Mode.Degrees:
					this.degrees = value;
					break;
				case Mode.Turns:
					this.turns = value;
					break;
			}
		}

		[MethodImpl(AggressiveInlining)]
		public static Angle Radians(float value) => new Angle {
			radians = value
		};

		[MethodImpl(AggressiveInlining)]
		public static Angle Degrees(float value) => new Angle {
			degrees = value
		};

		[MethodImpl(AggressiveInlining)]
		public static Angle Turns(float value) => new Angle {
			turns = value
		};

		// MARK: - Supporting Data

		public enum Mode : byte {
			Radians = 0,
			Degrees = 1,
			Turns = 2
		}

		// MARK: - Operators

		[MethodImpl(AggressiveInlining)]
		public static implicit operator float(Angle angle)
			=> angle.radians;

		[MethodImpl(AggressiveInlining)]
		public static implicit operator Angle(float value)
			=> Angle.Radians(value);

		[MethodImpl(AggressiveInlining)]
		public static bool operator ==(Angle lhs, Angle rhs)
			=> lhs.Equals(rhs);

		[MethodImpl(AggressiveInlining)]
		public static bool operator !=(Angle lhs, Angle rhs)
			=> !lhs.Equals(rhs);

		[MethodImpl(AggressiveInlining)]
		public static bool operator <(Angle lhs, Angle rhs)
			=> lhs._storage < rhs._storage;

		[MethodImpl(AggressiveInlining)]
		public static bool operator >(Angle lhs, Angle rhs)
			=> lhs._storage > rhs._storage;

		[MethodImpl(AggressiveInlining)]
		public static bool operator <=(Angle lhs, Angle rhs)
			=> lhs._storage <= rhs._storage;

		[MethodImpl(AggressiveInlining)]
		public static bool operator >=(Angle lhs, Angle rhs)
			=> lhs._storage >= rhs._storage;

		// MARK: - IEquatable<Angle>

		[MethodImpl(AggressiveInlining)]
		public readonly bool Equals(Angle other)
			=> this._storage == other._storage;

		// MARK: - IComparable<Angle>

		[MethodImpl(AggressiveInlining)]
		public readonly int CompareTo(Angle other)
			=> this._storage.CompareTo(other._storage);

		// MARK: - Override

		[MethodImpl(AggressiveInlining)]
		public readonly override string ToString()
			=> string.Format(DEBUG_FORMAT, radians, degrees, turns);

		[MethodImpl(AggressiveInlining)]
		public readonly override bool Equals(object other) => other switch {
			Angle _angle => this.Equals(_angle),
			float _float => this._storage == _float,
			_ => false
		};

		[MethodImpl(AggressiveInlining)]
		public readonly override int GetHashCode()
			=> _storage.GetHashCode();

		// MARK: - Constants

		private const float TAU_RCP = 1.0f / math.TAU;

		private const string DEBUG_FORMAT = "Angle(radians: {0}, degrees: {1}, turns: {2})";

		public readonly Angle Zero
			=> new Angle();
	}
}