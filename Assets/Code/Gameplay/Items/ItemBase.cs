using ObjectPooling;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Items
{
    [System.Serializable]
    public abstract class ItemBase<T> : IIdEqualable where T : ItemDataBase
    {
        #region VARIABLES

        [SerializeField, ReadOnly] protected int id = Guid.NewGuid().GetHashCode();
        [SerializeField, ReadOnly] protected int dataId;
        [SerializeField] private SerializableDictionary<ItemVisualizationSocketType, ItemElementVisualization> visualizations;

        protected T elementData;

        #endregion

        #region PROPERTIES

        public abstract T Data { get; }
        public int Id => id;
        public SerializableDictionary<ItemVisualizationSocketType, ItemElementVisualization> Visualizations => visualizations;

        #endregion

        #region CONSTRUCTORS

        public ItemBase()
        {

        }

        public ItemBase(T data)
        {
            this.elementData = data;
            dataId = data.Id;
        }

        #endregion

        #region METHODS

        public void CreateVisualization(BodyType bodyType)
        {
            ClearAllVisualizations();
            foreach (var visualizationData in Data.VisualizationIds[bodyType])
            {
                foreach (var poolId in visualizationData.PoolIds)
                {
                    ItemElementVisualization visualization = ObjectPool.Instance.GetFromPool(poolId, visualizationData.Category).GetComponent<ItemElementVisualization>();
                    if (visualization != null)
                        visualizations.Add(visualization.Socket, visualization);
                }
            }
        }

        public void ClearAllVisualizations()
        {
            foreach (var visualization in visualizations)
                ObjectPool.Instance.ReturnToPool(visualization.Value);
            visualizations.Clear();
        }

        public void ClearVisualization(ItemElementVisualization visualization)
        {
            ObjectPool.Instance.ReturnToPool(visualization);
            visualizations.Remove(visualization.Socket);
        }

        //public abstract void Visualize();
        //public abstract void HideVisualization();

        public bool IdEquals(int id)
        {
            return Id == id;
        }

        #endregion
    }
}
