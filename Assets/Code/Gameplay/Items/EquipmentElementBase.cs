using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Items
{
    [System.Serializable]
    public abstract class EquipmentElementBase<T>: IIdEqualable where T : ElementDataBase
    {
        #region VARIABLES

        [SerializeField, ReadOnly] protected int id = Guid.NewGuid().GetHashCode();
        [SerializeField, ReadOnly] protected int dataId;

        protected T elementData;

        #endregion

        #region PROPERTIES

        public int Id => id;
        public abstract T Data { get; }

        #endregion

        #region CONSTRUCTORS

        public EquipmentElementBase()
        {

        }

        public EquipmentElementBase(T data)
        {
            this.elementData = data;
            dataId = data.Id;
        }

        #endregion

        #region METHODS

        public bool IdEquals(int id)
        {
            return Id == id;
        }

        #endregion
    }
}
