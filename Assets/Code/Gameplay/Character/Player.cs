using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Character
{
    public class Player : CharacterBase
    {
        #region VARIABLES

        [SerializeField, FoldoutGroup("Modules")] private PlayerControllersModule controllersModule;

        #endregion

        #region PROPERTIES

        public PlayerControllersModule ControllersModule => controllersModule;
        public override bool IsMoving => ControllersModule.MovementController.IsMoving;

        #endregion

        #region UNITY_METHODS

        #endregion

        #region METHODS

        protected override void SetModules()
        {
            base.SetModules();
            modules.Add(controllersModule = new());
        }

        #endregion
    }
}