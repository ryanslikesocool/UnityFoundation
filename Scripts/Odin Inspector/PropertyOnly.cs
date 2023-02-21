#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
using System;

namespace Sirenix.OdinInspector {
    [IncludeMyAttributes]
    [HideLabel, InlineProperty]
    public sealed class PropertyOnly: Attribute {}
}
#endif