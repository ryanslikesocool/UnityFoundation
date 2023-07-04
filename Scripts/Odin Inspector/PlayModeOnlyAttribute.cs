#if ODIN_INSPECTOR
using System;
using Sirenix.OdinInspector;

namespace Sirenix.OdinInspector {
    [IncludeMyAttributes]
    [DisableIf("@(!UnityEngine.Application.isPlaying)")]
    [ShowInInspector]
    public sealed class PlayModeOnlyAttribute : Attribute { }
}
#endif