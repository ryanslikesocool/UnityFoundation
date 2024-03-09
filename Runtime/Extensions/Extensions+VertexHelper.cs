using UnityEngine.UI;
using UnityEngine;
using System.Runtime.CompilerServices;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace Foundation {
	public static partial class Extensions {
		[MethodImpl(AggressiveInlining)]
		public static void AddVert(this VertexHelper vertexHelper, float x, float y, in Color32 color, in Vector4 uv0)
			=> vertexHelper.AddVert(new Vector3(x, y, 0), color, uv0);

		[MethodImpl(AggressiveInlining)]
		public static void AddVert(this VertexHelper vertexHelper, in Vector2 position, in Color32 color, in Vector4 uv0)
			=> vertexHelper.AddVert(new Vector3(position.x, position.y, 0), color, uv0);

		[MethodImpl(AggressiveInlining)]
		public static void AddQuad(this VertexHelper vertexHelper, int idx0, int idx1, int idx2, int idx3) {
			vertexHelper.AddTriangle(idx0, idx1, idx2);
			vertexHelper.AddTriangle(idx0, idx2, idx3);
		}
	}
}