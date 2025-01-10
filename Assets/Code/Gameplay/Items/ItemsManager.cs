using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using ObjectPooling;
using Gameplay.Character;

namespace Gameplay.Items
{
    public class ItemsManager : GameplayManager<ItemsManager>
    {
        #region VARIABLES

        [SerializeField, FoldoutGroup("DEBUG")] private Player player;
        [SerializeField, FoldoutGroup("DEBUG")] private ItemCategory debugItemCategory;
        [SerializeField, FoldoutGroup("DEBUG"), ValueDropdown(nameof(GetCategoryInstancesNames))] private int debugItemId;

        #endregion

        #region PROPERTIES

        private ItemsDatabse Database => MainDatabases.Instance.ItemsDatabase;

        #endregion

        #region METHODS

        public Item CreateItem(ItemData itemData)
        {
            return new Item(itemData);
        }

        public void AddItemToCharacter(CharacterBase character, ItemData itemData)
        {
            AddItemToCHaracter(character, CreateItem(itemData));
        }

        public void AddItemToCHaracter(CharacterBase character, Item item)
        {
            character.EquipmentModule.InventoryController.AddItem(item);
        }

        #region EDITOR_METHODS

        [Button]
        private void AddDebugItemToPlayer()
        {
            ItemData itemData = MainDatabases.Instance.ItemsDatabase.FindItemData(debugItemId, debugItemCategory);
            if (itemData == null)
                return;

            AddItemToCharacter(player, itemData);
        }


        public IEnumerable GetCategoryInstancesNames()
        {
            return ItemsDatabse.GetCategoryInstancesNames(debugItemCategory);
        }

        #endregion

        #endregion
    }
}