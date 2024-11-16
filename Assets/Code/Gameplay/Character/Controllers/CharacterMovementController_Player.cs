using GlobalEventSystem;
using System;
using UnityEngine;

namespace Gameplay.Character.Controller
{
    [Serializable]
    public class CharacterMovementController_Player : CharacterMovementController
    {
        #region VARIABLES

        public event Action OnResetPosition;

        #endregion

        #region VARIABLES

        #endregion

        #region PROPERTIES

        protected Player Player => Character as Player;

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

        protected override void MoveInDirection(Vector2 direction)
        {
            if (Player.ControllersModule.CameraController.NeedReset)
            {
                //Ustaliæ kierunek kamera->player i tak zmienic rotacjê. Dodatkowo utworzyc event z obliczonym kontem o ile zmienia sie pozycja aby wywo³aæ animacje zwrotu
                Player.transform.rotation = Quaternion.Euler(0, 0, 0);
                OnResetPosition?.Invoke();
            }

            base.MoveInDirection(direction);
        }

        #endregion
    }
}