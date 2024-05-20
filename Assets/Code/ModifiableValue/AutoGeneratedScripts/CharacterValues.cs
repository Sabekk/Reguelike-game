using UnityEngine;

public class CharacterValues
{
	#region VARIABLES

		private ModifiableValue hp = new ();
		private ModifiableValue mana = new ();
		private ModifiableValue stamina = new ();
		private ModifiableValue strenght = new ();
		private ModifiableValue agility = new ();

	#endregion

	#region PROPERTIES

		public ModifiableValue HP => hp;
		public ModifiableValue Mana => mana;
		public ModifiableValue Stamina => stamina;
		public ModifiableValue Strenght => strenght;
		public ModifiableValue Agility => agility;

	#endregion

}
