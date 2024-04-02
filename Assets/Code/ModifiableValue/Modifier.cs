using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Modifier
{
    #region VARIABLES

    [SerializeField] private Type type;
    [SerializeField] private DurationType durationType;
    [SerializeField] private float value;
    [SerializeField] private float timeLeft;

    #endregion

    #region CONSTRUCTORS
    public Modifier() { }
    public Modifier(float value, Type type, DurationType durationType, float time = -1)
    {
        this.value = value;
        this.type = type;
        this.durationType = durationType;
        this.timeLeft = time;
    }

    #endregion

    #region PROPERTIES
    public float Value => value;
    public Type ModifierType => type;
    public DurationType ModifierDurationType => durationType;

    #endregion

    #region METHODS

    public bool HandleTimePassed()
    {
        if (durationType != DurationType.TIME)
            return false;

        timeLeft -= Time.deltaTime;

        return timeLeft >= 0;
    }

    #endregion

    #region ENUMS

    public enum Type { ADDITIONAL, PERCENTAGE }
    public enum DurationType { CONSTANT, TIME }

    #endregion
}
