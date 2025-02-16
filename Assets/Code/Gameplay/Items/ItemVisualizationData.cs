using ObjectPooling;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemVisualizationData
{
    #region VARIABLES

    [SerializeField, ValueDropdown(ObjectPoolDatabase.GET_POOL_CATEGORIES_METHOD), OnValueChanged(nameof(HandleCategoryChanged))] private int category;
    [SerializeField, ValueDropdown(nameof(GetCategoryInstancesNames))] private List<int> poolIds;

    #endregion

    #region PROPERTIES

    public int Category => category;
    public List<int> PoolIds => poolIds;

    #endregion

    #region METHODS

    public IEnumerable GetCategoryInstancesNames()
    {
        return ObjectPoolDatabase.GetCategoryInstancesIds(category);
    }

    private void HandleCategoryChanged()
    {
        poolIds.Clear();
    }

    #endregion
}
