using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ModifiableValues
{
    public class ModifiableValuesContainer
    {
        #region VARIABLES

        [SerializeField] private List<ModifiableValue> allValues;

        #endregion

        #region PROPERTIES

        public List<ModifiableValue> AllValues => allValues;

        #endregion

        #region CONSTRUCTORS

        public ModifiableValuesContainer()
        {

        }

        #endregion

        #region METHODS

        public void Initialze()
        {
            foreach (var propertyInfo in typeof(ModifiableValue).GetProperties())
            {
                object propertyObject = propertyInfo.GetValue(this);
                if (propertyObject is ModifiableValue modifiableValue)
                {
                    allValues.Add(modifiableValue);
                    HandleValuePropertyInfo(modifiableValue, propertyInfo);
                }
            }
        }

        public ModifiableValue GetValue(string valueId)
        {
            return allValues.Find(x => x.Id == valueId);
        }

        protected void HandleValuePropertyInfo(ModifiableValue value, PropertyInfo propertyInfo)
        {
            ModifiableValueData modifiableData = ModifiableValuesDatabase.Instance.FindData(propertyInfo.Name, GetType().Name);
            value.InitializeData(modifiableData);
        }

        #endregion
    }
}
