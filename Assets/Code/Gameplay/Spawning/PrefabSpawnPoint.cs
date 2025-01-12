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

        public bool VariantsContainCategory(int categoryId)
        {
            foreach (var variant in SpawnVariants)
            {
                if (variant.CategoryId == categoryId)
                    return true;
            }

            return false;
        }

        #endregion

        #region EDITOR_METHODS
        public static IEnumerable GetChoosenCategoryInstances(int categoryId)
        {
            ValueDropdownList<int> values = new();
            foreach (PoolCategoryData poolCategory in MainDatabases.Instance.ObjectPoolDatabase.PoolCategories)
            {
                if (poolCategory.IdEquals(categoryId))
                    continue;

                foreach (var instance in poolCategory.Instances)
                    values.Add(instance.Name, instance.Id);
            }

            return values;
        }

        #endregion

    }

    [Serializable]
    public class PrefabSpawnVariant : SpawnVariant
    {
        #region VARIABLES

        public const string GET_POOL_CATEGORY_INSTANCES_METHOD = "@PrefabSpawnPoint.GetChoosenCategoryInstances(categoryid)";

        [SerializeField]
        [ValueDropdown(ObjectPoolDatabase.GET_POOL_CATEGORIES_METHOD)]
        private int categoryid;
        [SerializeField]
        [ValueDropdown(GET_POOL_CATEGORY_INSTANCES_METHOD)]
        private int[] instanceIds;

        #endregion

        #region PROPERTIES

        public int CategoryId => categoryid;
        public int[] InstanceIds => instanceIds;

        #endregion

        #region METHODS

        public override int GetCategoryId()
        {
            return CategoryId;
        }


        public override int[] GetVariantIds()
        {
            return InstanceIds;
        }

        #endregion
    }
}
