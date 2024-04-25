using ObjectPooling;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class ObjectPoolEditorWindow : OdinMenuEditorWindow
    {
        [MenuItem("Tools/ObjectPool Editor")]

        public static void OpenWindow()
        {
            ObjectPoolEditorWindow window = GetWindow<ObjectPoolEditorWindow>(string.Format("ObjectPool Editor"));
            window.minSize = new Vector2(800f, 800f);
            window.maxSize = new Vector2(800f, 800f);

            window.Show();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            OdinMenuTreeDrawingConfig style = new OdinMenuTreeDrawingConfig();
            OdinMenuTree tree = new OdinMenuTree(false, style);

            List<PoolCategory> allObjectPoolCategories = new List<PoolCategory>(ObjectPoolDatabase.Instance.PoolCategories);
            allObjectPoolCategories.Sort((x, y) => x.CategoryName.CompareTo(y.CategoryName));

            foreach (var objectPoolCategory in allObjectPoolCategories)
            {
                tree.AddObjectAtPath(objectPoolCategory.CategoryName, objectPoolCategory);
            }

            return tree;
        }


        protected override void OnImGUI()
        {
            base.OnImGUI();

            EditorGUILayout.LabelField("ToolBox", SirenixGUIStyles.BoldLabelCentered);
            SirenixEditorGUI.HorizontalLineSeparator(Color.white, 2);
            SirenixEditorGUI.BeginHorizontalToolbar();

            if (GUILayout.Button("AddNewCategory", SirenixGUIStyles.Button))
            {
                AddNewCategory();
            }

            SirenixEditorGUI.EndHorizontalToolbar();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            Debug.Log("ObjectPool Saving");
            SaveThisAsset();
        }

        private void SaveThisAsset()
        {
            if (EditorApplication.isUpdating)
            {
                return;
            }

            EditorUtility.SetDirty(ObjectPoolDatabase.Instance);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private void AddNewCategory()
        {

        }
    }
}
