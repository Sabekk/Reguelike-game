using GlobalEventSystem;
using System;

namespace Gameplay.Character.Animations
{
    [Serializable]
    public class AnimatorStateModule_Player : AnimatorStateController
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        protected Player Player => Character as Player;

        #endregion

        #region METHODS

        protected override void AttachEvents()
        {
            base.AttachEvents();
            Events.Gameplay.Move.OnMoveInDirection += MoveInDirection;
            Player.PlayerMovementController.MovementModule.OnStartMoving += HandleStartMoving;
        }

        protected override void DetachEvents()
        {
            base.DetachEvents();
            Events.Gameplay.Move.OnMoveInDirection -= MoveInDirection;
            Player.PlayerMovementController.MovementModule.OnStartMoving -= HandleStartMoving;
        }

        #region HANDLERS

        private void HandleStartMoving()
        {
            //Animacja zawracania o odpowiedni stopieñ
        }


        #endregion

        #endregion
    }
}