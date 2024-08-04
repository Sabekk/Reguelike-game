using System;

[Serializable]
public class MovementStartingValue : StartingValue
{
    #region VARIABLES

    #endregion

    #region PROPERTIES

    #endregion

    #region METHODS

    protected override string GetCategory()
    {
        return ModifiableValuesDefinitions.MovementValuesCategory;
    }

    #endregion
}
