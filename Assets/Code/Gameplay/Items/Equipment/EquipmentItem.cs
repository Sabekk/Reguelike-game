using System;

namespace Gameplay.Items
{
    [Serializable]
    public class EquipmentItem : ItemBase
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        public EquipmentItemData ElementData => Data as EquipmentItemData;

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
