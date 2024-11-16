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
            Events.Gameplay.Move.OnLookInDirection += LookInDirection;
        }

        protected override void DetachEvents()
        {
            base.DetachEvents();
            Events.Gameplay.Move.OnLookInDirection -= LookInDirection;
        }

        private void LookInDirection(Vector2 direction)
        {
            if (direction == Vector2.zero)
                return;

            currentXRotation = UpdateRotation(currentXRotation, direction.y, CharacterCamera.BottomClamp, CharacterCamera.TopClamp, true);

            if (Character.IsMoving == false)
            {
                currentYRotation = UpdateRotation(currentYRotation, direction.x, float.MinValue, float.MaxValue, false);
                CharacterCamera.UpdatePosition(currentXRotation, currentYRotation);
            }
            else
            {
                currentYRotation = CharacterCamera.Target.eulerAngles.y;
                CharacterCamera.UpdatePosition(currentXRotation);
            }
        }

        private float UpdateRotation(float currentRotation, float input, float min, float max, bool isXAxis)
        {
            currentRotation += isXAxis ? -input : input;
            return Mathf.Clamp(currentRotation, min, max);
        }

        #endregion
    }
}