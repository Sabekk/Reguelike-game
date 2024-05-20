using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StartingValue
{
    #region VARIABLES

    [ValueDropdown(nameof(GetStartingValue))]
    [SerializeField] string valueName;

    #endregion

    #region PROPERTIES

    #endregion

    #region METHODS

    protected abstract List<string> GetStartingValue();

    #endregion
}
