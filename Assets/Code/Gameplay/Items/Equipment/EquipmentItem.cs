using Sirenix.OdinInspector;

namespace Gameplay.Items
{
    [System.Serializable]
    public class EquipmentItem : ItemBase
    {
        #region VARIABLES

        private EquipmentItemData elementData;

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

        protected override void CatchData()
        {
            if (elementData == null)
                elementData = MainDatabases.Instance.ItemsDatabase.FindItemData(dataId) as EquipmentItemData;
        }


        #endregion
    }
}
