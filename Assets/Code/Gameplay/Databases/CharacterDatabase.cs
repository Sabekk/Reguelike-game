using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/Characters", fileName = "CharactersDatabase")]
public class CharacterDatabase : ScritableSingleton<CharacterDatabase>
{
    [SerializeField] private CharacterData playerData;

    public CharacterData PlayerData => playerData;
}
