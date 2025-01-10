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
        #region VARIABLES

        [SerializeField] private EquipmentController equipmentController;
        [SerializeField] private InventoryController inventoryController;

        #endregion

        #region PROPERTIES

        public EquipmentController EquipmentController => equipmentController;
        public InventoryController InventoryController => inventoryController;

        #endregion

        #region METHODS

        public override void Initialize(CharacterBase character)
        {
            base.Initialize(character);
            controllers.Add(equipmentController);
            controllers.Add(inventoryController);
        }

        #endregion
    }
}