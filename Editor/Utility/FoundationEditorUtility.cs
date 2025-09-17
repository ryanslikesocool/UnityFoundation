// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using System.IO;
using UnityEditor;

namespace Foundation.Editor {
	public static partial class FoundationEditorUtility {
		/// <summary>
		/// Create a new directory in the project, including any intermediate folders.
		/// </summary>
		/// <param name="path">The path of the new directory, relative to the Assets folder.</param>
		public static void CreateDirectory(string path) {
			string directory = Path.GetDirectoryName(path);
			if (Directory.Exists(directory)) {
				return;
			}
			Directory.CreateDirectory(directory);
			AssetDatabase.Refresh();
		}
	}
}