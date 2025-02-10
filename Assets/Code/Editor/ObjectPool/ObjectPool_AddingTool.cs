using ObjectPooling;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class ObjectPool_AddingTool : EditorWindow
    {
        #region VARIABLES

        [SerializeField] private string poolName;
        [SerializeField] private int startIndex;
        [SerializeField] private int itemSize;
        [SerializeField] private bool useIndexOfObjects;

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
            itemSize = EditorGUILayout.IntField("Pool size", itemSize);
            useIndexOfObjects = EditorGUILayout.Toggle("Use index of objects", useIndexOfObjects);

            if (GUILayout.Button("Add selected items to pool"))
            {
                TryAddSelectedToPool();
            }
        }

        #endregion

        #region METHODS

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

        private Dictionary<GameObject, string> GetPossibleNames(List<GameObject> objects, out bool isAlternative)
        {
            Dictionary<GameObject, string> namesOfInstances = new();
            int currentIndex = startIndex;
            isAlternative = false;

            for (int i = objects.Count - 1; i >= 0; i--)
            {
                string newName = StringBuilderScaler.GetScaledText(NAME_FORM, poolName, currentIndex);
                while (SelectedCategory.FindInstanceData(newName) != null || namesOfInstances.ContainsValue(newName) == true)
                {
                    isAlternative = true;
                    currentIndex++;
                    newName = StringBuilderScaler.GetScaledText(NAME_FORM, poolName, currentIndex);
                }
                namesOfInstances.Add(objects[i], newName);
                currentIndex++;
            }

            return namesOfInstances;
        }

        private Dictionary<GameObject, string> GetPossibleNamesOfObjectsIndexes(List<GameObject> objects, out bool isAlternative)
        {
            Dictionary<int, KeyValuePair<GameObject, string>> possibleInstancesWithIndex = new();
            Dictionary<GameObject, string> namesOfInstances = new();
            int currentIndex = startIndex;
            isAlternative = false;

            bool alternativeItem = false;

            for (int i = objects.Count - 1; i >= 0; i--)
            {
                int indexOfPoolName = GetIndexFromName(objects[i].name);
                if (indexOfPoolName < 0)
                    indexOfPoolName = currentIndex;

                alternativeItem = false;
                string newName = StringBuilderScaler.GetScaledText(NAME_FORM, poolName, indexOfPoolName);

                while (SelectedCategory.FindInstanceData(newName) != null || namesOfInstances.ContainsValue(newName) == true)
                {
                    alternativeItem = true;
                    isAlternative = true;
                    currentIndex++;
                    newName = StringBuilderScaler.GetScaledText(NAME_FORM, poolName, currentIndex);
                }

                possibleInstancesWithIndex.Add(alternativeItem ? currentIndex : indexOfPoolName, new KeyValuePair<GameObject, string>(objects[i], newName));
                currentIndex++;
            }

            foreach (var nameOfInstance in possibleInstancesWithIndex.OrderBy(x => x.Key))
            {
                namesOfInstances.Add(nameOfInstance.Value.Key, nameOfInstance.Value.Value);
            }

            return namesOfInstances;
        }

        private int GetIndexFromName(string name)
        {
            int index = -1;

            int lastCharToIndex = name.LastIndexOf("_");

            if (lastCharToIndex >= 0)
            {
                string indexName = name.Remove(0, lastCharToIndex + 1);

                if (int.TryParse(indexName, out int indexFromName))
                    index = indexFromName;
            }

            return index;
        }

        private void TryAddSelectedToPool()
        {
            if (SelectedCategory == null)
            {
                EditorUtility.DisplayDialog("No category selected", "Select category in object pool editor", "Ok");
                return;
            }

            List<GameObject> selected = GetSelectedObjects();

            if (selected.Count == 0)
            {
                EditorUtility.DisplayDialog("None selected", "None prefabs are seleted to add", "Ok");
                return;
            }

            bool isAlternative;
            Dictionary<GameObject, string> instancesNames = useIndexOfObjects ? GetPossibleNamesOfObjectsIndexes(selected, out isAlternative) : GetPossibleNames(selected, out isAlternative);
            if (isAlternative)
            {
                if (EditorUtility.DisplayDialog("Duplicate", "There is one or more name of iterated names are duplicated.", "Use possible names", "Back"))
                    AddToPool(instancesNames);
            }
            else
                AddToPool(instancesNames);
        }

        private void AddToPool(Dictionary<GameObject, string> instancesNames)
        {
            foreach (var instanceName in instancesNames)
            {
                PoolInstanceData data = new PoolInstanceData(instanceName.Value, instanceName.Key, itemSize);
                SelectedCategory.AddInstance(data);
            }
        }

        #endregion
    }
}