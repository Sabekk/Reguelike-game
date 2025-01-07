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
    public class ItemData : ScriptableObject
    {
        #region VARIABLES

        public const string GET_POOL_ITEMS_METHOD = "@GetItemPools()";

        [SerializeField, ReadOnly] private int id = Guid.NewGuid().GetHashCode();
        [SerializeField, FoldoutGroup("BaseInfo")] private string itemName;
        [SerializeField, FoldoutGroup("BaseInfo")] private Sprite icon;
        [SerializeField, FoldoutGroup("BaseInfo")] private ItemType itemType;
        [SerializeField, FoldoutGroup("BaseInfo")] private ItemUseType useType;
        [SerializeField, FoldoutGroup("Visualization"), ValueDropdown(GET_POOL_ITEMS_METHOD)] private List<string> visualizationsIds;
        [SerializeField, FoldoutGroup("Stats")] private ItemAttributes attributes;
        //TODO dodatkowe efekty

        #endregion

        #region PROPERTIES

        public int Id => id;
        public string ItemName => itemName;
        public Sprite Icon => icon;
        public ItemType ItemType => itemType;
        public ItemUseType UseType => useType;
        public List<string> VisualizationsIds => visualizationsIds;
        public ItemAttributes Attributes => attributes;

        #endregion

        #region METHODS

        private IEnumerable GetItemPools()
        {
            return ObjectPoolDatabase.GetCategoryInstancesNames("Item");
        }

        #endregion
    }
}