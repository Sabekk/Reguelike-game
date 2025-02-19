using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Items
{
    [System.Serializable]
    public class EquipmentCategoryData : ItemCategoryData<EquipmentItemData>
    {
        #region VARIABLES

        [SerializeField] private EquipmentItemType elementType;

        #endregion

        #region PROPERTIES

        public EquipmentItemType ElementType => elementType;

        #endregion
        #region CONSTRUCTORS

        public EquipmentCategoryData() { }
        public EquipmentCategoryData(EquipmentItemType elementType)
        {
            this.elementType = elementType;
        }

        #endregion

        #region METHODS

        #endregion
    }
}