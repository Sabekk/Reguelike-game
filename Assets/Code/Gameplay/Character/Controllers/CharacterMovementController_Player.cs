using GlobalEventSystem;
using System;
using UnityEngine;

namespace Gameplay.Character.Controller
{
    [Serializable]
    public class CharacterMovementController_Player : CharacterMovementController
    {
        #region VARIABLES

        /// <summary>
        /// Action with degrees of turning
        /// </summary>
        public event Action<float> OnTurnAfterLookingAround;

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
            if (Player.ControllersModule.CameraController.IsLookingAround)
            {
                //Ustaliæ kierunek kamera->player i tak zmienic rotacjê. Dodatkowo utworzyc event z obliczonym kontem o ile zmienia sie pozycja aby wywo³aæ animacje zwrotu

                float rotateY = Player.ControllersModule.CameraController.RotationOfTarget.eulerAngles.y;

                CharacterTransform.rotation = Quaternion.Euler(CharacterTransform.rotation.x, rotateY, CharacterTransform.rotation.z);

                OnTurnAfterLookingAround?.Invoke(0);
            }

            base.MoveInDirection(direction);
        }

        #endregion
    }
}