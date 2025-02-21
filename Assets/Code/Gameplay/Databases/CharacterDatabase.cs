using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/CharacterDatabase", fileName = "CharactersDatabase")]
public class CharacterDatabase : ScriptableObject
{
    #region VARIABLES

    [SerializeReference] private CharacterData playerData;

    #endregion

    #region PROPERTIES

    public CharacterData PlayerData => playerData;

    #endregion

    #region METHODS

    #endregion

}
