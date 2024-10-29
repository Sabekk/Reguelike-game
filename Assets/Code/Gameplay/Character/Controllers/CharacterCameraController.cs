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

        #endregion

        #region PROPERTIES

        private ThreePersonCamera CharacterCamera => ThreePersonCameraManager.Instance != null ? ThreePersonCameraManager.Instance.PersonCameraInGame : null;

        #endregion

        #region METHODS

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
        }

        #endregion
    }
}