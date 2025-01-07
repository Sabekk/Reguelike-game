public class ModifiableValuesDefinitions
{
	#region CATEGORIES

	public const string CharacterValuesCategory = "CharacterValues";
	public const string MovementValuesCategory = "MovementValues";
	public const string ItemValuesCategory = "ItemValues";

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

	public static class ItemValues
	{
		public const string AttackDamage = "AttackDamage";
		public const string FireAttack = "FireAttack";
		public const string ColdAttack = "ColdAttack";
		public const string DarkAttack = "DarkAttack";
		public const string LightAttack = "LightAttack";
		public const string Armor = "Armor";
		public const string FireResistance = "FireResistance";
		public const string ColdResistance = "ColdResistance";
		public const string LightResistance = "LightResistance";
		public const string DarkFireResistance = "DarkFireResistance";
		public const string Weight = "Weight";
		public const string AdditionalHealth = "AdditionalHealth";
		public const string AdditionalMana = "AdditionalMana";
		public const string AdditionalStrenght = "AdditionalStrenght";
		public const string AdditionalAgility = "AdditionalAgility";
		public const string AdditionalStamina = "AdditionalStamina";
		public const string AdditionalSpeed = "AdditionalSpeed";
	}

	#endregion
}
