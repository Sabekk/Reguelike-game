using Gameplay.Character.Module;
using Gameplay.Items;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character.Equipment
{
    [System.Serializable]
    public class InventoryModule : ModuleBase
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
        public override void AttachEvents()
        {
            base.AttachEvents();
            Character.EquipmentController.OnItemEquip += HandleItemEquip;
            Character.EquipmentController.OnItemUnequip += HandleItemUnequip;

            Character.EquipmentController.OnItemCollect += HandleItemCollect;
            Character.EquipmentController.OnItemRemove += HandleItemRemove;
        }

        public override void DetachEvents()
        {
            base.DetachEvents();
            Character.EquipmentController.OnItemEquip -= HandleItemEquip;
            Character.EquipmentController.OnItemUnequip -= HandleItemUnequip;

            Character.EquipmentController.OnItemCollect -= HandleItemCollect;
            Character.EquipmentController.OnItemRemove -= HandleItemRemove;
        }

        public bool ContainItem(EquipmentItem item)
        {
            return items.ContainsId(item.Id);
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