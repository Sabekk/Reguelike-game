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
        private string[] instanceName;

        #endregion

        #region PROPERTIES

        public string CategoryName => categoryName;
        public string[] InstanceName => instanceName;

        #endregion

        #region METHODS

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

        public T SpawnAndGetObject<T>() where T : Component
        {
            PoolObject polledObject = ObjectPool.Instance.GetFromPool(GetRandomInstanceName(), categoryName);
            polledObject.Prefab.transform.SetParent(transform);
            T spawnedObject = polledObject.GetComponent<T>();
            return spawnedObject;
        }

        public void SpawnObject()
        {
            var spawnedObject = ObjectPool.Instance.GetFromPool(GetRandomInstanceName(), categoryName);
            spawnedObject.Prefab.transform.SetParent(transform);
            spawnedObject.Prefab.transform.localPosition = Vector3.zero;
        }

        private string GetRandomInstanceName()
        {
            return instanceName[UnityEngine.Random.Range(0, instanceName.Length - 1)];
        }

        #endregion
    }
}
