using Gameplay.Character.Animations;
using Gameplay.Character.Camera;
using Gameplay.Character.Controller;
using System;
using UnityEngine;

namespace Gameplay.Character.Movement
{
    [Serializable]
    public class PlayerMovementController : CharacterControllerWithModules
    {
        #region VARIABLES

        [SerializeField] private CharacterMovementModule_Player movementModule;
        [SerializeField] private AnimatorStateModule_Player animatorStateModule;
        [SerializeField] private CharacterCameraModule cameraModule;

        #endregion

        #region PROPERTIES

        public CharacterMovementModule_Player MovementModule => movementModule;
        public AnimatorStateModule_Player AnimatorStateModule => animatorStateModule;
        public CharacterCameraModule CameraModule => cameraModule;

        #endregion

        #region METHODS


        public override void SetModules()
        {
            base.SetModules();
            modules.Add(movementModule = new());
            modules.Add(animatorStateModule = new());
            modules.Add(cameraModule = new());
        }

        #endregion
    }
}
