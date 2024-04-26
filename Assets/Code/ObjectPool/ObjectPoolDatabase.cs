using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling
{
    [CreateAssetMenu(menuName = "Database/ObjectPool", fileName = "ObjectPoolDatabase")]
    public class ObjectPoolDatabase : ScriptableSingleton<ObjectPoolDatabase>
    {
        #region VARIABLES

        [SerializeField] private List<PoolCategory> poolCategories;

        #endregion

        #region PROPERTIES
        public new static ObjectPoolDatabase Instance => GetInstance("Singletons/ObjectPoolDatabase");
        public List<PoolCategory> PoolCategories => poolCategories;

        #endregion

        #region METHODS

        public void AddCategory(string name)
        {
            poolCategories.Add(new PoolCategory(name));
        }

        public void RemoveCategory(PoolCategory categoryToDelete)
        {
            poolCategories.Remove(categoryToDelete);
        }

        #endregion
    }
}
