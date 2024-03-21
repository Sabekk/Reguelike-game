using GlobalEventSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputSystem
{
	public class InputController : MonoBehaviour, InputBinds.IPlayerActions
	{
        #region VARIABLES

        static InputBinds _controll;

        #endregion

        #region PROPERTIES

        public static InputBinds Input
		{
			get
			{
				if (_controll == null)
					_controll = new InputBinds();
				return _controll;
			}
		}

        #endregion

        #region UNITY_METHODS

        private void Awake()
		{
			Input.Player.SetCallbacks(this);
		}

		private void OnEnable()
		{
			Input.Enable();
		}

		private void OnDisable()
		{
			Input.Disable();
		}

        #endregion

        #region METHODS

        public void OnJump(InputAction.CallbackContext context)
		{
			if (context.performed)
				Events.Gameplay.Move.OnJump.Invoke();
		}

		public void OnMovement(InputAction.CallbackContext context)
		{
			if (!context.started)
				Events.Gameplay.Move.OnMoveInDirection.Invoke(context.ReadValue<Vector2>());
		}

		public void OnLookAround(InputAction.CallbackContext context)
		{
			if (!context.started)
				Events.Gameplay.Move.OnLookInDirection.Invoke(context.ReadValue<Vector2>());
		}

		public void OnSprint(InputAction.CallbackContext context)
		{
			if (context.started)
				Events.Gameplay.Move.OnSprint.Invoke(true);

			if (context.canceled)
				Events.Gameplay.Move.OnSprint.Invoke(false);
		}

        #endregion
    }
}

