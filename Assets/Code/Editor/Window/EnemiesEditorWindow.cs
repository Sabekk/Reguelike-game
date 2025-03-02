using Gameplay.Character.Data;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class EnemiesEditorWindow : OdinMenuEditorWindow
    {
        [MenuItem("Tools/Enemies Editor")]

        #region VARIABLES

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        public static void OpenWindow()
        {
            EnemiesEditorWindow window = GetWindow<EnemiesEditorWindow>(string.Format("Enemies data editor"));
            window.minSize = new Vector2(800f, 800f);
            window.maxSize = new Vector2(800f, 800f);

            window.Show();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            OdinMenuTreeDrawingConfig style = new OdinMenuTreeDrawingConfig();
            OdinMenuTree tree = new OdinMenuTree(false, style);

            if (MainDatabases.Instance.EnemiesDatabase.EnemyBiomData != null)
            {
                List<EnemyBiomCategoryData> allEnemyBiomCategories = new List<EnemyBiomCategoryData>(MainDatabases.Instance.EnemiesDatabase.EnemyBiomData);

                for (int i = 0; i < allEnemyBiomCategories.Count; i++)
                {
                    tree.AddObjectAtPath(i + " - " + allEnemyBiomCategories[i].BiomType.ToString(), allEnemyBiomCategories[i]);
                }
            }

            return tree;
        }

        protected override void OnImGUI()
        {
            base.OnImGUI();

            EditorGUILayout.LabelField("ToolBox", SirenixGUIStyles.BoldLabelCentered);
            SirenixEditorGUI.HorizontalLineSeparator(Color.white, 2);
            SirenixEditorGUI.BeginHorizontalToolbar();

            if (GUILayout.Button("AddNewBiomCategory", SirenixGUIStyles.Button))
            {
                AddNewBiomEnemies();
            }

            if (GUILayout.Button("DeleteCategory", SirenixGUIStyles.Button))
            {
                DeleteBiomEnemies();
            }


            SirenixEditorGUI.EndHorizontalToolbar();

            SirenixEditorGUI.BeginHorizontalToolbar();

            if (GUILayout.Button("Refresh", SirenixGUIStyles.Button))
            {
                ForceMenuTreeRebuild();
            }

            SirenixEditorGUI.EndHorizontalToolbar();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            Debug.Log("Enemies database saving");
            SaveThisAsset();
        }

        private void SaveThisAsset()
        {
            if (EditorApplication.isUpdating)
            {
                return;
            }

            EditorUtility.SetDirty(MainDatabases.Instance.EnemiesDatabase);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private void AddNewBiomEnemies()
        {
            MainDatabases.Instance.EnemiesDatabase.AddBiomCategory(new EnemyBiomCategoryData());

            ForceMenuTreeRebuild();
        }

        private void DeleteBiomEnemies()
        {
            EnemyBiomCategoryData categoryToDelete = MenuTree.Selection.SelectedValue as EnemyBiomCategoryData;

            if (categoryToDelete != null)
            {
                if (EditorUtility.DisplayDialog("Are you sure?", StringBuilderScaler.GetScaledText("{0} - {1}", "Delete this enemies biom?", categoryToDelete.BiomType.ToString()), "Yes", "Cancel") == false)
                    return;

                MainDatabases.Instance.EnemiesDatabase.DeleteBiomCategory(categoryToDelete);
                ForceMenuTreeRebuild();
            }
        }

        #endregion

    }
}