using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/CharacterData", fileName = "CharacterData")]
public class CharacterData : ScriptableObject
{
    #region VARIABLES

    [SerializeField] private int id = Guid.NewGuid().GetHashCode();
    [SerializeField] private string characterName;
    [SerializeField] private List<CharacterStartingValue> startingValues;
    [SerializeField] private List<MovementStartingValue> movementStartingValues;

    #endregion

    #region PROPERTIES

    public int Id => id;
    public string CharacterName => characterName;
    public List<CharacterStartingValue> StartingValues => startingValues;
    public List<MovementStartingValue> MovementStartingValues => movementStartingValues;

    #endregion
}
