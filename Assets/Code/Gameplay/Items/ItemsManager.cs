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

        public EquipmentItem CreateItem(EquipmentItemData itemData)
        {
            return new EquipmentItem(itemData);
        }

        public void AddItemToCharacter(CharacterBase character, EquipmentItemData itemData)
        {
            AddItemToCharacter(character, CreateItem(itemData));
        }

        public void AddItemToCharacter(CharacterBase character, EquipmentItem item)
        {
            character.EquipmentModule.CollectItem(item);
        }

        #region EDITOR_METHODS

        [Button]
        private void AddDebugItemToPlayer()
        {
            EquipmentItemData itemData = MainDatabases.Instance.ItemsDatabase.FindItemData(debugItemId, debugItemCategory);
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