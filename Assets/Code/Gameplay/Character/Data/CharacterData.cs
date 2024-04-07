using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/Data", fileName = "characterData")]
public class CharacterData : ScriptableObject
{
    #region VARIABLES

    [SerializeField] private CharacterValues values;

    #endregion

    #region PROPERTIES

    public CharacterValues Values => values;

    #endregion
}
