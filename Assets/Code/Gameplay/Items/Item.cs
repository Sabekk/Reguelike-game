using ObjectPooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Items
{
    public class Item : MonoBehaviour, IPoolable
    {
        #region VARIABLES

        [SerializeField] private ItemVisualization[] visualizations;

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
