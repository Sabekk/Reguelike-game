using ObjectPooling;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character.Data
{
    [CreateAssetMenu(menuName = "Data/Character/CharacterData", fileName = "CharacterData")]
    [Serializable]
    public class CharacterData : ScriptableObject, IIdEqualable
    {
        #region VARIABLES

        [SerializeField, FoldoutGroup("Data"), ReadOnly] private int id = Guid.NewGuid().GetHashCode();
        [SerializeField, FoldoutGroup("Data")] private string characterName;
        [SerializeField, FoldoutGroup("Starting values")] private List<CharacterStartingValue> startingValues;
        [SerializeField, FoldoutGroup("Starting values")] private List<MovementStartingValue> movementStartingValues;
        [SerializeReference, FoldoutGroup("Starting values")] private StartingBodyData startingBody;
        [SerializeField, FoldoutGroup("Pool"), ValueDropdown(ObjectPoolDatabase.GET_POOL_CATEGORIES_METHOD)] private int characterCategoryPoolId;
        [SerializeField, FoldoutGroup("Pool"), ValueDropdown(ObjectPoolDatabase.GET_POOL_CHARACTER_BASE_METHOD)] private int characterInGamePoolId;

        #endregion

        #region PROPERTIES

        public int Id => id;
        public int CharacterInGamePoolId => characterInGamePoolId;
        public int CharacterCategoryPoolId => characterCategoryPoolId;
        public string CharacterName => characterName;
        public List<CharacterStartingValue> StartingValues => startingValues;
        public List<MovementStartingValue> MovementStartingValues => movementStartingValues;
        public StartingBodyData StartingBody => startingBody;

        #endregion

        #region METHODS

        public bool IdEquals(int id)
        {
            return Id == id;
        }

        #endregion
    }
}
