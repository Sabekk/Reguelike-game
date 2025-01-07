using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ModifiableValuesDatabase
{
    #region VARIABLES

    public const string GET_MODIFIABLE_ITEM_VALUES_METHOD = "@" + nameof(ModifiableValuesDatabase) + "." + nameof(GetAllItemValuesToSelect) + "()";

    #endregion

    #region PROPERTIES

    #endregion

    #region METHODS

    public static List<string> GetAllValuesFromCategory(string categoryName, bool onlyModifiable = false)
    {
        List<string> dropdownSet = new();
        ModifiableValuesDatabase currentDatabase = GetCurrentDatabase();
        foreach (var category in currentDatabase.ModifiableValueCategories)
        {
            if (category.CategoryName != categoryName)
                continue;

            foreach (var data in category.ModifiableValueDatas)
            {
                if (onlyModifiable && data.CanApplyModifiers == false)
                    continue;

                dropdownSet.Add(data.Id);
            }
        }
        return dropdownSet;
    }

    public static List<string> GetAllItemValuesToSelect()
    {
        return GetAllValuesFromCategory(ModifiableValuesDefinitions.ItemValuesCategory, true);
    }


    #endregion
}
