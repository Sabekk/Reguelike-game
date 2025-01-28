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

        public Action<Item> OnItemAdded;
        public Action<Item> OnItemRemoved;

        #endregion

        #region VARIABLES

        [SerializeField] private List<Item> items = new();

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        public bool ContainItem(Item item)
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

        private void AddItem(Item item)
        {
            items.Add(item);
            OnItemAdded?.Invoke(item);
        }

        private void RemoveItem(Item item)
        {
            items.Remove(item);
            OnItemRemoved?.Invoke(item);
        }

        #region HANDLERS

        private void HandleItemEquip(Item item)
        {
            RemoveItem(item);
        }

        private void HandleItemUnequip(Item item)
        {
            AddItem(item);
        }

        private void HandleItemCollect(Item item)
        {
            AddItem(item);
        }

        private void HandleItemRemove(Item item)
        {
            RemoveItem(item);
        }

        #endregion

        #endregion
    }
}