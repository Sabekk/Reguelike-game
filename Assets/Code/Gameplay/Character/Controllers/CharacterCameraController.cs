using Gameplay.Character.Camera;
using GlobalEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character.Controller
{
    public class CharacterCameraController : ControllerBase
    {
        #region VARIABLES

        private float currentXRotation;
        private float currentYRotation;

        #endregion

        #region PROPERTIES

        public bool IsLookingAround { get; set; }
        public Quaternion RotationOfTarget => CharacterCamera.Target.rotation;
        public Quaternion LocalRotationOfTarget => CharacterCamera.Target.localRotation;
        protected Player Player => Character as Player;
        private FollowingCamera CharacterCamera => CamerasManager.Instance != null ? CamerasManager.Instance.PersonCameraInGame : null;

        #endregion

        #region METHODS

        public override void Initialize(CharacterBase character)
        {
            base.Initialize(character);
            CharacterCamera.Initialzie(character);
        }

        protected override void AttachEvents()
        {
            base.AttachEvents();
            Events.Gameplay.Move.OnLookInDirection += HandleLookInDirection;

            if (Player)
                Player.ControllersModule.MovementController.OnTurnAfterLookingAround += HandleTurningAfterLookingAround;
        }

        protected override void DetachEvents()
        {
            base.DetachEvents();
            Events.Gameplay.Move.OnLookInDirection -= HandleLookInDirection;

            if (Player)
                Player.ControllersModule.MovementController.OnTurnAfterLookingAround -= HandleTurningAfterLookingAround;
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

            currentXRotation = UpdateRotation(currentXRotation, direction.y, CharacterCamera.BottomClamp, CharacterCamera.TopClamp, true);

            if (Character.IsMoving == false)
            {
                IsLookingAround = true;

                currentYRotation = UpdateRotation(currentYRotation, direction.x, float.MinValue, float.MaxValue, false);
                CharacterCamera.UpdateXYPosition(currentXRotation, currentYRotation);
            }
            else
            {
                currentYRotation = CharacterCamera.Target.eulerAngles.y;
                CharacterCamera.UpdateXPosition(currentXRotation);
            }
        }

        private void HandleTurningAfterLookingAround(float degrees)
        {
            CharacterCamera.ResetYLocal();

            currentYRotation = Character.transform.eulerAngles.y;
            IsLookingAround = false;
        }

        #endregion

        #endregion
    }
}