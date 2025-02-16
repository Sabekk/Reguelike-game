using Gameplay.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character.Body
{
    [System.Serializable]
    public class BodySocket : MonoBehaviour
    {
        #region VARIABLES

        [SerializeField] private ItemVisualizationSocketType socketType;

        #endregion

        #region PROPERTIES

        public ItemVisualizationSocketType SocketType => socketType;

        #endregion

        #region METHODS

        #endregion
    }
}
