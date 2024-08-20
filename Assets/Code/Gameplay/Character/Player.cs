namespace Gameplay.Character
{
    public class Player : CharacterBase
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        public PlayerControllersModule ControllersModule => new();

        #endregion

        #region UNITY_METHODS

        //Tymczasowe - do zmiany - inicjalizacja przez manager
        private void Start()
        {
            Initialize();
        }

        #endregion

        #region METHODS

        protected override void SetModules()
        {
            base.SetModules();
            modules.Add(ControllersModule);
        }

        #endregion
    }
}