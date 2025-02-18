using ObjectPooling;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Gameplay.Items
{
    public class ItemBase : IIdEqualable
    {
        #region VARIABLES

        [SerializeField, ReadOnly] protected int id = Guid.NewGuid().GetHashCode();
        [SerializeField, ReadOnly] protected int dataId;
        [SerializeField] private SerializableDictionary<ItemVisualizationSocketType, ItemElementVisualization> visualizations;

        private ItemData elementData;

        #endregion

        #region PROPERTIES

        public ItemData Data
        {
            get
            {
                CatchData();
                return elementData;
            }
        }

        public int Id => id;
        public ItemType ItemType => Data.ItemType;
        public SerializableDictionary<ItemVisualizationSocketType, ItemElementVisualization> Visualizations
        {
            get
            {
                if (visualizations == null)
                    visualizations = new();
                return visualizations;
            }
        }

        #endregion

        #region CONSTRUCTORS

        public ItemBase()
        {

        }

        public ItemBase(ItemData data)
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
                        Visualizations.Add(visualization.Socket, visualization);
                }
            }
        }

        public void ClearAllVisualizations()
        {
            foreach (var visualization in Visualizations)
                ObjectPool.Instance.ReturnToPool(visualization.Value);
            Visualizations.Clear();
        }

        public void ClearVisualization(ItemElementVisualization visualization)
        {
            ObjectPool.Instance.ReturnToPool(visualization);
            Visualizations.Remove(visualization.Socket);
        }

        //public abstract void Visualize();
        //public abstract void HideVisualization();

        public bool IdEquals(int id)
        {
            return Id == id;
        }

        protected virtual void CatchData()
        {
            if (elementData == null)
                elementData = MainDatabases.Instance.ItemsDatabase.FindItemData(dataId);
        }

        #endregion
    }
}
