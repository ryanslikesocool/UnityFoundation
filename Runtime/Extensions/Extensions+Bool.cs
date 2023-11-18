using UnityEngine;

namespace Foundation {
    public static partial class Extensions {
        public static bool Toggle(this ref bool value) {
            value = !value;
            return value;
        }
    }
}