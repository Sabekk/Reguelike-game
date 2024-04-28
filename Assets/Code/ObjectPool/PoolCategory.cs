using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling
{
    [Serializable]
    public class PoolCategory
    {
        #region VARIABLES

        [SerializeField] private PoolCategoryData data;
        [SerializeField] private Transform categoryTransform;
        [SerializeField] private List<PoolInstance> instances;

        #endregion

        #region PROPERTIES

        public string Name => data.CategoryName;

        #endregion

        #region CONSTRUCTORS

        public PoolCategory() { }
        public PoolCategory(PoolCategoryData data)
        {
            this.data = data;
        }

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        public void Initialzie()
        {
            instances = new List<PoolInstance>();

            categoryTransform = new GameObject(data.CategoryName).transform;
            categoryTransform.SetParent(ObjectPool.Instance.MainPoolParent);

            foreach (var instanceData in data.Instances)
                instances.Add(new PoolInstance(instanceData, data.CategoryName, categoryTransform));
        }

        public PoolInstance TryGetInstance(string instanceName)
        {
            foreach (var instance in instances)
            {
                if (instance.Name == instanceName)
                    return instance;
            }
            return null;
        }

        #endregion
    }
}