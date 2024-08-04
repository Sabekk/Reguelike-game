using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/Characters", fileName = "CharactersDatabase")]
public class CharacterDatabase : ScriptableObject
{
    #region VARIABLES

    [SerializeField] private CharacterData playerData;

    #endregion

    #region PROPERTIES

    public CharacterData PlayerData => playerData;

    #endregion

    #region METHODS

    #endregion

}
