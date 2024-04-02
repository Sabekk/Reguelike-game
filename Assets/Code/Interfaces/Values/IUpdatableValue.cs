using System;

public interface IUpdatableValue
{
    public event Action OnValueChanged;
    public float CurrentValue { get; }
}
