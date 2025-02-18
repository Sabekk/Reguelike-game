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

        [SerializeField, ReadOnly] private int id = Guid.NewGuid().GetHashCode();
        [SerializeField, FoldoutGroup("BaseInfo")] private string elementName;
        [SerializeField, FoldoutGroup("BaseInfo")] private Sprite icon;
        [SerializeField, FoldoutGroup("Visualization")] private SerializableDictionary<BodyType, ItemVisualizationData[]> visualizationIds;

        #endregion

        #region PROPERTIES

        public abstract ItemType ItemType { get; }
        public int Id => id;
        public string ElementName => elementName;
        public Sprite Icon => icon;
        public SerializableDictionary<BodyType, ItemVisualizationData[]> VisualizationIds => visualizationIds;

        #endregion

        #region METHODS

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