using GlobalEventSystem;
using ModifiableValues;
using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ModifiableValue : IUpdatableValue
{
    #region VARIABLES

    public event Action OnValueChanged;

    [SerializeField] private string id;
    [SerializeField] private float baseValue;
    [SerializeField, HideInInspector] private float currentValue;
    [SerializeField, HideInInspector] private List<Modifier> modifiers;

    private ModifiableValueData data;

    #endregion

    #region PROPERTIES

    public string Id => id;
    public float BaseValue => baseValue;
    public float CurrentValue => currentValue;

    public ModifiableValueData Data
    {
        get
        {
            if (data == null)
                data = MainDatabases.Instance.ModifiableValuesDatabase.FindData(id);
            return data;
        }
    }

    #endregion

    #region CONSTRUCTORS

    public ModifiableValue() { }

    #endregion

    #region METHODS

    public void InitializeData(ModifiableValueData data)
    {
        id = data.Id;
        this.data = data;

        modifiers = new List<Modifier>();
    }

    public void SetBaseValue(float value)
    {
        baseValue = value;
        RecalculateCurrentValue();
    }

    public void AddModifier(Modifier modifier)
    {
        modifiers.Add(modifier);
        Events.ModValue.Modifiers.OnAddModifier?.Invoke(this, modifier);

        RecalculateCurrentValue();
    }

    public void RemoveModifier(Modifier modifier)
    {
        modifiers.Remove(modifier);
        Events.ModValue.Modifiers.OnRemoveModifier?.Invoke(this, modifier);

        RecalculateCurrentValue();
    }

    public void HandleTimePassed()
    {
        for (int i = modifiers.Count - 1; i >= 0; i--)
        {
            if (modifiers[i].ModifierDurationType != Modifier.DurationType.TIME)
                continue;

            if (modifiers[i].HandleTimePassed())
                RemoveModifier(modifiers[i]);
        }
    }

    public bool HasTimingModifier()
    {
        foreach (var modifier in modifiers)
        {
            if (modifier.ModifierDurationType == Modifier.DurationType.TIME)
                return true;
        }
        return false;
    }

    private void RecalculateCurrentValue()
    {
        float oldValue = currentValue;
        currentValue = baseValue;

        foreach (var modifier in modifiers)
        {
            float valueToAdd = 0;
            switch (modifier.ModifierType)
            {
                case Modifier.Type.ADDITIONAL:
                    valueToAdd = modifier.Value;
                    break;
                case Modifier.Type.PERCENTAGE:
                    valueToAdd = baseValue * modifier.Value / 100f;
                    break;
                default:
                    break;
            }

            currentValue += valueToAdd;
        }

        if (currentValue != oldValue)
            OnValueChanged?.Invoke();
    }

    #endregion
}
