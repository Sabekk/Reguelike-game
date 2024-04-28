using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling
{
    [CreateAssetMenu(menuName = "Database/ObjectPool", fileName = "ObjectPoolDatabase")]
    public class ObjectPoolDatabase : ScriptableSingleton<ObjectPoolDatabase>
    {
        #region VARIABLES

        [SerializeField] private List<PoolCategoryData> poolCategories;

        #endregion

        #region PROPERTIES
        public new static ObjectPoolDatabase Instance => GetInstance("Singletons/ObjectPoolDatabase");
        public List<PoolCategoryData> PoolCategories => poolCategories;

        #endregion

        #region METHODS

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
