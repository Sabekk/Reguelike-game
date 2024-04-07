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

        public virtual void Initialize(CharacterData data)
        {
            SetDataValues(data);

        }

        public virtual void SetDataValues(CharacterData data)
        {
            values.HP.SetBaseValue(data.Values.HP.BaseValue);
            values.Stamina.SetBaseValue(data.Values.Stamina.BaseValue);
            values.Strenght.SetBaseValue(data.Values.Strenght.BaseValue);
            values.Agility.SetBaseValue(data.Values.Agility.BaseValue);
        }

        #endregion
    }
}
