using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Items
{
    public abstract class ItemDataBase : ScriptableObject, IIdEqualable
    {
        #region VARIABLES

        [SerializeField, ReadOnly] private int id = Guid.NewGuid().GetHashCode();
        [SerializeField, FoldoutGroup("BaseInfo")] private string elementName;
        [SerializeField, FoldoutGroup("BaseInfo")] private Sprite icon;

        #endregion

        #region PROPERTIES

        public int Id => id;
        public string ElementName => elementName;
        public Sprite Icon => icon;

        #endregion

        #region METHODS

        public bool IdEquals(int id)
        {
            return Id == id;
        }

        #endregion
    }
}