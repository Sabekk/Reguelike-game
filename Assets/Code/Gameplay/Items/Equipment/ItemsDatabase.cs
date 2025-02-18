using Gameplay.Items;
using ObjectPooling;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Items
{
    [CreateAssetMenu(menuName = "Database/ItemsDatabse", fileName = "ItemsDatabse")]
    public class ItemsDatabase : ScriptableObject
    {
        #region VARIABLES

        [SerializeField] private List<ItemCategoryData> itemCategories;

        #endregion

        #region PROPERTIES

        public List<ItemCategoryData> ItemCategories => itemCategories;

        #endregion

        #region METHODS

        public ItemData FindItemData(int itemDataId, ItemCategory category)
        {
            foreach (var itemCategory in ItemCategories)
            {
                if (itemCategory.Category != category)
                    continue;

                ItemData itemData = itemCategory.FindItemData(itemDataId);
                if (itemData != null)
                    return itemData;
            }

            return null;
        }

        public ItemData FindItemData(int itemDataId)
        {
            foreach (var itemCategory in ItemCategories)
            {
                ItemData itemData = itemCategory.FindItemData(itemDataId);
                if (itemData != null)
                    return itemData;
            }

            return null;
        }

        public static IEnumerable GetCategoryInstancesNames(ItemCategory category)
        {
            ValueDropdownList<int> values = new();
            foreach (ItemCategoryData itemCategory in MainDatabases.Instance.ItemsDatabase.ItemCategories)
            {
                if (itemCategory == null)
                    continue;

                if (category != itemCategory.Category)
                    continue;

                foreach (var itemData in itemCategory.ItemsData)
                {
                    if (itemData != null)
                        values.Add(itemData.ElementName, itemData.Id);
                }
            }

            return values;
        }

        #endregion
    }
}
