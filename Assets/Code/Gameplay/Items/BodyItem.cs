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

        public override BodyItemData Data => null;


        #endregion
    }
}