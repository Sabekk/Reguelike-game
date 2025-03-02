using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character.Data
{
    [CreateAssetMenu(menuName = "Database/CharacterDatabase", fileName = "CharactersDatabase")]
    public class CharacterDatabase : ScriptableObject
    {
        #region VARIABLES

        public const string GET_DATA_METHOD = "@" + nameof(CharacterDatabase) + "." + nameof(GetDatasNames) + "()";

        [SerializeReference] private CharacterData defaultData;
        [SerializeField] private List<CharacterData> datas;

        #endregion

        #region PROPERTIES

        public CharacterData DefaultData => defaultData;
        public List<CharacterData> Datas => datas;

        #endregion

        #region METHODS

        public CharacterData GetData(int dataId)
        {
            CharacterData data = datas.Find(x => x.IdEquals(dataId));
            if (data == null)
                data = defaultData;

            return data;
        }

        public static IEnumerable GetDatasNames()
        {
            ValueDropdownList<int> values = new();
            foreach (CharacterData characterData in MainDatabases.Instance.CharacterDatabase.Datas)
                values.Add(characterData.CharacterName, characterData.Id);

            return values;
        }

        #endregion

    }
}