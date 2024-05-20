using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Editor
{
    [CreateAssetMenu(menuName = "Database/ModifiableValuesDatabase", fileName = "ModifiableValuesDatabase")]
    public class ModifiableValuesDatabase : ScriptableSingleton<ModifiableValuesDatabase>
    {
        #region VARIABLES

        [SerializeField] private List<ModifiableValuesCategory> modifiableValueCategories;

        [Sirenix.OdinInspector.FilePath]
        [SerializeField]
        private string scriptsPath;

        private ModifiableValuesScriptsGenerator scriptsGenerator;

        #endregion

        #region PROPERTIES

        public new static ModifiableValuesDatabase Instance => GetInstance("Singletons/ModifiableValuesDatabase");
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
        }

        #endregion
    }
}