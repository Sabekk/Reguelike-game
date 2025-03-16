using Gameplay.Character.Module;
using Gameplay.Character.Movement;
using GlobalEventSystem;
using System;
using UnityEngine;

namespace Gameplay.Character.Camera
{
    [Serializable]
    public class CharacterCameraModule : ModuleBase
    {
        #region VARIABLES

        [SerializeField] private Transform lookTransform;

        private float currentXRotation;
        private float currentYRotation;

        private Quaternion savedRotation;

        #endregion

        #region PROPERTIES

        public bool IsFocusing => lookTransform != null;
        public bool IsLookingAround { get; set; }
        public Quaternion RotationOfTarget => CharacterCamera.Target.rotation;
        private Player Player => Character as Player;
        private FollowingCamera CharacterCamera => CamerasManager.Instance != null ? CamerasManager.Instance.PersonCameraInGame : null;
        private CharacterMovementModule_Player MovementController => Player.PlayerMovementController.MovementModule;

        #endregion

        #region METHODS

        public override void Initialize(CharacterBase character)
        {
            base.Initialize(character);
            CharacterCamera.Initialzie(character);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (IsFocusing)
            {
                CharacterCamera.Target.LookAt(lookTransform);
            }
            else
            {
                if (MovementController.IsReturningRotation)
                    CharacterCamera.UpdateRotation(savedRotation);
            }
        }

        protected override void AttachEvents()
        {
            base.AttachEvents();
            Events.Gameplay.Move.OnLookInDirection += HandleLookInDirection;

            if (Player)
                MovementController.OnStartMoving += HandleTurningAfterLookingAround;
        }

        protected override void DetachEvents()
        {
            base.DetachEvents();
            Events.Gameplay.Move.OnLookInDirection -= HandleLookInDirection;

            if (Player)
                MovementController.OnStartMoving -= HandleTurningAfterLookingAround;
        }

        private float UpdateRotation(float currentRotation, float input, float min, float max, bool isXAxis)
        {
            currentRotation += isXAxis ? -input : input;
            return Mathf.Clamp(currentRotation, min, max);
        }

        #region HANDLERS

        private void HandleLookInDirection(Vector2 direction)
        {
            if (direction == Vector2.zero)
                return;

            if (IsFocusing)
                return;

            currentXRotation = UpdateRotation(currentXRotation, direction.y, CharacterCamera.BottomClamp, CharacterCamera.TopClamp, true);

            if (Character.IsMoving == false)
            {
                IsLookingAround = true;

                currentYRotation = UpdateRotation(currentYRotation, direction.x, float.MinValue, float.MaxValue, false);
                CharacterCamera.UpdateXYRotation(currentXRotation, currentYRotation);
            }
            else
            {
                currentYRotation = CharacterCamera.Target.eulerAngles.y;
                CharacterCamera.UpdateXRotation(currentXRotation);
            }


            if (MovementController.IsLookingForward == false)
                savedRotation = CharacterCamera.Target.rotation;
        }

        private void HandleTurningAfterLookingAround()
        {
            IsLookingAround = false;
        }

        #endregion

        #endregion
    }
}