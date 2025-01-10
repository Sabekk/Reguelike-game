using Gameplay.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character.Controller
{
    [System.Serializable]
    public class EquipmentController : ControllerBase
    {
        #region ACTIONS

        public Action<Item> OnItemEquiped;
        public Action<Item> OnItemUnequiped;

        #endregion

        #region VARIABLES

        [SerializeField] private List<Item> itemsInUse;

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        public bool IsEquiped(Item item)
        {
            return itemsInUse.ContainsId(item.Id);
        }

        public bool IsItemTypeEquiped(ItemType type, out Item equipedItem)
        {
            equipedItem = itemsInUse.Find(x => x.Data.ItemType == type);
            return equipedItem != null;
        }

        public void EquipItem(Item item)
        {
            if (!Character.EquipmentModule.InventoryController.ContainItem(item))
                return;

            if (IsItemTypeEquiped(item.Data.ItemType, out Item equipedItem))
                UnequipItem(equipedItem);

            itemsInUse.Add(item);

            OnItemEquiped?.Invoke(item);
        }

        public void UnequipItem(Item item)
        {
            if (!IsEquiped(item))
                return;

            itemsInUse.Remove(item);

            OnItemUnequiped?.Invoke(item);
        }

        #endregion
    }
}