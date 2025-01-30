using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Items
{
    [System.Serializable]
    public class BodyCategoryData
    {
        #region VARIABLES

        [SerializeField] private ItemCategory category;
        [SerializeField] private List<EquipmentItemData> itemsData;

        #endregion

        #region PROPERTIES

        public ItemCategory Category => category;
        public List<EquipmentItemData> ItemsData => itemsData;

        #endregion

        #region METHODS

        public EquipmentItemData FindItemData(int itemDataId)
        {
            EquipmentItemData data = ItemsData.Find(x => x.IdEquals(itemDataId));
            if (data)
                return data;

            return null;
        }

        #endregion
    }
}