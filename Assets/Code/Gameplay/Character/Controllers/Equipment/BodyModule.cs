using Gameplay.Character.Body;
using Gameplay.Character.Controller;
using Gameplay.Character.Module;
using Gameplay.Items;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character.Equipment
{
    /// <summary>
    /// Controller to collect default body elements
    /// </summary>
    [Serializable]
    public class BodyModule : ModuleBase
    {
        #region VARIABLES

        [SerializeField] private BodyType bodyType;
        [SerializeField] private SerializableDictionary<BodyElementType, BodyItem> itemsInUse;

        #endregion


        #region PROPERTIES

        public BodyType BodyType => bodyType;
        public SerializableDictionary<BodyElementType, BodyItem> ItemsInUse => itemsInUse;

        #endregion

        #region CONSTRUCTORS

        #endregion

        #region METHODS

        public override void Initialize(CharacterBase character)
        {
            base.Initialize(character);
            //Character.EquipmentModule.bo
        }

        public override void AttachEvents()
        {
            base.AttachEvents();
            Character.EquipmentController.OnBodyItemEquip += HandleBodyItemEquip;
            Character.EquipmentController.OnBodyItemUnequip += HandleBodyItemUnequip;
        }

        public override void DetachEvents()
        {
            base.DetachEvents();
            Character.EquipmentController.OnBodyItemEquip -= HandleBodyItemEquip;
            Character.EquipmentController.OnBodyItemUnequip -= HandleBodyItemUnequip;
        }

        #region HANDLERS

        private void HandleBodyItemEquip(BodyItem item)
        {
            if (ItemsInUse.TryGetValue(item.BodyItemData.ElementType, out _))
            {
                Debug.LogWarning($"[{GetType().Name}] Body item of type [{item.BodyItemData.ElementType}] is already in use. Check settings");
                return;
            }

            ItemsInUse.Add(item.BodyItemData.ElementType, item);
        }

        private void HandleBodyItemUnequip(BodyItem item)
        {
            if (ItemsInUse.TryGetValue(item.BodyItemData.ElementType, out BodyItem equipedItem))
            {
                if (item.IdEquals(equipedItem.Id))
                    ItemsInUse.Add(item.BodyItemData.ElementType, item);
                else
                {
                    Debug.LogWarning($"[{GetType().Name}] Body item of type [{item.BodyItemData.ElementType}] is different then in use");
                    return;
                }
            }
            else
            {
                Debug.LogWarning($"[{GetType().Name}] Body item of type [{item.BodyItemData.ElementType}] is not in use");
                return;
            }
        }

        #endregion

        #endregion
    }
}