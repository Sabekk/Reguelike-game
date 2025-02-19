using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Items
{
    [Serializable]
    public class BodyItemsContainer : ItemDatabaseContainer<BodyCategoryData, BodyItemData>
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        public override ItemType ItemsType => ItemType.BODY;

        #endregion

        #region METHODS

        public IEnumerable GetCategoryInstancesNames(BodyElementType elementType)
        {
            ValueDropdownList<int> values = new();
            foreach (BodyCategoryData itemCategory in ItemCategories)
            {
                if (itemCategory == null)
                    continue;

                if (itemCategory.ElementType != elementType)
                    continue;

                foreach (var itemData in itemCategory.ItemsData)
                {
                    if (itemData != null)
                        values.Add(itemData.ElementName, itemData.Id);
                }
            }

            return values;
        }

        private BodyCategoryData TryGetCategoryForType(BodyElementType bodyElementType)
        {
            return ItemCategories.Find(x => x.ElementType == bodyElementType);
        }

        #region UNITY_METHODS

#if UNITY_EDITOR

        public override void TryAddItem(BodyItemData itemData)
        {
            BodyCategoryData categoryData = TryGetCategoryForType(itemData.ElementType);

            if (categoryData == null)
            {
                categoryData = new BodyCategoryData(itemData.ElementType);
                ItemCategories.Add(categoryData);
            }
            else if (categoryData.ItemsData.ContainsId(itemData.Id))
                return;

            categoryData.ItemsData.Add(itemData);
        }

#endif

        #endregion

        #endregion
    }
}