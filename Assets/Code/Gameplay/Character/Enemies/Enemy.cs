using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character
{
    public class Enemy : CharacterBase
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        private void Update()
        {
            if (IsInitialzied)
                Debug.Log(Values.HP.CurrentValue);
        }

        #endregion
    }
}
