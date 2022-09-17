using System;
using System.Collections.Generic;
using UnityEngine;

namespace Foundation {
    public class NotificationCenter : MonoBehaviour {
        private static NotificationCenter shared = null;
        private static NotificationCenter Shared {
            get {
                if (shared == null) {
                    shared = Foundation.Shared.gameObject.AddComponent<NotificationCenter>();
                }
                return shared;
            }
        }

        public delegate void Callback(Notification notification);

        private static Dictionary<int, NotificationEvent> events = new Dictionary<int, NotificationEvent>();
        private static Dictionary<int, List<Callback>> observers = new Dictionary<int, List<Callback>>();

        private class NotificationEvent {
            public event Callback eventDelegate;

            public void Invoke(Notification notification) {
                eventDelegate?.Invoke(notification);
            }
        }

        private void OnApplicationQuit() {
            shared = null;
            foreach (int n in observers.Keys) {
                observers[n].Clear();
            }
        }

        #region Static

        public static void Post(Notification.Name notification, object data) {
            int hash = notification.GetHashCode();
            ValidateNotification(hash);
            Notification obj = new Notification(notification, data);
            events[hash].Invoke(obj);
        }

        public static void AddObserver(Notification.Name notification, Callback action) {
            int hash = notification.GetHashCode();
            ValidateNotification(hash);

            observers[hash].Add(action);
            events[hash].eventDelegate += action;
        }

        public static void RemoveObserver(Notification.Name notification, Callback action) {
            int hash = notification.GetHashCode();
            ValidateNotification(hash);

            observers[hash].Remove(action);
            events[hash].eventDelegate -= action;
        }

        public static void RemoveAllObservers(Notification.Name notification) {
            int hash = notification.GetHashCode();
            ValidateNotification(hash);

            foreach (Callback observer in observers[hash]) {
                events[hash].eventDelegate -= observer;
            }
            observers[hash].Clear();
        }

        #endregion

        #region Internal

        private static void ValidateNotification(int notification) {
            if (!events.ContainsKey(notification)) {
                events.Add(notification, new NotificationEvent());
                observers.Add(notification, new List<Callback>());
            }
        }

        #endregion
    }

    public class Notification : EventArgs {
        public readonly Name name;
        public readonly object data;

        public Notification(Name name, object data) {
            this.name = name;
            this.data = data;
        }

        public class Name {
            public readonly string value;

            public Name(string value) {
                this.value = value;
            }
        }
    }
}