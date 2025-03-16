using Gameplay.Character.Animations;
using Gameplay.Character.Controller;
using System;
using UnityEngine;

namespace Gameplay.Character.Movement
{
    [Serializable]
    public class CharacterMovementController : CharacterControllerBase
    {
        #region VARIABLES

        [SerializeField] protected CharacterMovementModule movementModule;
        [SerializeField] protected AnimatorStateController animatorStateModule;

        #endregion

        #region PROPERTIES

        public CharacterMovementModule MovementModule => movementModule;
        public AnimatorStateController AnimatorStateModule => animatorStateModule;

        #endregion

        #region METHODS


        public override void SetModules()
        {
            base.SetModules();
            modules.Add(MovementModule);
            modules.Add(AnimatorStateModule);
        }

        public override void CreateModules()
        {
            movementModule = new CharacterMovementModule();
            animatorStateModule = new AnimatorStateController();
        }

        #endregion
    }
}
