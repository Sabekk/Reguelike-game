using Gameplay.Character;
using ObjectPooling;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : GameplayManager<CharacterManager>
{
    #region VARIABLES

    [SerializeField, FoldoutGroup("Settings"), ValueDropdown(ObjectPoolDatabase.GET_POOL_CHARACTER_BASE_METHOD)] private int baseBodyForPlayerId;
    [SerializeField] private Player player;

    #endregion

    #region PROPERTIES

    public Player Player => player;

    #endregion

    #region METHODS

    #region EDITOR_METHODS

    [Button]
    private void SpawnPlayer()
    {
        if (player != null)
            return;
    }

    #endregion

    #endregion
}
