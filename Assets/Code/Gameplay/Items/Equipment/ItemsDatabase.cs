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

        #endregion
    }
}
