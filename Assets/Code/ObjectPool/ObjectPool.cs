using System;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling
{
    public class ObjectPool : MonoSingleton<ObjectPool>
    {
        #region VARIABLES

        [SerializeField] private List<PoolCategory> poolCategories;
        [SerializeField] private Transform mainPoolParent;

        #endregion

        #region PROPERTIES

        public Transform MainPoolParent
        {
            get
            {
                if (mainPoolParent == null)
                    mainPoolParent = new GameObject("Pools").transform;

                return mainPoolParent;
            }
        }

        #endregion

        #region UNITY_METHODS

        protected override void Awake()
        {
            base.Awake();
            InitializePools();
        }

        #endregion

        #region METHODS

        public PoolObject GetFromPool(string instanceName, string categoryName = "")
        {
            foreach (var category in poolCategories)
            {
                if (!string.IsNullOrEmpty(categoryName))
                    if (category.Name != categoryName)
                        continue;

                PoolInstance instance = category.TryGetInstance(instanceName);

                if (instance != null)
                    return instance.GetFromPool();
                else
                {
                    Debug.LogError("Pool cannot be found");
                    return null;
                }
            }

            Debug.LogError("Pool cannot be found");
            return null;
        }

        public void ReturnToPool(IPoolable poolableObject)
        {
            PoolObject poolObj = poolableObject.Poolable;

            foreach (var category in poolCategories)
            {
                if (category.Name != poolObj.Category)
                    continue;

                PoolInstance instance = category.TryGetInstance(poolObj.Name);

                if (instance != null)
                    instance.ReturnToPool(poolObj);
                else
                    Debug.LogError("Object is not from pool", poolObj.Prefab);

                break;
            }
        }

        //public void GetAllPoolsOfType(string type, ref List<string> pools)
        //{
        //    foreach (var poolDictionary in poolDictionary)
        //    {
        //        if (!poolDictionary.Key.Contains(type))
        //            continue;
        //        pools.Add(poolDictionary.Key);
        //    }
        //}

        private void InitializePools()
        {
            foreach (var poolCategoryData in ObjectPoolDatabase.Instance.PoolCategories)
                poolCategories.Add(new PoolCategory(poolCategoryData));

            foreach (var poolCategory in poolCategories)
                poolCategory.Initialzie();

        }

        //private Transform GetCategoryParent(string name)
        //{
        //    if (mainPoolParent == null)
        //    {
        //        mainPoolParent = new GameObject("Pools").transform;
        //        mainPoolParent.SetParent(transform);
        //    }
        //    string category = name.Substring(0, name.IndexOf('_'));
        //    if (poolCategory.ContainsKey(category))
        //        return poolCategory[category];
        //    else
        //    {
        //        Transform categoryParent = new GameObject(category).transform;
        //        categoryParent.SetParent(mainPoolParent);
        //        poolCategory.Add(category, categoryParent);
        //        return categoryParent;
        //    }
        //}

        #endregion
    }
}