using Gameplay.Character.Animations;
using Gameplay.Character.Controller;
using System;
using UnityEngine;

namespace Gameplay.Character
{
    [Serializable]
    public class PlayerControllersModule : CharacterControllersModule
    {
        #region VARIABLES

        [SerializeField] private CharacterMovementController_Player movementController;
        [SerializeField] private AnimatorStateController_Player animatorStateController;

        #endregion

        #region PROPERTIES

        public CharacterMovementController_Player MovementController => movementController;
        public AnimatorStateController_Player AnimatorStateController => animatorStateController;

        #endregion

        #region METHODS


        public override void SetControllers()
        {
            base.SetControllers();
            controllers.Add(movementController = new());
            controllers.Add(animatorStateController = new());
        }

        #endregion
    }
}
