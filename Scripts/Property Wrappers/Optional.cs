using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Foundation {
    [Serializable]
    public struct Optional<Value> : IPropertyWrapper<Value> where Value : struct {
        [SerializeField] private Value _value;
        [SerializeField] private bool _hasValue;

        public Value wrappedValue {
            get {
                if (!HasValue) {
                    throw new System.InvalidOperationException("Serializable nullable object must have a value.");
                }
                return _value;
            }
            set {
                _hasValue = true;
                _value = value;
            }
        }
        public bool HasValue => _hasValue;

        public Optional(bool hasValue, Value v) {
            this._value = v;
            this._hasValue = hasValue;
        }

        private Optional(Value v) {
            this._value = v;
            this._hasValue = true;
        }

        public bool TryGetValue(out Value value) {
            value = this._value;
            return HasValue;
        }

        public Value? System {
            get {
                if (!HasValue) {
                    return null;
                } else {
                    return _value;
                }
            }
        }

        public static implicit operator Optional<Value>(Value value)
            => new Optional<Value>(value);

        public static implicit operator Optional<Value>(System.Nullable<Value> value)
            => value.HasValue ? new Optional<Value>(value.Value) : new Optional<Value>();

        public static implicit operator System.Nullable<Value>(Optional<Value> value)
            => value.HasValue ? (Value?)value.wrappedValue : null;
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(Optional<>))]
    internal class OptionalDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginProperty(position, label, property);

            // Draw label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            // Don't make c$$anonymous$$ld fields be indented
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            // Calculate rects
            var setRect = new Rect(position.x, position.y, 15, position.height);
            var consumed = setRect.width + 5;
            var valueRect = new Rect(position.x + consumed, position.y, position.width - consumed, position.height);

            // Draw fields - pass GUIContent.none to each so they are drawn without labels
            var hasValueProp = property.FindPropertyRelative("_hasValue");
            EditorGUI.PropertyField(setRect, hasValueProp, GUIContent.none);
            bool guiEnabled = GUI.enabled;
            GUI.enabled = guiEnabled && hasValueProp.boolValue;
            EditorGUI.PropertyField(valueRect, property.FindPropertyRelative("_value"), GUIContent.none);
            GUI.enabled = guiEnabled;

            // Set indent back to what it was
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
#endif

}