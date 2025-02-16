using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Items
{
    [System.Serializable]
    public class ItemCategoryData
    {
        #region VARIABLES

        [SerializeField] private ItemCategory category;
        [SerializeField] private List<ItemDataBase> itemsData;

        #endregion

        #region PROPERTIES

        public ItemCategory Category => category;
        public List<ItemDataBase> ItemsData => itemsData;

        #endregion

        #region METHODS

        public ItemDataBase FindItemData(int itemDataId)
        {
            ItemDataBase data = ItemsData.Find(x => x.IdEquals(itemDataId));
            if (data)
                return data;

            return null;
        }

        #endregion
    }
}