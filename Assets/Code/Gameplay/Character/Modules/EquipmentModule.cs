using Gameplay.Character.Body;
using Gameplay.Character.Controller;
using Gameplay.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character
{
    [Serializable]
    public class EquipmentModule : CharacterControllersModule
    {
        #region ACTIONS

        public Action<Item> OnItemCollect;
        public Action<Item> OnItemRemove;
        public Action<Item> OnItemEquip;
        public Action<Item> OnItemUnequip;

        #endregion

        #region VARIABLES

        [SerializeField] private BodyController bodyController;
        [SerializeField] private EquipmentController equipmentController;
        [SerializeField] private InventoryController inventoryController;
        [SerializeField] private EquipmentVisualizationController visualizationController;

        #endregion

        #region PROPERTIES

        public BodyController BodyController => bodyController;
        public EquipmentController EquipmentController => equipmentController;
        public InventoryController InventoryController => inventoryController;
        public EquipmentVisualizationController VisualizationController => visualizationController;

        #endregion

        #region METHODS

        public override void SetControllers()
        {
            base.SetControllers();
            controllers.Add(BodyController);
            controllers.Add(EquipmentController);
            controllers.Add(InventoryController);
            controllers.Add(VisualizationController);
        }

        public bool IsEquiped(Item item)
        {
            return EquipmentController.IsEquiped(item);
        }

        public bool IsItemTypeEquiped(ItemType type, out Item equipedItem)
        {
            return EquipmentController.IsItemTypeEquiped(type, out equipedItem);
        }

        public void EquipItem(Item item)
        {
            if (!InventoryController.ContainItem(item))
                return;

            if (IsItemTypeEquiped(item.Data.ItemType, out Item equipedItem))
                UnequipItem(equipedItem);

            OnItemEquip?.Invoke(item);
        }

        public void UnequipItem(Item item)
        {
            if (!IsEquiped(item))
                return;

            OnItemUnequip?.Invoke(item);
        }

        public void CollectItem(Item item)
        {
            OnItemCollect?.Invoke(item);
        }

        public void RemoveItem(Item item)
        {
            UnequipItem(item);

            if (InventoryController.ContainItem(item))
                OnItemRemove?.Invoke(item);
        }

        #endregion
    }
}