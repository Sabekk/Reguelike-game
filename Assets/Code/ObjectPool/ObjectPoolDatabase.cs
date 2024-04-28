using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling
{
    [CreateAssetMenu(menuName = "Database/ObjectPool", fileName = "ObjectPoolDatabase")]
    public class ObjectPoolDatabase : ScriptableSingleton<ObjectPoolDatabase>
    {
        #region VARIABLES

        public const string GET_POOL_CATEGORIES_METHOD = "@ObjectPoolDatabase.GetCategoriesNames()";

        [SerializeField] private List<PoolCategoryData> poolCategories;

        #endregion

        #region PROPERTIES
        public new static ObjectPoolDatabase Instance => GetInstance("Singletons/ObjectPoolDatabase");
        public List<PoolCategoryData> PoolCategories => poolCategories;

        #endregion

        #region METHODS

        public static IEnumerable GetCategoriesNames()
        {
            ValueDropdownList<string> values = new();
            foreach (PoolCategoryData poolCategory in Instance.PoolCategories)
            {
                values.Add(poolCategory.CategoryName);
            }

            return values;
        }

        public IEnumerable GetCategoryInstancesNames(string category)
        {
            ValueDropdownList<string> values = new();
            foreach (PoolCategoryData poolCategory in Instance.PoolCategories)
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

        #endregion
    }
}
