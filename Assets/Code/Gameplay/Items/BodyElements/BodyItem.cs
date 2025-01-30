using Gameplay.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Items
{
    public class BodyItem : ItemBase<BodyItemData>
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES
        public override BodyItemData Data
        {
            get
            {
                //if (elementData == null)
                //    elementData = MainDatabases.Instance.ItemsDatabase.FindItemData(dataId);
                //return elementData;
                return null;
            }
        }

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