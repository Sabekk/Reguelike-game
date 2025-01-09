using Gameplay.Character.Controller;
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

        #endregion

        #region PROPERTIES

        public EquipmentController EquipmentController => equipmentController;

        #endregion

        #region METHODS

        public override void Initialize(CharacterBase character)
        {
            base.Initialize(character);
            controllers.Add(equipmentController);
        }

        #endregion
    }
}