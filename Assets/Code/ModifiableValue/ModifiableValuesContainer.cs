using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModifiableValues
{
    public class ModifiableValuesContainer<T> where T: class
    {
        #region VARIABLES

        [SerializeField]private List<ModifiableValue> allValues;

        #endregion

        #region PROPERTIES

        public List<ModifiableValue> AllValues => allValues;

        #endregion

        #region CONSTRUCTORS

        public ModifiableValuesContainer()
        {
            //typeof(T).GetProperties()
        }

        #endregion

        #region METHODS

        #endregion
    }
}
