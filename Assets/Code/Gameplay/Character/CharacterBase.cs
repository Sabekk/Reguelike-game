using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character
{
    public abstract class CharacterBase : MonoBehaviour
    {
        #region VARIABLES

        [SerializeField] private CharacterValues values;
        [SerializeField] private CharacterData data;

        #endregion

        #region PROPERTIES

        public CharacterValues Values => values;

        #endregion

        #region CONSTRUCTORS

        public CharacterBase()
        {

        }

        #endregion

        #region METHODS
        public void Initialize()
        {
            values = new();
            data = new();

            values.Initialze();
        }

        public void SetData(CharacterData data)
        {
            this.data = data;
        }

        public void SetStartingValues()
        {
            Values.SetStartingValues(new List<StartingValue>(data.StartingValues));
        }

        #endregion
    }
}
