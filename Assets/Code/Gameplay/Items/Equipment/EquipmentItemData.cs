using UnityEngine;
using Sirenix.OdinInspector;

namespace Gameplay.Items
{
    [CreateAssetMenu(menuName = "Data/Item/EquipmentItemData", fileName = "EquipmentItemData")]
    public class EquipmentItemData : ItemData
    {
        #region VARIABLES

        [SerializeField, FoldoutGroup("BaseInfo")] private EquipmentItemType equipmentItemType;
        [SerializeField, FoldoutGroup("BaseInfo")] private ItemUseType useType;
        [SerializeField, FoldoutGroup("Stats")] private ItemAttributes attributes;
        //TODO dodatkowe efekty

        #endregion

        #region PROPERTIES

        public override ItemType ItemType => ItemType.EQUIPMENT;
        public EquipmentItemType EquipmentItemType => equipmentItemType;
        public ItemUseType UseType => useType;
        public ItemAttributes Attributes => attributes;


        #endregion

        #region METHODS

        #endregion
    }
}