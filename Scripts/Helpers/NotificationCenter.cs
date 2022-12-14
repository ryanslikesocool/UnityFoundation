using System;
using System.Collections.Generic;
using UnityEngine;

namespace Foundation {
    /// <summary>
    /// A notification dispatch mechanism that enables the broadcast of information to registered observers.
    /// </summary>
    public class NotificationCenter {
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

        // MARK: - Internal

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

        // MARK: - Post

        /// <summary>
        /// Posts a given notification to the notification center.
        /// </summary>
        /// <param name="notification">The notification to post.</param>
        public void Post(in Notification notification) {
            int hash = ValidateNotification(notification.name);
            events[hash].Invoke(notification);
        }

        /// <summary>
        /// Creates a notification with a given name and sender and posts it to the notification center.
        /// </summary>
        /// <param name="name">The name of the notification.</param>
        /// <param name="sender">The sender posting the notification.</param>
        public void Post(in Notification.Name name, in object sender) {
            int hash = ValidateNotification(name);
            Post(new Notification(name, sender, null));
        }

        /// <summary>
        /// Creates a notification with a given name, sender, and information and posts it to the notification center.
        /// </summary>
        /// <param name="name">The name of the notification.</param>
        /// <param name="sender">The sender posting the notification.</param>
        /// <param name="data">A optional information about the notification.</param>
        public void Post(in Notification.Name name, in object sender, in object data) {
            int hash = ValidateNotification(name);
            Post(new Notification(name, sender, data));
        }

        // MARK: - Add Observer

        /// <summary>
        /// Adds an entry to the notification center to receive notifications that passed to the provided block.
        /// </summary>
        /// <param name="name">The name of the notification to register for delivery to the observer block.</param>
        /// <param name="block">
        /// The block that executes when receiving a notification.
        ///
        /// The notification center copies the block.The notification center strongly holds the copied block until you remove the observer registration.
        ///
        /// The block takes one argument: the notification.
        /// </param>
        public void AddObserver(in Notification.Name name, in Callback block) {
            int hash = ValidateNotification(name);
            observers[hash].Add(block);
            events[hash].eventDelegate += block;
        }

        // MARK: - Remove Observer

        /// <summary>
        /// Removes matching entries from the notification center's dispatch table.
        /// </summary>
        /// <param name="name">The name of the notification to remove from the dispatch table. Specify a notification name to remove only entries with this notification name.</param>
        /// <param name="observer">The block to remove from the dispatch table. Specify a notification observer to remove only entries with this observer.</param>
        public void RemoveObserver(in Notification.Name name, in Callback observer) {
            int hash = ValidateNotification(name);
            observers[hash].Remove(observer);
            events[hash].eventDelegate -= observer;
        }

        /// <summary>
        /// Removes matching entries from the notification center's dispatch table.
        /// </summary>
        /// <param name="name">The name of the notification to remove from the dispatch table. Specify a notification name to remove only entries with this notification name.</param>
        public void RemoveAllObservers(in Notification.Name name) {
            int hash = ValidateNotification(name);
            foreach (Callback observer in observers[hash]) {
                events[hash].eventDelegate -= observer;
            }
        }
    }
}