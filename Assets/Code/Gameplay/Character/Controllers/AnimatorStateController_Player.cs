using GlobalEventSystem;
using System;

namespace Gameplay.Character.Animations
{
    [Serializable]
    public class AnimatorStateController_Player : AnimatorStateController
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
            Player.ControllersModule.MovementController.OnTurnAfterLookingAround += HandleTurningAfterLookingAround;
        }

        protected override void DetachEvents()
        {
            base.DetachEvents();
            Events.Gameplay.Move.OnMoveInDirection -= MoveInDirection;
            Player.ControllersModule.MovementController.OnTurnAfterLookingAround -= HandleTurningAfterLookingAround;
        }

        #region HANDLERS

        private void HandleTurningAfterLookingAround(float degrees)
        {
            //Animacja zawracania o odpowiedni stopieñ
        }


        #endregion

        #endregion
    }
}