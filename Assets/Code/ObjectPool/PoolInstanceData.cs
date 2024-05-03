using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ObjectPooling
{
    [Serializable]
    public class PoolInstanceData
    {
        #region VARIABLES

        [SerializeField] private string name;
        [SerializeField] private GameObject poolObject;
        [SerializeField] private int size;

        #endregion

        #region PROPERTIES

        public string Name => name;
        public GameObject PoolObject => poolObject;
        public int Size => size;

        #endregion

        #region METHODS

        #endregion
    }
}
