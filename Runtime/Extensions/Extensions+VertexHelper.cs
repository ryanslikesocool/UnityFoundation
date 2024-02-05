using UnityEngine.UI;
using UnityEngine;

namespace Foundation {
	public static partial class Extensions {
		public static void AddVert(this VertexHelper vertexHelper, float x, float y, in Color32 color, in Vector4 uv0)
			=> vertexHelper.AddVert(new Vector3(x, y, 0), color, uv0);

		public static void AddVert(this VertexHelper vertexHelper, in Vector2 position, in Color32 color, in Vector4 uv0)
			=> vertexHelper.AddVert(new Vector3(position.x, position.y, 0), color, uv0);

		public static void AddTriangle(this VertexHelper vertexHelper, int a, int b, int c)
			=> vertexHelper.AddTriangle(a, b, c);

		public static void AddQuad(this VertexHelper vertexHelper, int a, int b, int c, int d) {
			vertexHelper.AddTriangle(a, b, c);
			vertexHelper.AddTriangle(a, c, d);
		}
	}
}