using Gameplay.Character;
using Gameplay.Character.Data;
using ObjectPooling;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : GameplayManager<CharacterManager>
{
    #region VARIABLES

    [SerializeField, ValueDropdown(CharacterDatabase.GET_DATA_METHOD)] private int defaultPlayerId;
    [SerializeField] private Player player;
    [SerializeField] private List<CharacterBase> characters = new();

    private Dictionary<CharacterBase, bool> charactersTmp = new();

    #endregion

    #region PROPERTIES

    public Player Player => player;

    #endregion

    #region UNITY_METHODS

    private void Update()
    {
        foreach (var characterTmp in charactersTmp)
        {
            if (characterTmp.Value)
                characters.Add(characterTmp.Key);
            else
                characters.Remove(characterTmp.Key);
        }

        charactersTmp.Clear();

        for (int i = 0; i < characters.Count; i++)
            characters[i].OnUpdate();
    }

    #endregion

    #region METHODS


    /// <summary>
    /// Spawn setted character
    /// </summary>
    /// <typeparam name="T">Type of character like player or enemy</typeparam>
    /// <param name="dataId">Id of character data into CharacterDatabase</param>
    /// <returns></returns>
    public T SpawnCharacter<T>(int dataId) where T : CharacterBase, new()
    {
        CharacterData data = MainDatabases.Instance.CharacterDatabase.GetData(dataId);
        return SpawnCharacter<T>(data);
    }

    /// <summary>
    /// Spawn setted character
    /// </summary>
    /// <typeparam name="T">Type of character like player or enemy</typeparam>
    /// <param name="characterPoolId">Id of pooled character</param>
    /// <param name="data">Base data for character</param>
    /// <param name="poolCategoryId">Optional pool category of pooled character id [Optimization]</param>
    /// <returns></returns>
    public T SpawnCharacter<T>(CharacterData data) where T : CharacterBase, new()
    {
        T character = new T();
        //T character = ObjectPool.Instance.GetFromPool(data.CharacterPoolId, data.CharacterCategoryPoolId).GetComponent<T>();

        character.SetData(data);
        character.Initialize();
        character.SetStartingValues();
        if (character.TryCreateVisualization(transform))
            charactersTmp.Add(character, true);

        return character;
    }

    public void RemoveCharacter<T>(T character) where T : CharacterBase
    {
        character.CleanUp();
        character.DetachEvents();
        charactersTmp.Add(character, false);
    }

    public void SpawnPlayer()
    {
        if (player != null)
            return;

        CharacterData data = MainDatabases.Instance.CharacterDatabase.GetData(defaultPlayerId);
        if (data != null)
            player = SpawnCharacter<Player>(data);
    }

    #region EDITOR_METHODS

    [Button]
    private void TrySpawnPlayer()
    {
        SpawnPlayer();
    }

    #endregion

    #endregion
}
