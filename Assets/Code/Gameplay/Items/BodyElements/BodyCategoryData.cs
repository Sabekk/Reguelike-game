using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Items
{
    [System.Serializable]
    public class BodyCategoryData: ItemCategoryData<BodyItemData>
    {
        #region VARIABLES

        [SerializeField] private BodyElementType elementType;

        #endregion

        #region PROPERTIE
        public BodyElementType ElementType => elementType;

        #endregion

        #region METHODS

        //public EquipmentItemData FindItemData(int itemDataId)
        //{
        //    EquipmentItemData data = ItemsData.Find(x => x.IdEquals(itemDataId));
        //    if (data)
        //        return data;

        //    return null;
        //}

        #endregion
    }
}