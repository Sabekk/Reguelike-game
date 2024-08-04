public class ModifiableValuesDefinitions
{
	#region CATEGORIES

	public const string CharacterValuesCategory = "CharacterValues";
	public const string MovementValuesCategory = "MovementValues";

	#endregion

	#region CATEGORY_ELEMENTS

	public static class CharacterValues
	{
		public const string HP = "HP";
		public const string Mana = "Mana";
		public const string Strengh = "Strengh";
		public const string Agility = "Agility";
		public const string Stamina = "Stamina";
	}

	public static class MovementValues
	{
		public const string WalkSpeed = "WalkSpeed";
		public const string SprintSpeed = "SprintSpeed";
		public const string JumpPower = "JumpPower";
		public const string Gravity = "Gravity";
	}

	#endregion
}
