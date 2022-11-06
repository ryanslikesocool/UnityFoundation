using System;
using System.Collections.Generic;
using System.Text;

namespace Foundation {
    public struct Notification : Hashable, CustomStringConvertible {
        public readonly Name name;
        public readonly object sender;
        public readonly object data;

        public string description {
            get {
                StringBuilder stringBuilder = new StringBuilder($"name = {name.value}");
                if (sender != null) {
                    stringBuilder.AppendFormat(", sender = {0}", sender);
                }
                if (data != null) {
                    stringBuilder.AppendFormat(", data = {0}", data);
                }
                return stringBuilder.ToString();
            }
        }

        public T ReadData<T>() => (T)data;

        public Notification(in Name name) {
            this.name = name;
            this.sender = null;
            this.data = null;
        }

        public Notification(in Name name, in object sender) {
            this.name = name;
            this.sender = sender;
            this.data = null;
        }

        public Notification(in Name name, in object sender, in object data) {
            this.name = name;
            this.sender = sender;
            this.data = data;
        }

        public void Hash(ref Hasher hasher) {
            hasher.Combine(name);
            hasher.Combine(sender);
            hasher.Combine(data);
        }

        public struct Name {
            public readonly string value;

            public Name(in string value) {
                this.value = value;
            }

            public override bool Equals(object obj) {
                if (obj is Notification.Name other) {
                    return value == other.value;
                }
                return false;
            }

            public override int GetHashCode() => value.GetHashCode();

            public static bool operator ==(Name lhs, Name rhs) => lhs.value == rhs.value;
            public static bool operator !=(Name lhs, Name rhs) => lhs.value != rhs.value;
        }
    }
}