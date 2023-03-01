#if ODIN_INSPECTOR
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using System;
using System.Linq;

namespace Foundation.Editors {
    internal sealed class ConfigurationManager: OdinMenuEditorWindow {
        private static Type[] typesToDisplay = TypeCache.GetTypesWithAttribute<ConfigurationDataAttribute>()
            .OrderBy(c => c.Name)
            .ToArray();

        private Type selectedType;

        [MenuItem("Tools/Configuration Manager")]
        private static void OpenEditor() => GetWindow<ConfigurationManager>();

        protected override OdinMenuTree BuildMenuTree() {
            OdinMenuTree tree = new OdinMenuTree();
            foreach (Type type in typesToDisplay) {
                tree.AddAllAssetsAtPath(type.Name, "Assets/", type, true, true);
            }
            return tree;
        }
    }
}
#endif