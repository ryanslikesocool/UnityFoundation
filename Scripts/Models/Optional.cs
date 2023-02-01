using System;
using UnityEngine;
#if UNITY_EDITOR
   using UnityEditor;
#endif

namespace Foundation {
    [Serializable]
    public struct Optional<T> where T : struct {
        [SerializeField] private T _value;
        [SerializeField] private bool _hasValue;

        public T Value {
            get {
                if (!HasValue) {
                    throw new System.InvalidOperationException("Serializable nullable object must have a value.");
                }
                return _value;
            }
        }
        public bool HasValue => _hasValue;

        public Optional(bool hasValue, T v) {
            this._value = v;
            this._hasValue = hasValue;
        }

        private Optional(T v) {
            this._value = v;
            this._hasValue = true;
        }

        public bool TryGetValue(out T value) {
            value = this._value;
            return HasValue;
        }

        public T? System {
            get {
                if (!HasValue) {
                    return null;
                } else {
                    return _value;
                }
            }
        }

        public static implicit operator Optional<T>(T value)
            => new Optional<T>(value);

        public static implicit operator Optional<T>(System.Nullable<T> value)
            => value.HasValue ? new Optional<T>(value.Value) : new Optional<T>();

        public static implicit operator System.Nullable<T>(Optional<T> value)
            => value.HasValue ? (T?)value.Value : null;

        //public static explicit operator System.Nullable<T>(Optional<T> value)
        //    => value.HasValue ? (T?)value.Value : null;
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