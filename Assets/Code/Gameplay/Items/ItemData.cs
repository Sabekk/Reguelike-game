using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor;

namespace Gameplay.Items
{

    [CreateAssetMenu(menuName = "Item/ItemData", fileName = "ItemData")]
    public class ItemData : ScriptableObject
    {
        #region VARIABLES

        [SerializeField, ReadOnly] private string id = GUID.Generate().ToString();
        [SerializeField, FoldoutGroup("BaseInfo")] private string itemName;
        [SerializeField, FoldoutGroup("BaseInfo")] private Sprite icon;
        [SerializeField, FoldoutGroup("BaseInfo")] private ItemType itemType;
        [SerializeField, FoldoutGroup("BaseInfo")] private ItemUseType useType;
        [SerializeField, FoldoutGroup("Visualization")] private List<ItemVisualization> visualizations;
        [SerializeField, FoldoutGroup("Stats")] private ItemAttributes attributes;
        //TODO dodatkowe efekty

        #endregion

        #region PROPERTIES

        public string Id => id;
        public string ItemName => itemName;
        public Sprite Icon => icon;
        public ItemType ItemType => itemType;
        public ItemUseType UseType => useType;
        public List<ItemVisualization> Visualizations => visualizations;
        public ItemAttributes Attributes => attributes;

        #endregion

        #region METHODS

        #endregion
    }
}