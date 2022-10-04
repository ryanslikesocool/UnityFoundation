using System.Collections.Generic;
using System.Text;

namespace Foundation {
    public struct Notification : Hashable, CustomStringConvertible {
        public readonly Name name;
        public readonly object sender;
        public readonly Dictionary<Hashable, object> userInfo;

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

        public Notification(Name name, Dictionary<Hashable, object> userInfo) {
            this.name = name;
            this.sender = null;
            this.userInfo = userInfo;
        }

        public Notification(Name name, object sender, Dictionary<Hashable, object> userInfo) {
            this.name = name;
            this.sender = sender;
            this.userInfo = userInfo;
        }

        public void Hash(ref Hasher hasher) {
            hasher.Combine(name);
            hasher.Combine(sender);
            hasher.Combine(userInfo);
        }

        public struct Name : Hashable {
            public readonly string value;

            public Name(string value) {
                this.value = value;
            }

            public void Hash(ref Hasher hasher) {
                hasher.Combine(value);
            }
        }
    }
}