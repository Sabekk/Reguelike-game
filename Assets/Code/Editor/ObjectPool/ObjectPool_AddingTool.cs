using ObjectPooling;
using System.Collections.Generic;
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

            if (GUILayout.Button("Add selected items to pool"))
                TryAddSelectedToPool();
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

            for (int i = 0; i < objects.Count; i++)
            {
                string newName = StringBuilderScaler.GetScaledText(NAME_FORM, poolName, currentIndex);
                while (SelectedCategory.FindInstanceData(newName) != null || namesOfInstances.ContainsValue(newName) == true)
                {
                    isAlternative = true;
                    currentIndex++;
                    newName = StringBuilderScaler.GetScaledText(NAME_FORM, poolName, currentIndex);
                }
                namesOfInstances.Add(objects[i], newName);
            }

            return namesOfInstances;
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

            Dictionary<GameObject, string> instancesNames = GetPossibleNames(selected, out bool isAlternative);
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