using System;
using Unity.Mathematics;
using UnityEngine;

namespace Foundation {
    [Serializable]
    public struct Angle : IEquatable<Angle>, IComparable<Angle> {
        private const float TAU = math.PI * 2.0f;
        private const float TAU_RCP = 1.0f / TAU;

        [SerializeField, HideInInspector] private float _storage;

        public float radians {
            get => _storage;
            set => _storage = value;
        }

        public float degrees {
            get => math.degrees(_storage);
            set => _storage = math.radians(value);
        }

        public float turns {
            get => _storage * TAU_RCP;
            set => _storage = value * TAU;
        }

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

        public static Angle Radians(float value) {
            Angle result = new Angle();
            result.radians = value;
            return result;
        }

        public static Angle Degrees(float value) {
            Angle result = new Angle();
            result.degrees = value;
            return result;
        }

        public static Angle Turns(float value) {
            Angle result = new Angle();
            result.turns = value;
            return result;
        }

        public static readonly Angle zero = new Angle();

        public enum Mode : byte {
            Radians = 0,
            Degrees = 1,
            Turns = 2
        }

        // MARK: - Utility

        public override string ToString() => $"Angle(radians: {radians}, degrees: {degrees}, turns: {turns})";

        public static implicit operator float(Angle angle) => angle.radians;
        public static implicit operator Angle(float value) => Angle.Radians(value);

        public int CompareTo(Angle other) => this._storage.CompareTo(other._storage);

        public bool Equals(Angle other) => this._storage == other._storage;

        public override bool Equals(object other) => other switch {
            Angle _angle => this.Equals(_angle),
            float _float => this._storage == _float,
            _ => false
        };

        public override int GetHashCode() => _storage.GetHashCode();

        public static bool operator ==(Angle lhs, Angle rhs) => lhs.Equals(rhs);
        public static bool operator !=(Angle lhs, Angle rhs) => !lhs.Equals(rhs);
    }
}