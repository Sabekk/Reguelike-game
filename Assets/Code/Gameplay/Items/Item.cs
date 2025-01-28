using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Gameplay.Items
{
    [System.Serializable]
    public class Item : EquipmentElementBase<ItemData>
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        public override ItemData Data
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

        public Item()
        {

        }

        public Item(ItemData data) : base(data)
        {

        }

        #endregion

        #region METHODS

        #endregion
    }
}
