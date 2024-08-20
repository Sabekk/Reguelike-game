using Gameplay.Character.Animations;
using Gameplay.Character.Controller;

namespace Gameplay.Character
{
    public class PlayerControllersModule : CharacterControllersModule
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        public CharacterMovementController_Player MovementController => new();
        public AnimatorStateController_Player AnimatorStateController => new();

        #endregion

        #region METHODS


        public override void SetControllers()
        {
            base.SetControllers();
            controllers.Add(MovementController);
            controllers.Add(AnimatorStateController);
        }

        #endregion
    }
}
