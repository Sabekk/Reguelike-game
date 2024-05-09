using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData
{
    #region VARIABLES

    [SerializeField] private string id;
    [SerializeField] private CharacterValues values;

    #endregion

    #region PROPERTIES

    public string Id => id;
    public CharacterValues Values => values;

    #endregion
}
