using System;

[Serializable]
public class CharacterStartingValue : StartingValue
{
    #region VARIABLES

    #endregion

    #region PROPERTIES

    #endregion

    #region METHODS

    protected override string GetCategory()
    {
        return ModifiableValuesDefinitions.CharacterValuesCategory;
    }

    #endregion
}
