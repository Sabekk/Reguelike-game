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

        private ItemsDatabase Database => MainDatabases.Instance.ItemsDatabase;

        #endregion

        #region METHODS

        public ItemBase CreateItem(ItemData itemData)
        {
            switch (itemData.ItemType)
            {
                case ItemType.BODY:
                    if (itemData is BodyItemData bodyItemData)
                        return new BodyItem(bodyItemData);
                    break;
                case ItemType.EQUIPMENT:
                    if (itemData is EquipmentItemData equipmentData)
                        return new EquipmentItem(equipmentData);
                    break;
                default:
                    break;
            }

            return new ItemBase(itemData);
        }

        public void AddItemToCharacter(CharacterBase character, ItemData itemData)
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
            ItemData itemData = MainDatabases.Instance.ItemsDatabase.FindItemData(debugItemId, debugItemCategory);
            if (itemData == null)
                return;

            AddItemToCharacter(player, itemData);
        }


        public IEnumerable GetCategoryInstancesNames()
        {
            return ItemsDatabase.GetCategoryInstancesNames(debugItemCategory);
        }

        #endregion

        #endregion
    }
}