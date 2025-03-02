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

        public const string GET_POOL_CATEGORIES_METHOD = "@" + nameof(ObjectPoolDatabase) + "." + nameof(GetCategoriesNames) + "()";
        public const string GET_POOL_ITEMS_METHOD = "@" + nameof(ObjectPoolDatabase) + "." + nameof(GetPoolItems) + "()";
        public const string GET_POOL_BODY_ELEMENTS_METHOD = "@" + nameof(ObjectPoolDatabase) + "." + nameof(GetPoolBodyElements) + "()";
        public const string GET_POOL_CAMERAS_METHOD = "@" + nameof(ObjectPoolDatabase) + "." + nameof(GetPoolCameras) + "()";
        public const string GET_POOL_CHARACTER_BASE_METHOD = "@" + nameof(ObjectPoolDatabase) + "." + nameof(GetPoolBaseCharacters) + "()";
        public const string GET_POOL_CONTAINER_METHOD = "@" + nameof(ObjectPoolDatabase) + "." + nameof(GetPoolContainers) + "()";

        [SerializeField] private List<PoolCategoryData> poolCategories;

        #endregion

        #region PROPERTIES

        public List<PoolCategoryData> PoolCategories => poolCategories;

        #endregion

        #region METHODS

        public static IEnumerable GetCategoriesNames()
        {
            ValueDropdownList<int> values = new();
            foreach (PoolCategoryData poolCategory in MainDatabases.Instance.ObjectPoolDatabase.PoolCategories)
            {
                values.Add(poolCategory.CategoryName, poolCategory.Id);
            }

            return values;
        }

        public static IEnumerable GetCategoriesNamesArray()
        {
            List<string> values = new();
            foreach (PoolCategoryData poolCategory in MainDatabases.Instance.ObjectPoolDatabase.PoolCategories)
            {
                values.Add(poolCategory.CategoryName);
            }

            return values;
        }

        public static IEnumerable GetPoolItems()
        {
            return GetCategoryInstancesIds("Item");
        }

        public static IEnumerable GetPoolBodyElements()
        {
            return GetCategoryInstancesIds("Body");
        }

        public static IEnumerable GetPoolBaseCharacters()
        {
            return GetCategoryInstancesIds("Character");
        }

        public static IEnumerable GetPoolContainers()
        {
            return GetCategoryInstancesIds("Container");
        }

        public static IEnumerable GetPoolCameras()
        {
            return GetCategoryInstancesIds("Camera");
        }

        public IEnumerable GetCategoryInstanceIds(int categoryId)
        {
            ValueDropdownList<int> values = new();
            foreach (PoolCategoryData poolCategory in MainDatabases.Instance.ObjectPoolDatabase.PoolCategories)
            {
                if (!poolCategory.IdEquals(categoryId))
                    continue;

                foreach (var instance in poolCategory.Instances)
                    values.Add(instance.Name, instance.Id);
            }

            return values;
        }

        public static IEnumerable GetCategoryInstancesIds(string categoryName)
        {
            ValueDropdownList<int> values = new();
            PoolCategoryData category = MainDatabases.Instance.ObjectPoolDatabase.FindCategoryData(categoryName);

            if (category != null)
                foreach (var instance in category.Instances)
                    values.Add(instance.Name, instance.Id);

            return values;
        }

        public static IEnumerable GetCategoryInstancesIds(int categoryId)
        {
            ValueDropdownList<int> values = new();
            PoolCategoryData category = MainDatabases.Instance.ObjectPoolDatabase.FindCategoryData(categoryId);

            if (category != null)
                foreach (var instance in category.Instances)
                    values.Add(instance.Name, instance.Id);

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

        public PoolCategoryData FindCategoryData(int categoryDataId)
        {
            foreach (var poolCategory in PoolCategories)
            {
                if (poolCategory.IdEquals(categoryDataId))
                    return poolCategory;
            }
            return null;
        }

        #endregion
    }
}
