
namespace GlobalEventSystem
{
    public class ModifiableValueEvents
    {
        #region PROPERTIES
        public ModifiersEvents Modifiers { get; private set; } = new ModifiersEvents();

        #endregion

        #region CLASSES

        /// <summary>
        /// Events of modifiers
        /// </summary>
        public class ModifiersEvents
        {
            /// <summary>
            /// Called when modifiable value got modifier
            /// </summary>
            public Events.Event<ModifiableValue, Modifier> OnAddModifier = new Events.Event<ModifiableValue, Modifier>();
            /// <summary>
            /// Called when modifiable value removed modifier
            /// </summary>
            public Events.Event<ModifiableValue, Modifier> OnRemoveModifier = new Events.Event<ModifiableValue, Modifier>();

        }

        #endregion

    }
}