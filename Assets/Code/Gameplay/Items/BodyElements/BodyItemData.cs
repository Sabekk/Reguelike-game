using ObjectPooling;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Items
{
    [CreateAssetMenu(menuName = "Body/BodyItemData", fileName = "BodyItemData")]
    public class BodyItemData : ItemData
    {
        #region VARIABLES

        [SerializeField, FoldoutGroup("BaseInfo")] private BodyElementType elementType;


        #endregion

        #region PROPERTIES

        public override ItemType ItemType => ItemType.BODY;
        public BodyElementType ElementType => elementType;

        #endregion

        #region METHODS


        #endregion
    }
}