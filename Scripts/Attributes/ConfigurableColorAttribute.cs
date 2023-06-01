using UnityEngine;

namespace Foundation {
    [System.AttributeUsage(
        System.AttributeTargets.Field,
        AllowMultiple = false
    )]
    public class ConfigurableColorAttribute : PropertyAttribute {
        public enum Options : byte {
            Eyedropper = 1 << 0,
            Alpha = 1 << 1,
            HDR = 1 << 2,
        }

        public Options options;

        public ConfigurableColorAttribute(Options options = Options.Eyedropper) {
            this.options = options;
        }
    }
}