using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using ModifiableValues;

public class ModifiableValuesScriptsGenerator
{
    #region VARIABLES

    private readonly string folderPath;
    private readonly StringBuilder scriptText = new();

    #endregion

    #region PROPERTIES

    #endregion

    #region CONSTRUCTORS

    public ModifiableValuesScriptsGenerator(string path)
    {
        folderPath = path;
    }

    #endregion

    #region METHODS

    public void GenerateScript(ModifiableValuesCategory category)
    {
        scriptText.Clear();
        string fileName = ConvertToPascalCase(category.CategoryName);
        string className = StringBuilderScaler.GetScaledText("{0} : ModifiableValuesContainer", fileName);

        CreateClassUses();
        scriptText.AppendLine();
        CreateClassHeader(className);
        CreateValues(category.ModifiableValueDatas);
        scriptText.AppendLine("}");

        SaveClassToFile(fileName);
    }

    public void GenerateDefinitions(List<ModifiableValuesCategory> categories)
    {
        scriptText.Clear();
        string className = "ModifiableValuesDefinitions";
        CreateClassHeader(className);

        CreateDefinitionCategories(categories);
        CreateDefinitionCategoryElements(categories);

        scriptText.AppendLine("}");

        SaveClassToFile(className);
    }

    private void CreateDefinitionCategories(List<ModifiableValuesCategory> categories)
    {
        scriptText.AppendLine("\t#region CATEGORIES");
        scriptText.AppendLine();
        foreach (var category in categories)
            AddDefinition(ConvertToPascalCase(category.CategoryName), "\t", "Category");

        scriptText.AppendLine();
        scriptText.AppendLine("\t#endregion");
    }
    private void CreateDefinitionCategoryElements(List<ModifiableValuesCategory> categories)
    {
        scriptText.AppendLine();
        scriptText.AppendLine("\t#region CATEGORY_ELEMENTS");

        foreach (var category in categories)
        {
            scriptText.AppendLine();
            scriptText.AppendLine(StringBuilderScaler.GetScaledText("\tpublic static class {0}", category.CategoryName));
            scriptText.AppendLine("\t{");
            foreach (var modifiableData in category.ModifiableValueDatas)
                AddDefinition(modifiableData.Id, "\t\t");
            scriptText.AppendLine("\t}");
        }

        scriptText.AppendLine();
        scriptText.AppendLine("\t#endregion");
    }

    private void AddDefinition(string definitionName, string space, string additionalText = "")
    {
        scriptText.AppendLine(StringBuilderScaler.GetScaledText("{0}public const string {1}{2} = \"{1}\";", space, definitionName, additionalText));
    }

    private void CreateClassUses()
    {
        scriptText.AppendLine("using UnityEngine;");
        scriptText.AppendLine("using ModifiableValues;");
    }

    private void CreateClassHeader(string className)
    {
        scriptText.AppendLine("public class " + className);
        scriptText.AppendLine("{");
    }

    private void CreateValues(List<ModifiableValueData> values)
    {
        scriptText.AppendLine("\t#region VARIABLES");
        scriptText.AppendLine();
        foreach (ModifiableValueData definedValue in values)
            AddVariable(definedValue);

        CloseRegion();

        scriptText.AppendLine("\t#region PROPERTIES");
        scriptText.AppendLine();
        foreach (ModifiableValueData definedValue in values)
            AddProperty(definedValue);

        CloseRegion();
    }

    private void AddVariable(ModifiableValueData valueSettings)
    {
        scriptText.AppendLine("\tprivate " + nameof(ModifiableValue) + " " + ConvertToCamelCase(valueSettings.Id) + " = new();");
    }

    private void AddProperty(ModifiableValueData valueSettings)
    {
        scriptText.AppendLine("\tpublic " + nameof(ModifiableValue) + " " + valueSettings.Id + " => " + ConvertToCamelCase(valueSettings.Id) + ";");
    }

    private void CloseRegion()
    {
        scriptText.AppendLine();
        scriptText.AppendLine("\t#endregion");
        scriptText.AppendLine();
    }

    private void SaveClassToFile(string className)
    {
        string assetPath = Path.Combine(folderPath, className + ".cs");

        Debug.Log("Generating: " + assetPath);

        string filePath = Application.dataPath + assetPath.Substring("Assets".Length);

        File.WriteAllText(filePath, scriptText.ToString(), Encoding.UTF8);
        AssetDatabase.ImportAsset(assetPath);
    }

    private string ConvertToCamelCase(string name)
    {
        List<string> words = name.Split(new[] { '-', '_', ' ' }).ToList();

        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < words.Count; i++)
        {
            if (i == 0)
            {
                stringBuilder.Append(words[i].ToLowerInvariant());
                continue;
            }

            stringBuilder.Append(words[i][0].ToString().ToUpper() + words[i].Substring(1));
        }

        return stringBuilder.ToString();
    }

    private string ConvertToPascalCase(string name)
    {
        List<string> words = name.Split(new[] { '-', '_', ' ' }).ToList();

        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < words.Count; i++)
        {
            stringBuilder.Append(words[i][0].ToString().ToUpper() + words[i].Substring(1));
        }

        return stringBuilder.ToString();
    }

    #endregion
}
