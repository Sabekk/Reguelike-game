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

        public Action<EquipmentItem> OnItemCollect;
        public Action<EquipmentItem> OnItemRemove;
        public Action<EquipmentItem> OnItemEquip;
        public Action<EquipmentItem> OnItemUnequip;

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

        public bool IsEquiped(EquipmentItem item)
        {
            return EquipmentController.IsEquiped(item);
        }

        public bool IsItemTypeEquiped(ItemType type, out EquipmentItem equipedItem)
        {
            return EquipmentController.IsItemTypeEquiped(type, out equipedItem);
        }

        public void EquipItem(EquipmentItem item)
        {
            if (!InventoryController.ContainItem(item))
                return;

            if (IsItemTypeEquiped(item.Data.ItemType, out EquipmentItem equipedItem))
                UnequipItem(equipedItem);

            OnItemEquip?.Invoke(item);
        }

        public void UnequipItem(EquipmentItem item)
        {
            if (!IsEquiped(item))
                return;

            OnItemUnequip?.Invoke(item);
        }

        public void CollectItem(EquipmentItem item)
        {
            OnItemCollect?.Invoke(item);
        }

        public void RemoveItem(EquipmentItem item)
        {
            UnequipItem(item);

            if (InventoryController.ContainItem(item))
                OnItemRemove?.Invoke(item);
        }

        #endregion
    }
}