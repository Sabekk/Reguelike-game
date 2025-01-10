using ObjectPooling;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Items
{
    [System.Serializable]
    public class Item : IIdEqualable
    {
        #region VARIABLES

        [SerializeField, ReadOnly] private int id = Guid.NewGuid().GetHashCode();
        [SerializeField, ReadOnly] private int dataId;

        private ItemData itemData;

        #endregion

        #region PROPERTIES

        public int Id => id;
        public ItemData Data
        {
            get
            {
                if (itemData == null)
                    itemData = MainDatabases.Instance.ItemsDatabase.FindItemData(dataId);
                return itemData;
            }
        }

        #endregion

        #region CONSTRUCTORS

        public Item()
        {

        }

        public Item(ItemData data)
        {
            this.itemData = data;
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
