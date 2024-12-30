
namespace GlobalEventSystem
{
    public class UIEvents
    {
        #region PROPERTIES

        public Windows Window { get; private set; } = new Windows();

		#endregion

		#region CLASSES

		/// <summary>
		/// Events of timing
		/// </summary>
		public class Windows
		{
			/// <summary>
			/// Called for toggle inventory window
			/// </summary>
			public Events.Event ToggleInventory = new Events.Event();
		}

		#endregion
	}
}