#if ODIN_INSPECTOR
using System;
using Sirenix.OdinInspector;

namespace Sirenix.OdinInspector {
    [IncludeMyAttributes]
    [EnableIf("@UnityEngine.Application.isPlaying")]
    public sealed class EnableInPlayModeAttribute : Attribute { }
}
#endif