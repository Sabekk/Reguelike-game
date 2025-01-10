using ObjectPooling;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Items
{
    public class Item : MonoBehaviour, IPoolable, IIdEqualable
    {
        #region VARIABLES

        [SerializeField] private int id = Guid.NewGuid().GetHashCode();
        [SerializeField] private int dataId;

        private ItemData itemData;

        #endregion

        #region PROPERTIES

        public int Id => id;
        public PoolObject Poolable { get; set; }
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


        public void AssignPoolable(PoolObject poolable)
        {
            Poolable = poolable;
        }

        public bool IdEquals(int id)
        {
            return Id == id;
        }

        #endregion
    }
}
