using GlobalEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character.Animations
{
    public class AnimatorStateController_Player : AnimatorStateController
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        protected override void AttachEvents()
        {
            base.AttachEvents();
            Events.Gameplay.Move.OnMoveInDirection += MoveInDirection;
        }

        protected override void DetachEvents()
        {
            base.DetachEvents();
            Events.Gameplay.Move.OnMoveInDirection -= MoveInDirection;
        }

        #endregion
    }
}