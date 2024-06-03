using UnityEngine.UI;
using UnityEngine;
using System.Runtime.CompilerServices;
using static System.Runtime.CompilerServices.MethodImplOptions;
using System.Collections.Generic;

namespace Foundation {
	public static partial class Extensions {
		/// <param name="vertexHelper">The destination VertexHelper.</param>
		[MethodImpl(AggressiveInlining)]
		public static void AddVert(this VertexHelper vertexHelper, float x, float y, in Color32 color, in Vector4 uv0)
			=> vertexHelper.AddVert(new Vector3(x, y, 0), color, uv0);

		/// <param name="vertexHelper">The destination VertexHelper.</param>
		[MethodImpl(AggressiveInlining)]
		public static void AddVert(this VertexHelper vertexHelper, in Vector2 position, in Color32 color, in Vector4 uv0)
			=> vertexHelper.AddVert(new Vector3(position.x, position.y, 0), color, uv0);

		/// <summary>
		/// Adds a triangle into a VertexHelper.
		/// </summary>
		/// <param name="vertexHelper">The destination VertexHelper.</param>
		/// <param name="startIndex">The start index.</param>
		/// <param name="idx0">The first index relative to the <see paramref="startIndex"/>.</param>
		/// <param name="idx1">The second index relative to the <see paramref="startIndex"/>.</param>
		/// <param name="idx2">The third index relative to the <see paramref="startIndex"/>.</param>
		[MethodImpl(AggressiveInlining)]
		public static void AddTriangle(this VertexHelper vertexHelper, int startIndex, int idx0, int idx1, int idx2)
			=> vertexHelper.AddTriangle(startIndex + idx0, startIndex + idx1, startIndex + idx2);

		/// <summary>
		/// Adds a triangle with sequential indices into a VertexHelper.
		/// </summary>
		/// <param name="vertexHelper">The destination VertexHelper.</param>
		/// <param name="index">A reference to the current index.</param>
		[MethodImpl(AggressiveInlining)]
		public static void AddTriangle(this VertexHelper vertexHelper, ref int index)
			=> vertexHelper.AddTriangle(index++, index++, index++);

		/// <summary>
		/// Adds a quad into a VertexHelper.
		/// </summary>
		/// <remarks>
		/// Internally, the quad is expressed as two triangles.
		/// </remarks>
		/// <param name="vertexHelper">The destination VertexHelper.</param>
		/// <param name="idx0">The first index.</param>
		/// <param name="idx1">The second index.</param>
		/// <param name="idx2">The third index.</param>
		/// <param name="idx3">The fourth index.</param>
		[MethodImpl(AggressiveInlining)]
		public static void AddQuad(this VertexHelper vertexHelper, int idx0, int idx1, int idx2, int idx3) {
			vertexHelper.AddTriangle(idx0, idx1, idx2);
			vertexHelper.AddTriangle(idx0, idx2, idx3);
		}

		/// <summary>
		/// Adds a quad into a VertexHelper.
		/// </summary>
		/// <remarks>
		/// Internally, the quad is expressed as two triangles.
		/// </remarks>
		/// <param name="vertexHelper">The destination VertexHelper.</param>
		/// <param name="startIndex">The start index.</param>
		/// <param name="idx0">The first index relative to the <see paramref="startIndex"/>.</param>
		/// <param name="idx1">The second index relative to the <see paramref="startIndex"/>.</param>
		/// <param name="idx2">The third index relative to the <see paramref="startIndex"/>.</param>
		/// <param name="idx3">The fourth index relative to the <see paramref="startIndex"/>.</param>
		[MethodImpl(AggressiveInlining)]
		public static void AddQuad(this VertexHelper vertexHelper, int startIndex, int idx0, int idx1, int idx2, int idx3)
			=> vertexHelper.AddQuad(startIndex + idx0, startIndex + idx1, startIndex + idx2, startIndex + idx3);

		/// <summary>
		/// Copies a triangle with sequential indices into a VertexHelper.
		/// </summary>
		/// <param name="vertexHelper">The destination VertexHelper.</param>
		/// <param name="indices">The source indices to copy.</param>
		/// <param name="index">A reference to the current index.</param>
		[MethodImpl(AggressiveInlining)]
		public static void AddTriangle(this VertexHelper vertexHelper, in IList<int> indices, ref int index)
			=> vertexHelper.AddTriangle(indices[index++], indices[index++], indices[index++]);

		/// <summary>
		/// Copies an existing index buffer into a VertexHelper.
		/// </summary>
		/// <param name="vertexHelper">The destination VertexHelper.</param>
		/// <param name="indices">The existing index buffer.</param>
		[MethodImpl(AggressiveInlining)]
		public static void AddTriangles(this VertexHelper vertexHelper, in IList<int> indices) {
			for (int i = 0; i < indices.Count; i += 3) {
				vertexHelper.AddTriangle(indices[i], indices[i + 1], indices[i + 2]);
			}
		}

		[MethodImpl(AggressiveInlining)]
		public static void AddTriangles(this VertexHelper vertexHelper, in IList<int> indices, int startIndex) {
			for (int i = 0; i < indices.Count; i += 3) {
				vertexHelper.AddTriangle(startIndex: startIndex, indices[i], indices[i + 1], indices[i + 2]);
			}
		}

		[MethodImpl(AggressiveInlining)]
		public static void AddVerts(this VertexHelper vertexHelper, in IEnumerable<Vector3> vertices, Color color, Vector4 texcoord = default) {
			foreach (Vector3 vertex in vertices) {
				vertexHelper.AddVert(vertex, color, texcoord);
			}
		}

		[MethodImpl(AggressiveInlining)]
		public static void AddVerts(this VertexHelper vertexHelper, in IEnumerable<Vector3> vertices, Vector3 offset, Color color, Vector4 texcoord = default)
			=> vertexHelper.AddVerts(vertices.Map(vertex => vertex + offset), color: color, texcoord: texcoord);

		[MethodImpl(AggressiveInlining)]
		public static void AddVerts(this VertexHelper vertexHelper, in IEnumerable<Vector2> vertices, Color color, Vector4 texcoord = default) {
			foreach (Vector2 vertex in vertices) {
				vertexHelper.AddVert(vertex, color, texcoord);
			}
		}

		[MethodImpl(AggressiveInlining)]
		public static void AddVerts(this VertexHelper vertexHelper, in IEnumerable<Vector2> vertices, Vector2 offset, Color color, Vector4 texcoord = default)
			=> vertexHelper.AddVerts(vertices.Map(vertex => vertex + offset), color: color, texcoord: texcoord);

		[MethodImpl(AggressiveInlining)]
		public static void AddVerts(this VertexHelper vertexHelper, in IEnumerable<Vector2> vertices, Vector3 offset, Color color, Vector4 texcoord = default)
			=> vertexHelper.AddVerts(vertices.Map(vertex => new Vector3(vertex.x, vertex.y, 0) + offset), color: color, texcoord: texcoord);
	}
}