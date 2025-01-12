using ObjectPooling;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : GameplayManager<CharacterManager>
{
    #region VARIABLES

    [SerializeField, ValueDropdown(ObjectPoolDatabase.GET_POOL_CHARACTER_BASE_METHOD)] string choosenBodyOfPlayer;

    #endregion

    #region PROPERTIES

    #endregion

    #region METHODS

    #endregion
}
