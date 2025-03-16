using Gameplay.Character.Controller;
using Gameplay.Character.Module;
using Gameplay.Items;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character.Equipment
{
    [Serializable]
    public class EquipmentModule : ModuleBase
    {
        #region VARIABLES

        [SerializeField] private List<EquipmentItem> itemsInUse;

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        public bool IsEquiped(EquipmentItem item)
        {
            return itemsInUse.ContainsId(item.Id);
        }

        public bool IsItemTypeEquiped(EquipmentItemType type, out EquipmentItem equipedItem)
        {
            equipedItem = itemsInUse.Find(x => x.ElementData.EquipmentItemType == type);
            return equipedItem != null;
        }

        public override void AttachEvents()
        {
            base.AttachEvents();
            Character.EquipmentController.OnItemEquip += HandleItemEquip;
            Character.EquipmentController.OnItemUnequip += HandleItemUnequip;
        }

        public override void DetachEvents()
        {
            base.DetachEvents();
            Character.EquipmentController.OnItemEquip -= HandleItemEquip;
            Character.EquipmentController.OnItemUnequip -= HandleItemUnequip;
        }

        #region HANDLERS

        private void HandleItemEquip(EquipmentItem item)
        {
            itemsInUse.Add(item);
        }

        private void HandleItemUnequip(EquipmentItem item)
        {
            itemsInUse.Remove(item);
        }

        #endregion

        #endregion
    }
}