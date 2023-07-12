using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Foundation.Editors {
    // https://discussions.unity.com/t/is-it-possible-to-group-folders-together-at-the-top-on-unity-for-macos-like-on-windows/228968/4
    [InitializeOnLoad]
    internal static class FoldersOnTop {
        private const string k_UnityEditorProjectBrowserAssemblyName = "UnityEditor.ProjectBrowser";
        private const string k_ProjectBrowsersFieldName = "s_ProjectBrowsers";
        private const string k_AssetTreeFieldName = "m_AssetTree";
        private const string k_ListAreaFieldName = "m_ListArea";
        private const string k_DataFieldName = "data";
        private const string k_FoldersFirstFieldName = "foldersFirst";

        private static readonly object s_BoolTrue = true;

        static FoldersOnTop() {
            EditorApplication.projectChanged += OnChanged;
            EditorApplication.playModeStateChanged += OnPlayMode;
            EditorApplication.projectWindowItemOnGUI += OnFirstTime;
        }
        private static void OnFirstTime(string guid, Rect _) {
            EditorApplication.projectWindowItemOnGUI -= OnFirstTime;
            Refresh();
        }
        private static void OnChanged() => Refresh();
        private static void OnPlayMode(PlayModeStateChange obj) => Refresh();

        /// <summary>
        /// foreach browser in UnityEditor.ProjectBrowser.s_ProjectBrowsers
        ///     browser.m_AssetTree.data.foldersFirst = true
        ///     browser.m_ListArea.foldersFirst = true
        /// </summary>
        private static void Refresh() {
            Assembly assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
            Type projectBrowser = assembly.GetType(k_UnityEditorProjectBrowserAssemblyName);
            FieldInfo field = projectBrowser.GetField(k_ProjectBrowsersFieldName, BindingFlags.Static | BindingFlags.NonPublic);
            if (field == null)
                return;
            IEnumerable list = (IEnumerable)field.GetValue(projectBrowser);
            foreach (object pb in list)
                SetFolderFirstForProjectWindow(pb);
        }
        private static void SetFolderFirstForProjectWindow(object pb) {
            IEnumerable<FieldInfo> members = pb.GetType().GetRuntimeFields();
            int maxMembersSought = 2;
            foreach (FieldInfo member in members) {
                switch (member.Name) {
                    // One column
                    case k_AssetTreeFieldName:
                        SetOneColumnFolderFirst(pb, member);
                        maxMembersSought--;
                        break;
                    // Two column
                    case k_ListAreaFieldName:
                        SetTwoColumnFolderFirst(pb, member);
                        maxMembersSought--;
                        break;
                }

                if (maxMembersSought == 0)
                    break;
            }
        }
        private static void SetTwoColumnFolderFirst(object pb, FieldInfo listAreaField) {
            if (listAreaField == null)
                return;
            object listArea = listAreaField.GetValue(pb);
            // safety check
            if (listArea == null)
                return;
            PropertyInfo folderFirst = listArea.GetType().GetProperties().Single(x => x.Name == k_FoldersFirstFieldName);
            folderFirst.SetValue(listArea, s_BoolTrue);
        }
        private static void SetOneColumnFolderFirst(object pb, FieldInfo assetTreeField) {
            if (assetTreeField == null)
                return;

            object assetTree = assetTreeField.GetValue(pb);
            // Fix: as we are looping all members, it's possible to end up in a case where one member is seen first,
            // this will be null.
            if (assetTree == null)
                return;

            PropertyInfo data = assetTree.GetType().GetRuntimeProperties().Single(x => x.Name == k_DataFieldName);
            // AssetsTreeViewDataSource
            object dataSource = data.GetValue(assetTree);

            // safety check
            if (dataSource == null)
                return;
            PropertyInfo folderFirst = dataSource.GetType().GetProperties().Single(x => x.Name == k_FoldersFirstFieldName);
            folderFirst.SetValue(dataSource, s_BoolTrue);
        }
    }
}