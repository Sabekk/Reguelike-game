using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using ObjectPooling;

namespace Gameplay.Items
{
    public class ItemsManager : GameplayManager<ItemsManager>
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        private ItemsDatabse Database => MainDatabases.Instance.ItemsDatabase;

        #endregion

        #region METHODS

        public ItemData FindItemData(int itemDataId)
        {
            ItemData data = Database.ItemDatas.Find(x => x.Id == itemDataId);
            if (data)
                return data;

            return null;
        }

        #endregion
    }
}