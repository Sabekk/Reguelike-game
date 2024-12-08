using GlobalEventSystem;
using System;
using UnityEngine;

namespace Gameplay.Character.Controller
{
    [Serializable]
    public class CharacterMovementController_Player : CharacterMovementController
    {
        #region ACTIONS

        public event Action OnStartMoving;

        #endregion

        #region VARIABLES

        #endregion

        #region PROPERTIES

        public bool IsLookingForward
        {
            get
            {
                Quaternion cameraRotation = Quaternion.Euler(0, RotationOfCamera.eulerAngles.y, 0);
                Quaternion character = Quaternion.Euler(0, CharacterTransform.eulerAngles.y, 0);
                return Quaternion.Angle(cameraRotation, character) == 0;
            }
        }

        protected Player Player => Character as Player;
        private Quaternion RotationOfCamera => Player.ControllersModule.CameraController.RotationOfTarget;

        #endregion

        #region METHODS

        protected override void AttachEvents()
        {
            base.AttachEvents();
            Events.Gameplay.Move.OnJump += Jump;
            Events.Gameplay.Move.OnMoveInDirection += MoveInDirection;
            Events.Gameplay.Move.OnLookInDirection += LookInDirection;
            Events.Gameplay.Move.OnSprint += Sprint;
        }

        protected override void DetachEvents()
        {
            base.DetachEvents();
            Events.Gameplay.Move.OnJump -= Jump;
            Events.Gameplay.Move.OnMoveInDirection -= MoveInDirection;
            Events.Gameplay.Move.OnLookInDirection -= LookInDirection;
            Events.Gameplay.Move.OnSprint -= Sprint;
        }

        protected override void RotateCharacterByLookDirection()
        {
            //Ignoring rotation when player is looking around
            if (Player.ControllersModule.CameraController.IsLookingAround)
                return;

            if (!IsLookingForward)
                RotateCharacterToCamera();
            else
                base.RotateCharacterByLookDirection();
        }

        protected override void RotateCharacter(Quaternion rotation)
        {
            //Rotate character to camera direction
            if (!IsLookingForward)              
                Rb.MoveRotation(rotation);
            else 
                base.RotateCharacter(rotation);
        }

        protected override void MoveInDirection(Vector2 direction)
        {
            if (Player.ControllersModule.CameraController.IsLookingAround)
                OnStartMoving?.Invoke();

            base.MoveInDirection(direction);
        }

        private void RotateCharacterToCamera()
        {
            Quaternion cameraRotation = Quaternion.Euler(0, RotationOfCamera.eulerAngles.y, 0);
            Quaternion deltaRotation = Quaternion.RotateTowards(CharacterTransform.rotation, cameraRotation, rotationSpeed / 2/* * Time.fixedDeltaTime*/);
            RotateCharacter(deltaRotation);
            Debug.Log("ROtate");
        }

        #endregion
    }
}