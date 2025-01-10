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
            Character.EquipmentModule.EquipmentController.OnItemEquiped += RemoveItem;
            Character.EquipmentModule.EquipmentController.OnItemUnequiped += AddItem;
        }

        protected override void DetachEvents()
        {
            base.DetachEvents();
            Character.EquipmentModule.EquipmentController.OnItemEquiped -= RemoveItem;
            Character.EquipmentModule.EquipmentController.OnItemUnequiped -= AddItem;
        }

        public void AddItem(Item item)
        {
            if (items.ContainsId(item.Id))
                return;

            items.Add(item);
            OnItemAdded?.Invoke(item);
        }

        public void RemoveItem(Item item)
        {
            if (!items.ContainsId(item.Id))
                return;

            items.Remove(item);
            OnItemRemoved?.Invoke(item);
        }

        #endregion
    }
}