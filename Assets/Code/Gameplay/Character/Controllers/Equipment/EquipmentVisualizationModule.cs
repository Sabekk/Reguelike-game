using Gameplay.Character.Body;
using Gameplay.Character.Module;
using Gameplay.Items;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character.Equipment
{
    [Serializable]
    public class EquipmentVisualizationModule : ModuleBase
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        public override void AttachEvents()
        {
            base.AttachEvents();
            Character.EquipmentController.OnItemEquip += HandleItemEquip;
            Character.EquipmentController.OnItemUnequip += HandleItemUnequip;

            Character.EquipmentController.OnBodyItemEquip += HandleBodyItemEquip;
            Character.EquipmentController.OnBodyItemUnequip += HandleBodyItemUnequip;
        }

        public override void DetachEvents()
        {
            base.DetachEvents();
            Character.EquipmentController.OnItemEquip -= HandleItemEquip;
            Character.EquipmentController.OnItemUnequip -= HandleItemUnequip;

            Character.EquipmentController.OnBodyItemEquip -= HandleBodyItemEquip;
            Character.EquipmentController.OnBodyItemUnequip -= HandleBodyItemUnequip;
        }

        private void TryToggleIncompatibileVisualizations(ItemBase item, bool state)
        {
            foreach (var visualization in item.Visualizations.Values)
            {
                foreach (var incompatibileSocket in visualization.IncompatibleSockets)
                {
                    if (Character.CharacterInGame.BodySockets.TryGetValue(incompatibileSocket, out BodySocket socket))
                        socket.transform.gameObject.SetActiveOptimize(state);
                }
            }
        }

        private void CreateVisualization(ItemBase item)
        {
            item.CreateVisualization(Character.EquipmentController.BodyModule.BodyType);
            List<ItemElementVisualization> elementVisualizationsTmp = null;

            foreach (var visualization in item.Visualizations)
                if (Character.CharacterInGame.BodySockets.TryGetValue(visualization.Key, out BodySocket bodySocket))
                {
                    if (bodySocket.transform.childCount > 0)
                    {
                        Debug.LogWarning($"Socket contains elements until creating visualizations. All destroyed");
                        bodySocket.transform.DestroyChildren();
                    }

                    visualization.Value.transform.SetParent(bodySocket.transform);
                    visualization.Value.transform.localPosition = Vector3.zero;
                }
                else
                {
                    Debug.LogError($"Missing socket of type {visualization.Key}. Visualization will be removed to pool");
                    if (elementVisualizationsTmp == null)
                        elementVisualizationsTmp = new();
                    elementVisualizationsTmp.Add(visualization.Value); ;
                }

            if (elementVisualizationsTmp != null)
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