#if ODIN_INSPECTOR
using System;
using Sirenix.OdinInspector;

namespace Sirenix.OdinInspector {
    [IncludeMyAttributes]
    [DisableIf("@(UnityEngine.Application.isPlaying)")]
    public sealed class EditModeOnlyAttribute : Attribute { }
}
#endif