using Gameplay.Items;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StartingBodyItem
{
    #region VARIABLES

    [SerializeField, ValueDropdown(nameof(GetCategoryInstancesNames))] private int bodyElement;

    #endregion

    #region PROPERTIES

    #endregion

    #region METHODS

    public IEnumerable GetCategoryInstancesNames()
    {
        return null;
    }


    #endregion
}
