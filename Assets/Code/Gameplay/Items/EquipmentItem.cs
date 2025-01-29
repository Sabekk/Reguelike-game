using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Gameplay.Items
{
    [System.Serializable]
    public class EquipmentItem : ItemBase<EquipmentItemData>
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        public override EquipmentItemData Data
        {
            get
            {
                if (elementData == null)
                    elementData = MainDatabases.Instance.ItemsDatabase.FindItemData(dataId);
                return elementData;
            }
        }

        #endregion

        #region CONSTRUCTORS

        public EquipmentItem()
        {

        }

        public EquipmentItem(EquipmentItemData data) : base(data)
        {

        }

        #endregion

        #region METHODS

        #endregion
    }
}
