using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ObjectPooling
{
    [Serializable]
    public class PoolCategoryData
    {
        #region VARIABLES

        [SerializeField] private string categoryName;
        [SerializeField] private List<PoolInstanceData> instances;

        #endregion

        #region PROPERTIES

        public string CategoryName => categoryName;
        public List<PoolInstanceData> Instances => instances;

        #endregion

        #region CONSTRUCTORS

        public PoolCategoryData() { }
        public PoolCategoryData(string name)
        {
            categoryName = name;
        }

        #endregion

        #region METHODS

        public void AddInstance(PoolInstanceData instance)
        {
            instances.Add(instance);
        }

        public void RemoveInstance(PoolInstanceData instance)
        {
            instances.Remove(instance);
        }

        public PoolInstanceData FindInstanceData(string instanceDataName)
        {
            foreach (var instanceData in Instances)
            {
                if (instanceData.Name == instanceDataName)
                    return instanceData;
            }

            return null;
        }

        #endregion
    }
}
