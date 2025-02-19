using Gameplay.Items;
using ObjectPooling;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Gameplay.Items
{
    [CreateAssetMenu(menuName = "Database/ItemsDatabse", fileName = "ItemsDatabse")]
    public class ItemsDatabase : ScriptableObject
    {
        #region VARIABLES

        [SerializeField] private BodyItemsContainer bodyItems;
        [SerializeField] private EqupmentItemsContainer equipmentItems;

        #endregion

        #region PROPERTIES

        public BodyItemsContainer BodyItems => bodyItems;
        public EqupmentItemsContainer EquipmentItems => equipmentItems;

        #endregion

        #region METHODS

        public ItemData FindItemData(int itemDataId, ItemType itemType)
        {
            switch (itemType)
            {
                case ItemType.BODY:
                    return BodyItems.FindItemData(itemDataId);
                case ItemType.EQUIPMENT:
                    return EquipmentItems.FindItemData(itemDataId);
                default:
                    break;
            }

            return null;
        }

        public static IEnumerable GetCategoryInstancesNames(ItemType itemType)
        {
            ItemsDatabase database = MainDatabases.Instance.ItemsDatabase;
            ValueDropdownList<int> values = new();
            switch (itemType)
            {
                case ItemType.BODY:
                    return database.BodyItems.GetCategoryInstancesNames();
                case ItemType.EQUIPMENT:
                    return database.EquipmentItems.GetCategoryInstancesNames();
                default:
                    break;
            }

            return values;
        }

        #region EDITOR_METHODS

        [Button]
        private void FullfillDatabase()
        {
            BodyItems.ItemCategories.Clear();
            EquipmentItems.ItemCategories.Clear();

            var itemDataTypes = TypeCache.GetTypesDerivedFrom<ItemData>();
            List<ItemData> items = new();

            foreach (var itemDataType in itemDataTypes)
            {
                items.Clear();

                string name = itemDataType.Name;
                var itemGuids = AssetDatabase.FindAssets($"t:{name}");

                foreach (var itemGuid in itemGuids)
                {
                    string path = AssetDatabase.GUIDToAssetPath(itemGuid);
                    ItemData item = AssetDatabase.LoadAssetAtPath<ItemData>(path);
                    items.Add(item);
                }

                foreach (var item in items)
                {
                    switch (item.ItemType)
                    {
                        case ItemType.BODY:
                            if (item is BodyItemData bodyItemData)
                                BodyItems.TryAddItem(bodyItemData);
                            break;
                        case ItemType.EQUIPMENT:
                            if (item is EquipmentItemData equipmentItemData)
                                EquipmentItems.TryAddItem(equipmentItemData);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        #endregion

        #endregion
    }
}
