using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterValues
{
    #region VARIABLES

    [SerializeField] private ModifiableValue hp = new ModifiableValue();
    [SerializeField] private ModifiableValue stamina = new ModifiableValue();
    [SerializeField] private ModifiableValue strenght = new ModifiableValue();
    [SerializeField] private ModifiableValue agility = new ModifiableValue();

    #endregion

    #region PROPERTIES

    public ModifiableValue HP => hp;
    public ModifiableValue Stamina => stamina;
    public ModifiableValue Strenght => strenght;
    public ModifiableValue Agility => agility;

    #endregion
}
