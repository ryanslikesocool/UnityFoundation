#if ODIN_INSPECTOR
using System;
using Sirenix.OdinInspector;

namespace Sirenix.OdinInspector {
    [IncludeMyAttributes]
    [ShowIf("@(UnityEngine.Application.isPlaying)")]
    public sealed class ShowInPlayModeAttribute : Attribute { }
}
#endif