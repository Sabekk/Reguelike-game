using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ModifiableValueData
{
    #region VARIABLES

    [SerializeField] private string valueName;
    [SerializeField] private Sprite icon;
    [SerializeField] private bool canApplyModifiers;
    [SerializeField] private bool hasMinValue;
    [SerializeField] private bool hasMaxValue;
    [SerializeField, ShowIf(nameof(hasMinValue))] private float minValue;
    [SerializeField, ShowIf(nameof(hasMaxValue))] private float maxValue;

    #endregion

    #region PROPERTIES

    public string ValueName => valueName;
    public Sprite Icon => icon;
    public bool CanApplyModifiers => canApplyModifiers;
    public bool HasMinValue => hasMinValue;
    public bool HasMaxValue => hasMaxValue;
    public float MinValue => minValue;
    public float MaxValue => maxValue;

    #endregion

    #region METHODS

    #endregion
}
