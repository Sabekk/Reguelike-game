using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor;
using System;
using ObjectPooling;

namespace Gameplay.Items
{
    [CreateAssetMenu(menuName = "Item/ItemData", fileName = "ItemData")]
    public class EquipmentItemData : ItemDataBase
    {
        #region VARIABLES

        [SerializeField, FoldoutGroup("BaseInfo")] private ItemType itemType;
        [SerializeField, FoldoutGroup("BaseInfo")] private ItemUseType useType;
        [SerializeField, FoldoutGroup("Stats")] private ItemAttributes attributes;
        //TODO dodatkowe efekty

        #endregion

        #region PROPERTIES

        public ItemType ItemType => itemType;
        public ItemUseType UseType => useType;
        public ItemAttributes Attributes => attributes;

        #endregion

        #region METHODS

        #endregion
    }
}