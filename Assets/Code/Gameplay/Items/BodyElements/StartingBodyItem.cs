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

    [SerializeField, ValueDropdown(nameof(GetCategoryInstancesNames)), ShowIf(nameof(setted))] private int bodyElement;

    [SerializeField, HideInInspector] private BodyElementType elementType;
    [SerializeField, HideInInspector] private bool setted;

    #endregion

    #region PROPERTIES

    #endregion

    #region METHODS

    public void TrySetElementType(BodyElementType elementType, bool forcedChange = false)
    {
        if (setted == true)
        {
            if (forcedChange == false)
                return;
            else if (this.elementType != elementType)
                bodyElement = 0;
            else 
                return;
        }

        this.elementType = elementType;
        setted = true;
    }

    public IEnumerable GetCategoryInstancesNames()
    {
        if (setted)
            return MainDatabases.Instance.ItemsDatabase.BodyItems.GetCategoryInstancesNames(elementType);
        else
            return null;
    }


    #endregion
}
