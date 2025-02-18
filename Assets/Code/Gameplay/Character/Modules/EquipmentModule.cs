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

        public Action<ItemBase> OnItemCollect;
        public Action<EquipmentItem> OnItemRemove;
        public Action<EquipmentItem> OnItemEquip;
        public Action<EquipmentItem> OnItemUnequip;

        public Action<BodyItem> OnBodyItemEquip;
        public Action<BodyItem> OnBodyItemUnequip;

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

        #region EQUIP ITEMS

        public bool IsEquiped(EquipmentItem item)
        {
            return EquipmentController.IsEquiped(item);
        }

        public bool IsItemTypeEquiped(EquipmentItemType type, out EquipmentItem equipedItem)
        {
            return EquipmentController.IsItemTypeEquiped(type, out equipedItem);
        }

        public void EquipItem(EquipmentItem item)
        {
            if (!InventoryController.ContainItem(item))
                return;

            if (IsItemTypeEquiped(item.ElementData.EquipmentItemType, out EquipmentItem equipedItem))
                UnequipItem(equipedItem);

            OnItemEquip?.Invoke(item);
        }

        public void UnequipItem(EquipmentItem item)
        {
            if (!IsEquiped(item))
                return;

            OnItemUnequip?.Invoke(item);
        }

        public void CollectItem(ItemBase item)
        {
            switch (item.ItemType)
            {
                case ItemType.BODY:
                    if (item is BodyItem bodyItem)
                        EquipBodyItem(bodyItem);
                    break;
                case ItemType.EQUIPMENT:
                    if (item is EquipmentItem equipmentItem)
                    {
                        OnItemCollect?.Invoke(equipmentItem);
                        EquipItem(equipmentItem);
                    }
                    break;
                default:
                    break;
            }
        }

        public void RemoveItem(EquipmentItem item)
        {
            UnequipItem(item);

            if (InventoryController.ContainItem(item))
                OnItemRemove?.Invoke(item);
        }

        #endregion

        #region BODY ITEMS


        public void EquipBodyItem(BodyItem bodyItem)
        {
            if (BodyController.ItemsInUse.TryGetValue(bodyItem.BodyItemData.ElementType, out _))
            {
                Debug.LogWarning($"[{GetType().Name}] Body item of type [{bodyItem.BodyItemData.ElementType}] is already in use. Check settings");
                return;
            }

            OnBodyItemEquip?.Invoke(bodyItem);
        }

        public void UnequipBodyItem(BodyItem bodyItem)
        {
            OnBodyItemUnequip?.Invoke(bodyItem);
        }

        #endregion

        #endregion
    }
}