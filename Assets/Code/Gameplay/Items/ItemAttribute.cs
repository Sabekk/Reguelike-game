using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class ItemAttribute
{
    #region VARIABLES

    [SerializeField, ValueDropdown(ModifiableValuesDatabase.GET_MODIFIABLE_ITEM_VALUES_METHOD)] private string valueId;
    [SerializeField] private bool hasMaxValue;
    [SerializeField, Tooltip("First value of first to get random")] private int minValue1;
    [SerializeField, Tooltip("Second value of first to get random")] private int minValue2;
    [SerializeField, ShowIf("@hasMaxValue==true"), Tooltip("Second value of first to get random")] private int maxValue1;
    [SerializeField, ShowIf("@hasMaxValue==true"), Tooltip("Second value of second to get random")] private int maxValue2;

    #endregion

    #region PROPERTIES

    public string ValueId => valueId;
    public bool HasMaxValue => hasMaxValue;
    public int MinValue1 => minValue1;
    public int MinValue2 => minValue2;
    public int MaxValue1 => maxValue1;
    public int MaxValue2 => maxValue2;

    #endregion

    #region METHODS

    #endregion
}
