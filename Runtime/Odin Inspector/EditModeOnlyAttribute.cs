#if ODIN_INSPECTOR
using System;
using Sirenix.OdinInspector;

namespace Sirenix.OdinInspector {
	// TODO: remove
	[Obsolete("Use `[DisableInPlayMode]` instead.")]
	[IncludeMyAttributes]
	[DisableIf("@(UnityEngine.Application.isPlaying)")]
	public sealed class EditModeOnlyAttribute : Attribute { }
}
#endif