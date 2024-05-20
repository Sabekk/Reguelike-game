using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ModifiableValuesCategory
{
    #region VARIABLES

    [SerializeField] private string categoryName;
    [SerializeField] private List<ModifiableValueData> modifiableValueDatas;

    #endregion

    #region PROPERTIES

    public string CategoryName => categoryName;
    public List<ModifiableValueData> ModifiableValueDatas => modifiableValueDatas;

    #endregion

    #region METHODS

    #endregion
}
