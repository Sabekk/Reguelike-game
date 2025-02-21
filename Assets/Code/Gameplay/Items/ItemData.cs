using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Items
{
    public abstract class ItemData : ScriptableObject, IIdEqualable
    {
        #region VARIABLES

        [SerializeField, ReadOnly] private int id;
        [SerializeField, FoldoutGroup("BaseInfo")] private string elementName;
        [SerializeField, FoldoutGroup("BaseInfo")] private Sprite icon;
        [SerializeField, FoldoutGroup("Visualization")] private bool isUniversal;
        [SerializeField, FoldoutGroup("Visualization"), ShowIf(nameof(isUniversal))] private ItemVisualizationData[] universalVisualizationIds;
        [SerializeField, FoldoutGroup("Visualization"), HideIf(nameof(isUniversal))] private SerializableDictionary<BodyType, ItemVisualizationData[]> visualizationIds;

        #endregion

        #region PROPERTIES

        public abstract ItemType ItemType { get; }
        public int Id => id;
        public string ElementName => elementName;
        public Sprite Icon => icon;

        #endregion

        #region UNITY_METHODS

        private void Awake()
        {
            id = Guid.NewGuid().GetHashCode();
        }

        #endregion  

        #region METHODS

        public ItemVisualizationData[] GetVisualizations(BodyType bodyType)
        {
            if (isUniversal)
                return universalVisualizationIds;
            else if (visualizationIds.TryGetValue(bodyType, out ItemVisualizationData[] visualizations))
                return visualizations;

            return null;
        }

        public bool IdEquals(int id)
        {
            return Id == id;
        }

        #endregion

        #region STRUCTS

        protected struct VisualizationPerType
        {

        }

        #endregion
    }
}