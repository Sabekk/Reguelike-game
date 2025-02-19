using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Items
{
    [Serializable]
    public class EqupmentItemsContainer : ItemDatabaseContainer<EquipmentCategoryData, EquipmentItemData>
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        public override ItemType ItemsType => ItemType.EQUIPMENT;

        #endregion

        #region METHODS

        #endregion
    }
}