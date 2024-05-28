using ObjectPooling;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Arena
{
    [Serializable]
    public abstract class SpawnPoint<T> : MonoBehaviour where T : SpawnVariant
    {
        #region VARIABLES

        [SerializeField] private List<T> spawnVariants;

        #endregion

        #region PROPERTIES

        public List<T> SpawnVariants => spawnVariants;

        #endregion

        #region METHODS

        public void ClearChildren()
        {
            if (transform.childCount > 0)
                for (int i = transform.childCount - 1; i >= 0; i--)
                    DestroyImmediate(transform.GetChild(i).gameObject);
        }

        protected T GetVariantForBiom(BiomType biomType)
        {
            foreach (var variant in spawnVariants)
            {
                if (variant.BiomType == biomType)
                    return variant;
            }
            return null;
        }

        #endregion

    }

    [Serializable]
    public abstract class SpawnVariant
    {
        #region VARIABLES

        [SerializeField]
        protected BiomType biomType;

        #endregion

        #region PROPERTIES

        public BiomType BiomType => biomType;

        #endregion

        #region METHODS

        public string GetRandomInstanceName(string[] variantName)
        {
            return variantName[UnityEngine.Random.Range(0, variantName.Length)];
        }

        #endregion
    }
}
