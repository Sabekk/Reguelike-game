using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Items
{
    [Serializable]
    public abstract class ItemDatabaseContainer<T1, T2> where T1 : ItemCategoryData<T2> where T2 : ItemData
    {
        #region VARIABLES

        [SerializeField] protected List<T1> itemCategories;

        #endregion

        #region PROPERTIES

        public abstract ItemType ItemsType { get; }
        public List<T1> ItemCategories => itemCategories;

        #endregion

        #region METHODS

        public T2 FindItemData(int itemDataId)
        {
            foreach (var itemCategory in ItemCategories)
            {
                T2 itemData = itemCategory.FindItemData(itemDataId);
                if (itemData != null)
                    return itemData;
            }

            return null;
        }

        public IEnumerable GetCategoryInstancesNames()
        {
            ValueDropdownList<int> values = new();
            foreach (ItemCategoryData<T2> itemCategory in ItemCategories)
            {
                if (itemCategory == null)
                    continue;

                foreach (var itemData in itemCategory.ItemsData)
                {
                    if (itemData != null)
                        values.Add(itemData.ElementName, itemData.Id);
                }
            }

            return values;
        }

        #region EDITOR_METHODS

#if UNITY_EDITOR

        public abstract void TryAddItem(T2 itemData);

#endif

        #endregion

        #endregion
    }
}
