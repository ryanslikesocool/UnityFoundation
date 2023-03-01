#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
using UnityEngine;

namespace Foundation {
#if ODIN_INSPECTOR
    [InlineEditor, ConfigurationData]
#endif
    public abstract class AnyConfiguration : ScriptableObject, IConfiguration { }
}