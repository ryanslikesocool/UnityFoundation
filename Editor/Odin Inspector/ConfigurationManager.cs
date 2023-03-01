#if ODIN_INSPECTOR
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using System;
using System.Linq;

namespace Foundation.Editors {
    internal sealed class ConfigurationManager: OdinMenuEditorWindow {
        [MenuItem("Tools/Configuration Manager")]
        private static void OpenEditor() => GetWindow<ConfigurationManager>();

        protected override OdinMenuTree BuildMenuTree() {
            OdinMenuTree tree = new OdinMenuTree();
            tree.AddAllAssetsAtPath("Configuration", "Assets/_Scriptable Objects/Configuration", typeof(AnyConfiguration), true, false);
            tree.SortMenuItemsByName();
            return tree;
        }
    }
}
#endif