using ObjectPooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Items
{
    public class Item : MonoBehaviour, IPoolable
    {
        #region VARIABLES

        [SerializeField] private int dataId;

        private ItemData itemData;

        #endregion

        #region PROPERTIES

        public PoolObject Poolable { get; set; }
        public ItemData Data
        {
            get
            {
                if (itemData == null)
                    itemData = ItemsManager.Instance.FindItemData(dataId);
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

        #endregion
    }
}
