using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

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
        string className = ConvertToPascalCase(category.CategoryName);

        CreateClassUses();
        scriptText.AppendLine();
        CreateClassHeader(className);
        CreateValues(category.ModifiableValueDatas);
        scriptText.AppendLine("}");

        SaveClassToFile(className);
    }

    private void CreateClassUses()
    {
        scriptText.AppendLine("using UnityEngine;");
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
        scriptText.AppendLine("\t\tprivate " + nameof(ModifiableValue) + " " + ConvertToCamelCase(valueSettings.ValueName) + " = new ();");
    }

    private void AddProperty(ModifiableValueData valueSettings)
    {
        scriptText.AppendLine("\t\tpublic " + nameof(ModifiableValue) + " " + valueSettings.ValueName + " => " + ConvertToCamelCase(valueSettings.ValueName) + ";");
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
