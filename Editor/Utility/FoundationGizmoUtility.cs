// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Foundation.Editor {
	public static partial class FoundationGizmoUtility {
#if UNITY_EDITOR
		/// <summary>
		/// Get world space size of a gizmo at given position.
		/// </summary>
		/// <param name="position">The position of the gizmo in 3D space.</param>
		/// <returns>A constant screen-size for the gizmo, based on the distance between from the supplied gizmo's position to the camera.</returns>
		public static float GetGizmoSize(Vector3 position) {
			Camera current = Camera.current;
			position = Gizmos.matrix.MultiplyPoint(position);

			if ((bool)current) {
				Transform transform = current.transform;
				Vector3 position2 = transform.position;
				float z = Vector3.Dot(position - position2, transform.TransformDirection(new Vector3(0f, 0f, 1f)));
				Vector3 vector = current.WorldToScreenPoint(position2 + transform.TransformDirection(new Vector3(0f, 0f, z)));
				Vector3 vector2 = current.WorldToScreenPoint(position2 + transform.TransformDirection(new Vector3(1f, 0f, z)));
				float magnitude = (vector - vector2).magnitude;
				return 80f / Mathf.Max(magnitude, 0.0001f) * EditorGUIUtility.pixelsPerPoint;
			}

			return 20f;
		}
#endif

		// MARK: - Drawing

		public static void DrawWireRect(Rect rect) {
			Gizmos.DrawLineStrip(
				points: new Vector3[4] {
					new(rect.min.x, rect.min.y, 0),
					new(rect.min.x, rect.max.y, 0),
					new(rect.max.x, rect.max.y, 0),
					new(rect.max.x, rect.min.y, 0),
				},
				looped: true
			);
		}

		public static void DrawWireRect(Vector2 size) {
			DrawWireRect(
				rect: new(
					position: -size * 0.5f,
					size: size
				)
			);
		}

		public static void DrawWirePolygon(Vector3 position, Vector3 normal, float circumcircleRadius, int pointCount) {
			Debug.Assert(pointCount >= 3);

			float angleStep = (Mathf.PI * 2.0f) / pointCount;

			Vector3[] points = new Vector3[pointCount];
			for (int i = 0; i < pointCount; i++) {
				float radians = angleStep * i;
				points[i] = new(
					Mathf.Cos(radians),
					Mathf.Sin(radians),
					0
				);
			}

			Matrix4x4 localMatrix = Matrix4x4.TRS(
				position,
				Quaternion.LookRotation(normal),
				Vector3.one * circumcircleRadius
			);

			using (new DrawingScope(matrix: localMatrix * Gizmos.matrix)) {
				Gizmos.DrawLineStrip(
					points: points,
					looped: true
				);
			}
		}
	}
}