using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData
{
    #region VARIABLES

    [SerializeField] private string id;
    [SerializeField] private List<CharacterStartingValue> startingValues;

    #endregion

    #region PROPERTIES

    public string Id => id;
    public List<CharacterStartingValue> StartingValues => startingValues;

    #endregion
}
