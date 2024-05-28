using ObjectPooling;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;

namespace Gameplay.Arena
{
    public class PrefabSpawnPoint : SpawnPoint<PrefabSpawnVariant>
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        public T SpawnAndGetObject<T>() where T : Component
        {
            return SpawnAndGetObject<T>(ArenaManager.Instance.CurrentBiom);
        }

        public T SpawnAndGetObject<T>(BiomType biomType) where T : Component
        {
            ClearChildren();

            PrefabSpawnVariant variant = GetVariantForBiom(biomType);
            if (variant == null)
            {
                Debug.LogError(StringBuilderScaler.GetScaledText("Niepoprawnie ustawiony biom dla spawn point! {0}", biomType, this));
                variant = GetVariantForBiom(BiomType.DEFAULT);
            }
            if (variant == null)
                return null;

            PoolObject polledObject = ObjectPool.Instance.GetFromPool(variant.GetRandomInstanceName(variant.InstanceName), variant.CategoryName);
            polledObject.Prefab.transform.SetParent(transform);
            T spawnedObject = polledObject.GetComponent<T>();
            return spawnedObject;
        }

        public void SpawnObject()
        {
            SpawnObject(ArenaManager.Instance.CurrentBiom, true);
        }

        public void SpawnObject(BiomType biomType, bool useObjectPool)
        {
            ClearChildren();

            PrefabSpawnVariant variant = GetVariantForBiom(biomType);
            if (variant == null)
            {
                Debug.LogError(StringBuilderScaler.GetScaledText("Niepoprawnie ustawiony biom dla spawn point! {0}", biomType, this));
                variant = GetVariantForBiom(BiomType.DEFAULT);
            }
            if (variant == null)
                return;

            string instanceName = variant.GetRandomInstanceName(variant.InstanceName);
            GameObject spawnedObject = null;

            if (useObjectPool)
                spawnedObject = ObjectPool.Instance.GetFromPool(instanceName, variant.CategoryName).Prefab;
            else
            {
                PoolCategoryData poolCategoryData = ObjectPoolDatabase.Instance.FindCategoryData(variant?.CategoryName);
                if (poolCategoryData == null)
                    return;

                PoolInstanceData poolInstanceData = poolCategoryData.FindInstanceData(instanceName);
                if (poolInstanceData == null)
                    return;

                spawnedObject = Instantiate(poolInstanceData.PoolObject);
            }

            if (spawnedObject != null)
            {
                spawnedObject.transform.SetParent(transform);
                spawnedObject.transform.localPosition = Vector3.zero;
            }
        }

        public bool VariantsContainCategory(string categoryName)
        {
            foreach (var variant in SpawnVariants)
            {
                if (variant.CategoryName == categoryName)
                    return true;
            }

            return false;
        }

        #endregion

        #region EDITOR_METHODS
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

        #endregion

    }

    [Serializable]
    public class PrefabSpawnVariant : SpawnVariant
    {
        #region VARIABLES

        public const string GET_POOL_CATEGORY_INSTANCES_METHOD = "@PrefabSpawnPoint.GetChoosenCategoryInstances(categoryName)";

        [SerializeField]
        [ValueDropdown(ObjectPoolDatabase.GET_POOL_CATEGORIES_METHOD)]
        private string categoryName;
        [SerializeField]
        [ValueDropdown(GET_POOL_CATEGORY_INSTANCES_METHOD)]
        private string[] instanceName;

        #endregion

        #region PROPERTIES

        public string CategoryName => categoryName;
        public string[] InstanceName => instanceName;

        #endregion

        #region METHODS

        #endregion
    }
}
