using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Foundation {
    /// <summary>
    /// Allow a property to be modified within the Unity editor (i.e. the inspector), but not within C# code.
    /// </summary>
    [Serializable]
    public struct EditorMutable<Value> : IImmutablePropertyWrapper<Value> {
        [SerializeField] private Value _value;

        /// <summary>
        /// Read the underlying wrapped value.
        /// </summary>
        public Value wrappedValue => _value;

        public static implicit operator Value(EditorMutable<Value> wrapper) => wrapper.wrappedValue;
        public static implicit operator EditorMutable<Value>(Value value) => new EditorMutable<Value> { _value = value };
    }
}