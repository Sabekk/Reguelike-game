namespace Gameplay.Items
{
    [System.Serializable]
    public class BodyItem : ItemBase
    {
        #region VARIABLES

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

        #endregion
    }
}