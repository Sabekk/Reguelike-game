using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling
{
    [CreateAssetMenu(menuName = "Database/ObjectPool", fileName = "ObjectPoolDatabase")]
    public class ObjectPoolDatabase : ScriptableObject
    {
        #region VARIABLES

        public const string GET_POOL_CATEGORIES_METHOD = "@ObjectPoolDatabase.GetCategoriesNames()";

        [SerializeField] private List<PoolCategoryData> poolCategories;

        #endregion

        #region PROPERTIES

        public List<PoolCategoryData> PoolCategories => poolCategories;

        #endregion

        #region METHODS

        public static IEnumerable GetCategoriesNames()
        {
            ValueDropdownList<string> values = new();
            foreach (PoolCategoryData poolCategory in MainDatabases.Instance.ObjectPoolDatabase.PoolCategories)
            {
                values.Add(poolCategory.CategoryName);
            }

            return values;
        }

        public IEnumerable GetCategoryInstancesNames(string category)
        {
            ValueDropdownList<string> values = new();
            foreach (PoolCategoryData poolCategory in MainDatabases.Instance.ObjectPoolDatabase.PoolCategories)
            {
                if (category != poolCategory.CategoryName)
                    continue;

                foreach (var instance in poolCategory.Instances)
                    values.Add(instance.Name);
            }

            return values;
        }

        public void AddCategory(string name)
        {
            poolCategories.Add(new PoolCategoryData(name));
        }

        public void RemoveCategory(PoolCategoryData categoryToDelete)
        {
            poolCategories.Remove(categoryToDelete);
        }

        public PoolCategoryData FindCategoryData(string categoryDataName)
        {
            foreach (var poolCategory in PoolCategories)
            {
                if (poolCategory.CategoryName == categoryDataName)
                    return poolCategory;
            }
            return null;
        }

        #endregion
    }
}
