using UnityEngine;
using ModifiableValues;

public class CharacterValues : ModifiableValuesContainer
{
	#region VARIABLES

	private ModifiableValue hp = new();
	private ModifiableValue mana = new();
	private ModifiableValue strengh = new();
	private ModifiableValue agility = new();
	private ModifiableValue stamina = new();

	#endregion

	#region PROPERTIES

	public ModifiableValue HP => hp;
	public ModifiableValue Mana => mana;
	public ModifiableValue Strengh => strengh;
	public ModifiableValue Agility => agility;
	public ModifiableValue Stamina => stamina;

	#endregion

}
