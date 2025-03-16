using Gameplay.Character.Camera;
using GlobalEventSystem;
using System;
using UnityEngine;

namespace Gameplay.Character.Movement
{
    [Serializable]
    public class CharacterMovementModule_Player : CharacterMovementModule
    {
        #region ACTIONS

        public event Action OnStartMoving;

        #endregion

        #region VARIABLES

        #endregion

        #region PROPERTIES

        public bool IsReturningRotation { get; set; }
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
        private CharacterCameraModule CameraController => Player.PlayerMovementController.CameraModule;
        private Quaternion RotationOfCamera => CameraController.RotationOfTarget;
        private bool RotateWithCamera => IsReturningRotation || CameraController.IsFocusing;

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
            if (Player.PlayerMovementController.CameraModule.IsLookingAround)
                return;

            if (RotateWithCamera)
            {
                RotateCharacterToCamera();
                if (IsLookingForward)
                    IsReturningRotation = false;
            }
            else
                base.RotateCharacterByLookDirection();
        }

        protected override void RotateCharacter(Quaternion rotation)
        {
            //Rotate character to camera direction
            if (RotateWithCamera)
                Rb.MoveRotation(rotation);
            else
                base.RotateCharacter(rotation);
        }

        protected override void MoveCharacter()
        {
            if (IsReturningRotation)
                return;
            else
                base.MoveCharacter();
        }
        protected override void MoveInDirection(Vector2 direction)
        {
            if (Player.PlayerMovementController.CameraModule.IsLookingAround)
            {
                IsReturningRotation = true;
                OnStartMoving?.Invoke();
            }

            base.MoveInDirection(direction);
        }

        private void RotateCharacterToCamera()
        {
            IsReturningRotation = true;
            Quaternion cameraRotation = Quaternion.Euler(0, RotationOfCamera.eulerAngles.y, 0);
            Quaternion deltaRotation = Quaternion.RotateTowards(CharacterTransform.rotation, cameraRotation, rotationSpeed / 2/* * Time.fixedDeltaTime*/);
            RotateCharacter(deltaRotation);
        }

        #endregion
    }
}