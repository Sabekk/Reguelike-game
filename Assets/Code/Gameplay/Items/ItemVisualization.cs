using ObjectPooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Items
{
    [System.Serializable]
    public class ItemVisualization : MonoBehaviour, IPoolable
    {
        #region VARIABLES

        [SerializeField] private ItemVisualizationSocketType socketType;

        #endregion

        #region PROPERTIES

        public PoolObject Poolable { get; set; }

        #endregion

        #region METHODS


        public void AssignPoolable(PoolObject poolable)
        {
            Poolable = poolable;
        }

        #endregion
    }
}
