#if ODIN_INSPECTOR
using System;

namespace Foundation {
    /// <summary>
    /// An attribute to mark scriptable objects so they can be imported into the configuration manager editor window.
    /// </summary>
    public sealed class ConfigurationDataAttribute: Attribute { }
}
#endif