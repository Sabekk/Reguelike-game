using ObjectPooling;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class ObjectPool_RemovingTool : EditorWindow
    {
        #region VARIABLES

        [SerializeField] private string poolName;
        [SerializeField] private int startIndex;
        [SerializeField] private int endIndex;

        private const string NAME_FORM = "{0}_{1}";

        #endregion

        #region PROPERTIES

        public PoolCategoryData SelectedCategory
        {
            get
            {
                ObjectPoolEditorWindow window = GetWindow<ObjectPoolEditorWindow>();
                if (window == null)
                    return null;
                return window.CurrentCategorySelected;
            }
        }

        #endregion

        #region UNITY_METHODS

        private void OnGUI()
        {
            poolName = EditorGUILayout.TextField("Pool name", poolName);
            startIndex = EditorGUILayout.IntField("Starting number", startIndex);
            endIndex = EditorGUILayout.IntField("End number", endIndex);

            if (GUILayout.Button("Delete in range"))
                TryDeleteInstancesInRange();

            if (GUILayout.Button("Delete all of this category"))
                TryDeleteInstancesOfName();

            if (GUILayout.Button("Delete selected"))
                TryDeleteSelectedPrefabInstances();
        }

        #endregion

        #region METHODS

        private void TryDeleteInstancesInRange()
        {
            if (SelectedCategory == null)
            {
                EditorUtility.DisplayDialog("No category selected", "Select category in object pool editor", "Ok");
                return;
            }

            List<PoolInstanceData> instancesInRange = GetInstancesInRange();

            if (instancesInRange.Count == 0)
            {
                EditorUtility.DisplayDialog("No instances", "There is no instances to delete", "Ok");
                return;
            }
            else
            {
                PoolCategoryData category = SelectedCategory;
                if (EditorUtility.DisplayDialog("Are you sure", $"Are you sure about delete {instancesInRange.Count} instances?", "Yes", "Back"))
                    foreach (var instance in instancesInRange)
                        category.RemoveInstance(instance);

            }
        }

        private void TryDeleteInstancesOfName()
        {
            if (SelectedCategory == null)
            {
                EditorUtility.DisplayDialog("No category selected", "Select category in object pool editor", "Ok");
                return;
            }

            List<PoolInstanceData> instancesOfName = GetInstancesOfName();

            if (instancesOfName.Count == 0)
            {
                EditorUtility.DisplayDialog("No instances", "There is no instances to delete", "Ok");
                return;
            }
            else
            {
                PoolCategoryData category = SelectedCategory;
                if (EditorUtility.DisplayDialog("Are you sure", $"Are you sure about delete {instancesOfName.Count} instances?", "Yes", "Back"))
                    foreach (var instance in instancesOfName)
                        category.RemoveInstance(instance);
            }
        }

        private void TryDeleteSelectedPrefabInstances()
        {
            if (SelectedCategory == null)
            {
                EditorUtility.DisplayDialog("No category selected", "Select category in object pool editor", "Ok");
                return;
            }

            List<PoolInstanceData> selectedPrefabs = GetInstancesFromSelected();

            if (selectedPrefabs.Count == 0)
            {
                EditorUtility.DisplayDialog("No instances", "There is no instances to delete", "Ok");
                return;
            }
            else
            {
                PoolCategoryData category = SelectedCategory;
                if (EditorUtility.DisplayDialog("Are you sure", $"Are you sure about delete {selectedPrefabs.Count} instances?", "Yes", "Back"))
                    foreach (var instance in selectedPrefabs)
                        category.RemoveInstance(instance);
            }
        }

        private List<GameObject> GetSelectedObjects()
        {
            List<GameObject> selected = new();
            foreach (var prefab in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets))
            {
                if (prefab is GameObject gameObject)
                    selected.Add(gameObject);
            }

            return selected;
        }

        private List<string> GetPossibleNames()
        {
            List<string> names = new();
            for (int i = startIndex; i < endIndex; i++)
            {
                names.Add(StringBuilderScaler.GetScaledText(NAME_FORM, poolName, i));
            }

            return names;
        }

        private bool CheckPossibleName(out int possibleIndex)
        {
            possibleIndex = startIndex;
            string newName = StringBuilderScaler.GetScaledText(NAME_FORM, poolName, possibleIndex);

            while (SelectedCategory.FindInstanceData(newName) != null)
            {
                possibleIndex++;
                newName = StringBuilderScaler.GetScaledText(NAME_FORM, poolName, possibleIndex);
            }

            return possibleIndex == startIndex;
        }

        private List<PoolInstanceData> GetInstancesFromSelected()
        {
            List<PoolInstanceData> instances = new();

            if (SelectedCategory == null)
                return instances;

            List<GameObject> selected = GetSelectedObjects();

            foreach (var instance in SelectedCategory.Instances)

                if (selected.Contains(instance.PoolObject))
                    instances.Add(instance);

            return instances;
        }

        private List<PoolInstanceData> GetInstancesInRange()
        {
            List<PoolInstanceData> instances = new();

            if (SelectedCategory == null)
                return instances;

            List<string> possibleNames = GetPossibleNames();

            foreach (var instance in SelectedCategory.Instances)
            {
                if (possibleNames.Contains(instance.Name))
                    instances.Add(instance);
            }

            return instances;
        }

        private List<PoolInstanceData> GetInstancesOfName()
        {
            List<PoolInstanceData> instances = new();

            if (SelectedCategory == null)
                return instances;

            foreach (var instance in SelectedCategory.Instances)
            {
                if (instance.Name.Contains(poolName))
                    instances.Add(instance);
            }

            return instances;
        }

        #endregion
    }
}