using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Items
{
    [System.Serializable]
    public class ItemCategoryData<T> where T : ItemData
    {
        #region VARIABLES

        [SerializeField] private List<T> itemsData;

        #endregion

        #region PROPERTIES

        public List<T> ItemsData
        {
            get
            {
                if (itemsData == null)
                    itemsData = new();
                return itemsData;
            }
        }

        #endregion

        #region METHODS

        public T FindItemData(int itemDataId)
        {
            T data = ItemsData.Find(x => x.IdEquals(itemDataId));
            if (data)
                return data;

            return null;
        }

        #endregion
    }
}