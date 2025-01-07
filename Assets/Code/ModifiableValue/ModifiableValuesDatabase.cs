using ModifiableValues;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/ModifiableValuesDatabase", fileName = "ModifiableValuesDatabase")]
public partial class ModifiableValuesDatabase : ScriptableObject
{
    #region VARIABLES

    [SerializeField] private List<ModifiableValuesCategory> modifiableValueCategories;

    [Sirenix.OdinInspector.FilePath]
    [SerializeField]
    private string scriptsPath;

    private ModifiableValuesScriptsGenerator scriptsGenerator;

    #endregion

    #region PROPERTIES

    public List<ModifiableValuesCategory> ModifiableValueCategories => modifiableValueCategories;
    private ModifiableValuesScriptsGenerator ScriptsGenerator
    {
        get
        {
            if (scriptsGenerator == null)
                scriptsGenerator = new ModifiableValuesScriptsGenerator(scriptsPath);
            return scriptsGenerator;
        }
    }

    #endregion

    #region METHODS

    public static ModifiableValuesDatabase GetCurrentDatabase()
    {
        return MainDatabases.Instance.ModifiableValuesDatabase;
    }

    public void AddCategory(ModifiableValuesCategory category)
    {
        modifiableValueCategories.Add(category);
    }

    public void RemoveCategory(ModifiableValuesCategory category)
    {
        modifiableValueCategories.Remove(category);
    }

    public void GenerateScripts()
    {
        foreach (var category in modifiableValueCategories)
            ScriptsGenerator.GenerateScript(category);

        ScriptsGenerator.GenerateDefinitions(modifiableValueCategories);
    }

    public ModifiableValueData FindData(string dataId, string categoryName)
    {
        foreach (var category in modifiableValueCategories)
        {
            if (category.CategoryName != categoryName)
                continue;

            foreach (var data in category.ModifiableValueDatas)
            {
                if (data.Id == dataId)
                    return data;
            }
        }

        return null;
    }

    public ModifiableValueData FindData(string dataId)
    {
        foreach (var category in modifiableValueCategories)
        {
            foreach (var data in category.ModifiableValueDatas)
            {
                if (data.Id == dataId)
                    return data;
            }
        }

        return null;
    }

    public List<string> GetAllModifiableNamesFromCategory(string categoryName)
    {
        List<string> listOfNames = new List<string>();

        foreach (var category in modifiableValueCategories)
            if (category.CategoryName == categoryName)
                foreach (var modifiableValueData in category.ModifiableValueDatas)
                    listOfNames.Add(modifiableValueData.Id);

        return listOfNames;
    }

    #endregion
}
