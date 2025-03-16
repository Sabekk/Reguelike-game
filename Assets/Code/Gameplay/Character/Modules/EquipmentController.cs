using Gameplay.Character.Controller;
using Gameplay.Items;
using System;
using UnityEngine;

namespace Gameplay.Character.Equipment
{
    [Serializable]
    public class EquipmentController : CharacterControllerBase
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

        [SerializeField] private BodyModule bodyModule;
        [SerializeField] private EquipmentModule equipmentModule;
        [SerializeField] private InventoryModule inventoryModule;
        [SerializeField] private EquipmentVisualizationModule visualizationModule;

        #endregion

        #region PROPERTIES

        public BodyModule BodyModule => bodyModule;
        public EquipmentModule EquipmentModule => equipmentModule;
        public InventoryModule InventoryModule => inventoryModule;
        public EquipmentVisualizationModule VisualizationModule => visualizationModule;

        #endregion

        #region METHODS

        public override void CreateModules()
        {
            base.CreateModules();
            bodyModule = new BodyModule();
            equipmentModule = new EquipmentModule();
            inventoryModule = new InventoryModule();
            visualizationModule = new EquipmentVisualizationModule();
        }

        public override void SetModules()
        {
            base.SetModules();
            modules.Add(BodyModule);
            modules.Add(EquipmentModule);
            modules.Add(InventoryModule);
            modules.Add(VisualizationModule);
        }

        #region EQUIP ITEMS

        public bool IsEquiped(EquipmentItem item)
        {
            return EquipmentModule.IsEquiped(item);
        }

        public bool IsItemTypeEquiped(EquipmentItemType type, out EquipmentItem equipedItem)
        {
            return EquipmentModule.IsItemTypeEquiped(type, out equipedItem);
        }

        public void EquipItem(EquipmentItem item)
        {
            if (!InventoryModule.ContainItem(item))
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

            if (InventoryModule.ContainItem(item))
                OnItemRemove?.Invoke(item);
        }

        #endregion

        #region BODY ITEMS


        public void EquipBodyItem(BodyItem bodyItem)
        {
            if (BodyModule.ItemsInUse.TryGetValue(bodyItem.BodyItemData.ElementType, out _))
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