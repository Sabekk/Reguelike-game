using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

namespace ObjectPooling
{
    [Serializable]
    public class PoolCategoryData : IIdEqualable
    {
        #region VARIABLES

        [SerializeField, ReadOnly] private int id = Guid.NewGuid().GetHashCode();
        [SerializeField] private string categoryName;
        [SerializeField] private List<PoolInstanceData> instances;

        #endregion

        #region PROPERTIES

        public int Id => id;
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

        public PoolInstanceData FindInstanceData(int instanceDataId)
        {
            foreach (var instanceData in Instances)
            {
                if (instanceData.IdEquals(instanceDataId))
                    return instanceData;
            }

            return null;
        }

        public bool IdEquals(int id)
        {
            return Id == id;
        }

        #endregion
    }
}
