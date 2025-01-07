using UnityEngine;
using ModifiableValues;

public class ItemValues : ModifiableValuesContainer
{
	#region VARIABLES

	private ModifiableValue attackdamage = new();
	private ModifiableValue fireattack = new();
	private ModifiableValue coldattack = new();
	private ModifiableValue darkattack = new();
	private ModifiableValue lightattack = new();
	private ModifiableValue armor = new();
	private ModifiableValue fireresistance = new();
	private ModifiableValue coldresistance = new();
	private ModifiableValue lightresistance = new();
	private ModifiableValue darkfireresistance = new();
	private ModifiableValue weight = new();
	private ModifiableValue additionalhealth = new();
	private ModifiableValue additionalmana = new();
	private ModifiableValue additionalstrenght = new();
	private ModifiableValue additionalagility = new();
	private ModifiableValue additionalstamina = new();
	private ModifiableValue additionalspeed = new();

	#endregion

	#region PROPERTIES

	public ModifiableValue AttackDamage => attackdamage;
	public ModifiableValue FireAttack => fireattack;
	public ModifiableValue ColdAttack => coldattack;
	public ModifiableValue DarkAttack => darkattack;
	public ModifiableValue LightAttack => lightattack;
	public ModifiableValue Armor => armor;
	public ModifiableValue FireResistance => fireresistance;
	public ModifiableValue ColdResistance => coldresistance;
	public ModifiableValue LightResistance => lightresistance;
	public ModifiableValue DarkFireResistance => darkfireresistance;
	public ModifiableValue Weight => weight;
	public ModifiableValue AdditionalHealth => additionalhealth;
	public ModifiableValue AdditionalMana => additionalmana;
	public ModifiableValue AdditionalStrenght => additionalstrenght;
	public ModifiableValue AdditionalAgility => additionalagility;
	public ModifiableValue AdditionalStamina => additionalstamina;
	public ModifiableValue AdditionalSpeed => additionalspeed;

	#endregion

}
