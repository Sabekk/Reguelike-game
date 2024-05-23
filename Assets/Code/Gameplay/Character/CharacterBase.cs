using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character
{
    public abstract class CharacterBase : MonoBehaviour
    {
        #region VARIABLES

        [SerializeField] private CharacterValues values;

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

        #endregion
    }
}
