using Gameplay.Character.Body;
using Gameplay.Items;
using ObjectPooling;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character.Controller
{
    [Serializable]
    public class EquipmentVisualizationController : ControllerBase
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        protected override void AttachEvents()
        {
            base.AttachEvents();
            Character.EquipmentModule.OnItemEquip += HandleItemEquip;
            Character.EquipmentModule.OnItemUnequip += HandleItemUnequip;

            Character.EquipmentModule.OnBodyItemEquip += HandleBodyItemEquip;
            Character.EquipmentModule.OnBodyItemUnequip += HandleBodyItemUnequip;
        }

        protected override void DetachEvents()
        {
            base.DetachEvents();
            Character.EquipmentModule.OnItemEquip -= HandleItemEquip;
            Character.EquipmentModule.OnItemUnequip -= HandleItemUnequip;

            Character.EquipmentModule.OnBodyItemEquip -= HandleBodyItemEquip;
            Character.EquipmentModule.OnBodyItemUnequip -= HandleBodyItemUnequip;
        }

        private void TryToggleIncompatibileVisualizations<T>(ItemBase<T> item, bool state) where T : ItemDataBase
        {
            foreach (var visualization in item.Visualizations.Values)
            {
                foreach (var incompatibileSocket in visualization.IncompatibleSockets)
                {
                    if (Character.BodyContainer.BodySockets.TryGetValue(incompatibileSocket, out BodySocket socket))
                        socket.transform.gameObject.SetActiveOptimize(state);
                }
            }
        }

        private void CreateVisualization<T>(ItemBase<T> item) where T : ItemDataBase
        {
            item.CreateVisualization(Character.EquipmentModule.BodyController.BodyType);
            List<ItemElementVisualization> elementVisualizationsTmp = null;

            foreach (var visualization in item.Visualizations)
                if (Character.BodyContainer.BodySockets.TryGetValue(visualization.Key, out BodySocket bodySocket))
                    visualization.Value.transform.SetParent(bodySocket.transform);
                else
                {
                    Debug.LogError($"Missing socket of type {visualization.Key}. Visualization will be removed to pool");
                    if (elementVisualizationsTmp == null)
                        elementVisualizationsTmp = new();
                    elementVisualizationsTmp.Add(visualization.Value); ;
                }

            foreach (var toRemove in elementVisualizationsTmp)
                item.ClearVisualization(toRemove);

            TryToggleIncompatibileVisualizations(item, false);
        }


        #region HANDLERS

        private void HandleItemEquip(EquipmentItem item)
        {
            CreateVisualization(item);
        }

        private void HandleItemUnequip(EquipmentItem item)
        {
            TryToggleIncompatibileVisualizations(item, true);
            item.ClearAllVisualizations();
        }

        private void HandleBodyItemEquip(BodyItem item)
        {
            CreateVisualization(item);
        }

        private void HandleBodyItemUnequip(BodyItem item)
        {
            TryToggleIncompatibileVisualizations(item, true);
            item.ClearAllVisualizations();
        }

        #endregion

        #endregion
    }
}