using Gameplay.Character.Body;
using Gameplay.Character.Controller;
using Gameplay.Items;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character
{
    /// <summary>
    /// Controller to collect default body elements
    /// </summary>
    [Serializable]
    public class BodyController : ControllerBase
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

        protected override void AttachEvents()
        {
            base.AttachEvents();
            Character.EquipmentModule.OnBodyItemEquip += HandleBodyItemEquip;
            Character.EquipmentModule.OnBodyItemUnequip += HandleBodyItemUnequip;
        }

        protected override void DetachEvents()
        {
            base.DetachEvents();
            Character.EquipmentModule.OnBodyItemEquip -= HandleBodyItemEquip;
            Character.EquipmentModule.OnBodyItemUnequip -= HandleBodyItemUnequip;
        }

        #region HANDLERS

        private void HandleBodyItemEquip(BodyItem item)
        {
            if (ItemsInUse.TryGetValue(item.Data.ElementType, out _))
            {
                Debug.LogWarning($"[{GetType().Name}] Body item of type [{item.Data.ElementType}] is already in use. Check settings");
                return;
            }

            ItemsInUse.Add(item.Data.ElementType, item);
        }

        private void HandleBodyItemUnequip(BodyItem item)
        {
            if (ItemsInUse.TryGetValue(item.Data.ElementType, out BodyItem equipedItem))
            {
                if (item.IdEquals(equipedItem.Id))
                    ItemsInUse.Add(item.Data.ElementType, item);
                else
                {
                    Debug.LogWarning($"[{GetType().Name}] Body item of type [{item.Data.ElementType}] is different then in use");
                    return;
                }
            }
            else
            {
                Debug.LogWarning($"[{GetType().Name}] Body item of type [{item.Data.ElementType}] is not in use");
                return;
            }
        }

        #endregion

        #endregion
    }
}