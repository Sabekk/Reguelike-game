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

        public ItemBase CreateItem(ItemDataBase itemData)
        {
            if (itemData is EquipmentItemData equipmentData)
                return new EquipmentItem(equipmentData);
            if (itemData is BodyItemData bodyItemData)
                return new BodyItem(bodyItemData);

            return new ItemBase(itemData);
        }

        public void AddItemToCharacter(CharacterBase character, ItemDataBase itemData)
        {
            AddItemToCharacter(character, CreateItem(itemData));
        }

        public void AddItemToCharacter(CharacterBase character, ItemBase item)
        {
            character.EquipmentModule.CollectItem(item);
        }

        #region EDITOR_METHODS

        [Button]
        private void AddDebugItemToPlayer()
        {
            ItemDataBase itemData = MainDatabases.Instance.ItemsDatabase.FindItemData(debugItemId, debugItemCategory);
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