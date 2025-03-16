using Gameplay.Character.Animations;
using Gameplay.Character.Camera;
using System;
using UnityEngine;

namespace Gameplay.Character.Movement
{
    [Serializable]
    public class PlayerMovementController : CharacterMovementController
    {
        #region VARIABLES

        [SerializeField] protected CharacterCameraModule cameraModule;

        #endregion

        #region PROPERTIES

        public CharacterCameraModule CameraModule => cameraModule;
        public CharacterMovementModule_Player PlayerMovementModule => MovementModule as CharacterMovementModule_Player;

        #endregion

        #region METHODS

        public override void SetModules()
        {
            base.SetModules();
            modules.Add(CameraModule);
        }

        public override void CreateModules()
        {
            movementModule = new CharacterMovementModule_Player();
            animatorStateModule = new AnimatorStateModule_Player();
            cameraModule = new CharacterCameraModule();
        }

        #endregion
    }
}
