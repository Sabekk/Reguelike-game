using Gameplay.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character.Controller
{
    [System.Serializable]
    public class InventoryController : ControllerBase
    {
        #region ACTIONS

        public Action<EquipmentItem> OnItemAdded;
        public Action<EquipmentItem> OnItemRemoved;

        #endregion

        #region VARIABLES

        [SerializeField] private List<EquipmentItem> items = new();

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        public bool ContainItem(EquipmentItem item)
        {
            return items.ContainsId(item.Id);
        }

        protected override void AttachEvents()
        {
            base.AttachEvents();
            Character.EquipmentModule.OnItemEquip += HandleItemEquip;
            Character.EquipmentModule.OnItemUnequip += HandleItemUnequip;

            Character.EquipmentModule.OnItemCollect += HandleItemCollect;
            Character.EquipmentModule.OnItemRemove += HandleItemRemove;
        }

        protected override void DetachEvents()
        {
            base.DetachEvents();
            Character.EquipmentModule.OnItemEquip -= HandleItemEquip;
            Character.EquipmentModule.OnItemUnequip -= HandleItemUnequip;

            Character.EquipmentModule.OnItemCollect -= HandleItemCollect;
            Character.EquipmentModule.OnItemRemove -= HandleItemRemove;
        }

        private void AddItem(EquipmentItem item)
        {
            items.Add(item);
            OnItemAdded?.Invoke(item);
        }

        private void RemoveItem(EquipmentItem item)
        {
            items.Remove(item);
            OnItemRemoved?.Invoke(item);
        }

        #region HANDLERS

        private void HandleItemEquip(EquipmentItem item)
        {
            RemoveItem(item);
        }

        private void HandleItemUnequip(EquipmentItem item)
        {
            AddItem(item);
        }

        private void HandleItemCollect(ItemBase item)
        {
            if (item is EquipmentItem equipmentItem)
                AddItem(equipmentItem);
        }

        private void HandleItemRemove(EquipmentItem item)
        {
            RemoveItem(item);
        }

        #endregion

        #endregion
    }
}