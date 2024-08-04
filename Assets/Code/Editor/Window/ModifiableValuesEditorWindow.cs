using ModifiableValues;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class ModifiableValuesEditorWindow : OdinMenuEditorWindow
    {
        [MenuItem("Tools/Modifiable values")]

        public static void OpenWindow()
        {
            ModifiableValuesEditorWindow window = GetWindow<ModifiableValuesEditorWindow>(string.Format("Modifiable values Editor"));
            window.minSize = new Vector2(800f, 800f);
            window.maxSize = new Vector2(800f, 800f);

            window.Show();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            OdinMenuTreeDrawingConfig style = new OdinMenuTreeDrawingConfig();
            OdinMenuTree tree = new OdinMenuTree(false, style);

            List<ModifiableValuesCategory> allCategories = new List<ModifiableValuesCategory>(MainDatabases.Instance.ModifiableValuesDatabase.ModifiableValueCategories);
            allCategories.Sort((x, y) => x.CategoryName.CompareTo(y.CategoryName));

            for (int i = 0; i < allCategories.Count; i++)
            {
                tree.AddObjectAtPath(i + " - " + allCategories[i].CategoryName, allCategories[i]);
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

            if (GUILayout.Button("DeleteCategory", SirenixGUIStyles.Button))
            {
                DeleteCategory();
            }


            SirenixEditorGUI.EndHorizontalToolbar();

            SirenixEditorGUI.BeginHorizontalToolbar();

            if (GUILayout.Button("Refresh", SirenixGUIStyles.Button))
            {
                ForceMenuTreeRebuild();
            }

            SirenixEditorGUI.EndHorizontalToolbar();

            SirenixEditorGUI.BeginHorizontalToolbar();

            if (GUILayout.Button("Generate scripts", SirenixGUIStyles.Button))
            {
                MainDatabases.Instance.ModifiableValuesDatabase.GenerateScripts();
            }

            SirenixEditorGUI.EndHorizontalToolbar();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            Debug.Log("ModifiableValues Saving");
            SaveThisAsset();
        }

        private void SaveThisAsset()
        {
            if (EditorApplication.isUpdating)
            {
                return;
            }

            EditorUtility.SetDirty(MainDatabases.Instance.ModifiableValuesDatabase);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private void AddNewCategory()
        {
            MainDatabases.Instance.ModifiableValuesDatabase.AddCategory(new ModifiableValuesCategory());
            ForceMenuTreeRebuild();
        }

        private void DeleteCategory()
        {
            ModifiableValuesCategory categoryToDelete = MenuTree.Selection.SelectedValue as ModifiableValuesCategory;

            if (categoryToDelete != null)
            {
                if (EditorUtility.DisplayDialog("Are you sure?", StringBuilderScaler.GetScaledText("{0} - {1}", "Delete this category?", categoryToDelete.CategoryName), "Yes", "Cancel") == false)
                    return;

                MainDatabases.Instance.ModifiableValuesDatabase.RemoveCategory(categoryToDelete);
                ForceMenuTreeRebuild();
            }
        }
    }
}
