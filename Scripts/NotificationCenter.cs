using System;
using System.Collections.Generic;
using UnityEngine;

namespace Foundation {
    public partial class NotificationCenter {
        private static NotificationCenter @default = null;

        /// <summary>
        /// The app’s default notification center.
        /// </summary>
        public static NotificationCenter Default {
            get {
                if (@default == null) {
                    @default = new NotificationCenter();
                }
                return @default;
            }
        }

        public delegate void Callback(Notification notification);

        private Dictionary<int, NotificationEvent> events = new Dictionary<int, NotificationEvent>();
        private Dictionary<int, List<Callback>> observers = new Dictionary<int, List<Callback>>();
    }

    // MARK: - Internal

    public partial class NotificationCenter {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Init() {
            @default?.Clear();
            @default = null;
        }

        private class NotificationEvent {
            public event Callback eventDelegate;

            public void Invoke(in Notification notification) {
                eventDelegate?.Invoke(notification);
            }
        }

        private int ValidateNotification(in Notification.Name name) {
            int nameHash = name.GetHashCode();
            if (!events.ContainsKey(nameHash)) {
                events.Add(nameHash, new NotificationEvent());
                observers.Add(nameHash, new List<Callback>());
            }
            return nameHash;
        }

        private void Clear() {
            events.Clear();
            foreach (int n in observers.Keys) {
                observers[n].Clear();
            }
            observers.Clear();
        }
    }

    // MARK: - Post

    public partial class NotificationCenter {
        public void Post(in Notification notification) {
            int hash = ValidateNotification(notification.name);
            events[hash].Invoke(notification);
        }

        public void Post(in Notification.Name name, in object sender) {
            int hash = ValidateNotification(name);
            Post(new Notification(name, sender, null));
        }

        public void Post(in Notification.Name name, in object sender, in object data) {
            int hash = ValidateNotification(name);
            Post(new Notification(name, sender, data));
        }
    }

    // MARK: - Add Observer

    public partial class NotificationCenter {
        public void AddObserver(in Notification.Name name, in Callback block) {
            int hash = ValidateNotification(name);
            observers[hash].Add(block);
            events[hash].eventDelegate += block;
        }
    }

    // MARK: - Remove Observer

    public partial class NotificationCenter {
        public void RemoveObserver(in Notification.Name name, in Callback observer) {
            int hash = ValidateNotification(name);
            observers[hash].Remove(observer);
            events[hash].eventDelegate -= observer;
        }
    }
}