using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Items
{
    [Serializable]
    public class EqupmentItemsContainer : ItemDatabaseContainer<EquipmentCategoryData, EquipmentItemData>
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        public override ItemType ItemsType => ItemType.EQUIPMENT;

        #endregion

        #region METHODS

        private EquipmentCategoryData TryGetCategoryForType(EquipmentItemType elementType)
        {
            return ItemCategories.Find(x => x.ElementType == elementType);
        }


#if UNITY_EDITOR

        public override void TryAddItem(EquipmentItemData itemData)
        {
            EquipmentCategoryData categoryData = TryGetCategoryForType(itemData.EquipmentItemType);

            if (categoryData == null)
            {
                categoryData = new EquipmentCategoryData(itemData.EquipmentItemType);
                ItemCategories.Add(categoryData);
            }
            else if (categoryData.ItemsData.ContainsId(itemData.Id))
                return;

            categoryData.ItemsData.Add(itemData);
        }

#endif

        #endregion
    }
}