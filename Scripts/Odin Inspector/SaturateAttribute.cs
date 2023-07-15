#if ODIN_INSPECTOR
using System;
using Sirenix.OdinInspector;

namespace Sirenix.OdinInspector {
	[IncludeMyAttributes]
	[PropertyRange(0f, 1f)]
	public sealed class SaturateAttribute : Attribute { }
}
#endif