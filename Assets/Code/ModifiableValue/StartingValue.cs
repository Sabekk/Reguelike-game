using ModifiableValues;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public abstract class StartingValue
{
    #region VARIABLES

    [ValueDropdown(nameof(GetStartingValue))]
    [SerializeField] string valueName;
    [SerializeField] float value;

    #endregion

    #region PROPERTIES

    #endregion

    #region METHODS

    protected List<string> GetStartingValue()
    {
        return ModifiableValuesDatabase.Instance.GetAllModifiableNamesFromCategory(GetCategory());
    }

    protected abstract string GetCategory();


    #endregion
}
