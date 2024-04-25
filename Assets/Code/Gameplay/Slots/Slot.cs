using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Gameplay.Slot
{
    [Serializable]
    public class Slot : MonoBehaviour
    {
        #region VARIABLES

        [SerializeField] private SlotType slotType;
        [SerializeField] private IPoolable spawnedObject;

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        #endregion

        #region STRUCTS

        [Serializable]
        public struct Variants
        {
            public BiomType biomType;
            public string[] objectsToSpawn;
        }

        #endregion
    }
}
