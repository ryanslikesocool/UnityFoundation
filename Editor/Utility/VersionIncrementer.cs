// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

#if UNITY_EDITOR
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
#if UNITY_IOS
using UnityEditor.iOS.Xcode;
#endif

namespace SHC.Editors {
	public sealed class VersionIncrementor : IPreprocessBuildWithReport {
		public int callbackOrder => 0;

		[MenuItem("Tools/Version/Increment Build", priority = 1)]
		private static void IncreasePlatformVersion() {
			PlayerSettings.Android.bundleVersionCode += 1;
			PlayerSettings.iOS.buildNumber = (int.Parse(PlayerSettings.iOS.buildNumber) + 1).ToString();

#if UNITY_IOS
            UpdateSettingsBundle();
#endif
		}

		[MenuItem("Tools/Version/Increment Patch", priority = 12)]
		private static void IncreasePatch() {
			IncrementVersion(new[] { 0, 0, 1 });
		}

		[MenuItem("Tools/Version/Increment Minor", priority = 13)]
		private static void IncreaseMinor() {
			IncrementVersion(new[] { 0, 1, 0 });
		}

		[MenuItem("Tools/Version/Increment Major", priority = 14)]
		private static void IncreaseMajor() {
			IncrementVersion(new[] { 1, 0, 0 });
		}

		private static void IncrementVersion(int[] version) {
			string[] lines = PlayerSettings.bundleVersion.Split('.');

			for (int i = lines.Length - 1; i >= 0; i--) {
				bool isNumber = int.TryParse(lines[i], out int numberValue);

				if (isNumber && version.Length - 1 >= i) {
					if (i > 0 && version[i] + numberValue > 9) {
						version[i - 1]++;

						version[i] = 0;
					} else {
						version[i] += numberValue;
					}
				}
			}

			PlayerSettings.bundleVersion = $"{version[0]}.{version[1]}.{version[2]}";
		}

#if UNITY_IOS
		private static void UpdateSettingsBundle() {
			string plistPath = Path.Combine(Application.dataPath, "Plugins/iOS/Settings.bundle/About.plist");
			PlistDocument plist = new PlistDocument();
			plist.ReadFromFile(plistPath);

			string newValue = $"{PlayerSettings.bundleVersion} ({PlayerSettings.iOS.buildNumber})";

			PlistElementDict rootDict = plist.root;
			PlistElementArray preferencesArray = rootDict["PreferenceSpecifiers"].AsArray();
			PlistElementDict versionDict = preferencesArray.values[1].AsDict();
			versionDict.SetString("Key", newValue);
			versionDict.SetString("DefaultValue", newValue);

			File.WriteAllText(plistPath, plist.WriteToString());
		}
#endif

		public void OnPreprocessBuild(BuildReport report) {
			IncreasePlatformVersion();
		}
	}
}
#endif