using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System;

namespace ModifiableValues
{
    [Serializable]
    public class ModifiableValuesContainer
    {
        #region VARIABLES

        [SerializeField] private List<ModifiableValue> allValues = new List<ModifiableValue>();

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
            allValues.Clear();
            PropertyInfo[] listOfProperties = GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in listOfProperties)
            {
                object propertyObject = propertyInfo.GetValue(this);
                if (propertyObject is ModifiableValue modifiableValue)
                {
                    allValues.Add(modifiableValue);
                    HandleValuePropertyInfo(modifiableValue, propertyInfo);
                }
            }
        }

        public void SetStartingValues(List<StartingValue> startingValues)
        {
            foreach (var startingValue in startingValues)
                SetStartingValue(startingValue);
        }

        public void SetStartingValue(StartingValue startingValue)
        {
            ModifiableValue value = GetValue(startingValue.ValueId);
            if (value == null)
            {
                Debug.LogError(StringBuilderScaler.GetScaledText("Niepoprawnie ustawiony starting value dla {0}, nie odnaleziono wartoœci {1}", GetType().Name, startingValue.ValueId));
                return;
            }

            value.SetBaseValue(startingValue.Value);
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
