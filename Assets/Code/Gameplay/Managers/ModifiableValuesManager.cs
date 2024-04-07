using GlobalEventSystem;
using System.Collections.Generic;
using UnityEngine;

public class ModifiableValuesManager : MonoSingleton<ModifiableValuesManager>
{
    #region VARIABLES
    [SerializeField, HideInInspector] List<ModifiableValue> timingValues;
    [SerializeField, HideInInspector] List<ModifiableValue> timingValuesToRemove;
    #endregion

    #region PROPERTIES

    #endregion

    #region UNITY_METHODS
    private void Start()
    {
        Events.Gameplay.Time.OnRareTick += HandeTimePassedForValues;
        Events.ModValue.Modifiers.OnAddModifier += HandleAddedModifierToModValue;
        Events.ModValue.Modifiers.OnRemoveModifier += HandleRemovedModifierToModValue;
    }

    private void OnDestroy()
    {
        Events.Gameplay.Time.OnRareTick -= HandeTimePassedForValues;
        Events.ModValue.Modifiers.OnAddModifier -= HandleAddedModifierToModValue;
        Events.ModValue.Modifiers.OnRemoveModifier -= HandleRemovedModifierToModValue;
    }

    #endregion

    #region METHODS

    private void HandeTimePassedForValues()
    {
        foreach (var valueToRemove in timingValuesToRemove)
            timingValues.Remove(valueToRemove);
        timingValuesToRemove.Clear();

        for (int i = timingValues.Count - 1; i >= 0; i--)
            timingValues[i].HandleTimePassed();
    }

    private void HandleAddedModifierToModValue(ModifiableValue modValue, Modifier modifier)
    {
        if (modifier.ModifierDurationType == Modifier.DurationType.TIME)
            timingValues.Add(modValue);
    }

    private void HandleRemovedModifierToModValue(ModifiableValue modValue, Modifier modifier)
    {
        if (modifier.ModifierDurationType == Modifier.DurationType.TIME)
            if (!modValue.HasTimingModifier())
                timingValuesToRemove.Add(modValue);
    }

    #endregion
}
