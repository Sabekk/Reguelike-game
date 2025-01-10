using Gameplay.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character.Controller
{
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
            Character.EquipmentModule.EquipmentController.OnItemEquiped += HandleItemEquiped;
            Character.EquipmentModule.EquipmentController.OnItemUnequiped += HandleItemUnequiped;
        }

        protected override void DetachEvents()
        {
            base.DetachEvents();
            Character.EquipmentModule.EquipmentController.OnItemEquiped -= HandleItemEquiped;
            Character.EquipmentModule.EquipmentController.OnItemUnequiped -= HandleItemUnequiped;
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