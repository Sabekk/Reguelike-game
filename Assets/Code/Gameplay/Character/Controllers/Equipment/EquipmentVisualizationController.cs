using Gameplay.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character.Controller
{
    [Serializable]
    public class EquipmentVisualizationController : ControllerBase
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        protected override void AttachEvents()
        {
            base.AttachEvents();
            Character.EquipmentModule.OnItemEquip += HandleItemEquiped;
            Character.EquipmentModule.OnItemUnequip += HandleItemUnequiped;
        }

        protected override void DetachEvents()
        {
            base.DetachEvents();
            Character.EquipmentModule.OnItemEquip -= HandleItemEquiped;
            Character.EquipmentModule.OnItemUnequip -= HandleItemUnequiped;
        }


        #region HANDLERS

        private void HandleItemEquiped(Item item)
        {

        }

        private void HandleItemUnequiped(Item item)
        {

        }

        #endregion

        #endregion
    }
}