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
            foreach (PoolCategoryData poolCategory in MainDatabases.Instance.ObjectPoolDatabase.PoolCategories)
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

        public override string GetCategory()
        {
            return CategoryName;
        }

        public override string[] GetVariants()
        {
            return InstanceName;
        }

        #endregion
    }
}
