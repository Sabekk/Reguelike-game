using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
using ObjectPooling;

namespace Gameplay.Arena
{
    [Serializable]
    public class SpawnPoint : MonoBehaviour
    {
        #region VARIABLES

        public const string GET_POOL_CATEGORY_INSTANCES_METHOD = "@GetChoosenCategoryInstances(categoryName)";

        [SerializeField]
        [ValueDropdown(ObjectPoolDatabase.GET_POOL_CATEGORIES_METHOD)] 
        public string categoryName;
        [SerializeField]
        [ValueDropdown(GET_POOL_CATEGORY_INSTANCES_METHOD)]
        private string instanceName;

        [SerializeField]
        private bool useObjectPool = true;

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        private void Start()
        {
            SpawnObject();
        }

        public static IEnumerable GetChoosenCategoryInstances(string categoryName)
        {
            ValueDropdownList<string> values = new();
            foreach (PoolCategoryData poolCategory in ObjectPoolDatabase.Instance.PoolCategories)
            {
                if (categoryName != poolCategory.CategoryName)
                    continue;

                foreach (var instance in poolCategory.Instances)
                    values.Add(instance.Name);
            }

            return values;
        }

        public void SpawnObject()
        {
            if (useObjectPool)
            {
                var gameObj = ObjectPool.Instance.GetFromPool(instanceName, categoryName);
                gameObj.Prefab.transform.SetParent(transform);
            }
        }

        #endregion
    }
}
