using System;
using System.Collections.Generic;
using System.Text;

namespace Foundation {
    public struct Notification : Hashable, CustomStringConvertible {
        public readonly Name name;
        public readonly object sender;
        public readonly object userInfo;

        public string description {
            get {
                StringBuilder stringBuilder = new StringBuilder($"name = {name.value}");
                if (sender != null) {
                    stringBuilder.AppendFormat(", sender = {0}", sender);
                }
                if (userInfo != null) {
                    stringBuilder.AppendFormat(", userInfo = {0}", userInfo);
                }
                return stringBuilder.ToString();
            }
        }

        public Notification(Name name) {
            this.name = name;
            this.sender = null;
            this.userInfo = null;
        }

        public Notification(Name name, object sender) {
            this.name = name;
            this.sender = sender;
            this.userInfo = null;
        }

        public Notification(Name name, object sender, object userInfo) {
            this.name = name;
            this.sender = sender;
            this.userInfo = userInfo;
        }

        public void Hash(ref Hasher hasher) {
            hasher.Combine(name);
            hasher.Combine(sender);
            hasher.Combine(userInfo);
        }

        public struct Name {
            public readonly string value;

            public Name(string value) {
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