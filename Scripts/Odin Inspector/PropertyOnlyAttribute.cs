#if ODIN_INSPECTOR
using System;
using Sirenix.OdinInspector;

namespace Sirenix.OdinInspector {
    [IncludeMyAttributes]
    [HideLabel, InlineProperty]
    public sealed class PropertyOnlyAttribute : Attribute { }
}
#endif