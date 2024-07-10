// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using Type = System.Type;

namespace Foundation.Editors {
	public static class SubObjectUtility {
		public static T CreateSubObject<T>(Object parent, string name) where T : ScriptableObject {
			if (parent == null) {
				return null;
			}

			T subobject = ScriptableObject.CreateInstance<T>();
			subobject.name = name ?? typeof(T).ToString();

			AssetDatabase.AddObjectToAsset(subobject, parent);
			AssetDatabase.SaveAssetIfDirty(parent);

			return subobject;
		}

		public static Object CreateSubObject(Object parent, Type childType, string name) {
			if (parent == null) {
				return null;
			}

			Object subobject = ScriptableObject.CreateInstance(childType);
			subobject.name = name ?? childType.ToString();

			AssetDatabase.AddObjectToAsset(subobject, parent);
			AssetDatabase.SaveAssetIfDirty(parent);

			return subobject;
		}

		public static void DestroySubObject(Object subobject) {
			if (subobject == null) {
				return;
			}

			Object parent = RetrieveParentObject(subobject);

			if (parent == null) {
				return;
			}

			AssetDatabase.RemoveObjectFromAsset(subobject);
			Object.DestroyImmediate(subobject);
			AssetDatabase.SaveAssetIfDirty(parent);
		}

		public static Object RetrieveParentObject(Object subobject)
			=> AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GetAssetPath(subobject));

		public static T RetrieveParentObject<T>(Object subobject) where T : Object
			=> AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GetAssetPath(subobject)) as T;

		public static IEnumerable<T> RetrieveSubObjects<T>(Object parent) where T : Object
			=> AssetDatabase.LoadAllAssetRepresentationsAtPath(AssetDatabase.GetAssetPath(parent)).CompactMap(o => o as T);

		public static void DestroyAllSubObjects(Object parent) {
			IEnumerable<Object> subobjects = RetrieveSubObjects<Object>(parent);
			foreach (Object subobject in subobjects) {
				DestroySubObject(subobject);
			}
		}
	}
}