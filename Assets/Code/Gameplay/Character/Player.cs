using Gameplay.Character.Movement;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Character
{
    public class Player : CharacterBase
    {
        #region VARIABLES

        [SerializeField, FoldoutGroup("Controllers")] private PlayerMovementController playerMovementController;

        #endregion

        #region PROPERTIES

        public PlayerMovementController PlayerMovementController => playerMovementController;
        public override bool IsMoving => PlayerMovementController.MovementModule.IsMoving;

        #endregion

        #region UNITY_METHODS

        #endregion

        #region METHODS

        protected override void CreateControllers()
        {
            base.CreateControllers();
            movementController = new PlayerMovementController();
        }

        #endregion
    }
}