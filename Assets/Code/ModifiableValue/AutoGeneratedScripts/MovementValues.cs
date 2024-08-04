using UnityEngine;
using ModifiableValues;

public class MovementValues : ModifiableValuesContainer
{
	#region VARIABLES

	private ModifiableValue walkspeed = new();
	private ModifiableValue sprintspeed = new();
	private ModifiableValue jumppower = new();
	private ModifiableValue gravity = new();

	#endregion

	#region PROPERTIES

	public ModifiableValue WalkSpeed => walkspeed;
	public ModifiableValue SprintSpeed => sprintspeed;
	public ModifiableValue JumpPower => jumppower;
	public ModifiableValue Gravity => gravity;

	#endregion

}
