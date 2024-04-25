using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ObjectPooling
{
    [Serializable]
    public class PoolCategory
    {
        #region VARIABLES

        [SerializeField] private string categoryName;
        [SerializeField] private List<PoolInstance> instances;

        #endregion

        #region PROPERTIES

        public string CategoryName => categoryName;
        public List<PoolInstance> Instances => instances;

        #endregion

        #region METHODS

        public void AddInstance(PoolInstance instance)
        {
            instances.Add(instance);
        }

        public void RemoveInstance(PoolInstance instance)
        {
            instances.Remove(instance);
        }

        #endregion
    }
}
