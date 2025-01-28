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

        protected override void AttachEvents()
        {
            base.AttachEvents();
            Character.EquipmentModule.OnItemEquip += HandleItemEquip;
            Character.EquipmentModule.OnItemUnequip += HandleItemUnequip;
        }

        protected override void DetachEvents()
        {
            base.DetachEvents();
            Character.EquipmentModule.OnItemEquip -= HandleItemEquip;
            Character.EquipmentModule.OnItemUnequip -= HandleItemUnequip;
        }

        #region HANDLERS

        private void HandleItemEquip(Item item)
        {
            itemsInUse.Add(item);
        }

        private void HandleItemUnequip(Item item)
        {
            itemsInUse.Remove(item);
        }

        #endregion

        #endregion
    }
}