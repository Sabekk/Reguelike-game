namespace Gameplay.Items
{
    [System.Serializable]
    public class BodyItem : ItemBase
    {
        #region VARIABLES

        private BodyItemData bodyData;

        #endregion

        #region PROPERTIES

        public BodyItemData BodyItemData => Data as BodyItemData;

        #endregion


        #region CONSTRUCTORS

        public BodyItem()
        {

        }

        public BodyItem(BodyItemData data) : base(data)
        {

        }

        #endregion


        #region METHODS

        protected override void CatchData()
        {
            if (bodyData == null)
                bodyData = MainDatabases.Instance.ItemsDatabase.FindItemData(dataId) as BodyItemData;
        }

        #endregion
    }
}