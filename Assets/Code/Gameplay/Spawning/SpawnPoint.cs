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

        public TC SpawnAndGetObject<TC>(BiomType biomType) where TC : Component 
        {
            ClearChildren();

            T variant = GetVariantForBiom(biomType);
            if (variant == null)
            {
                Debug.LogWarning(StringBuilderScaler.GetScaledText("Niepoprawnie ustawiony biom dla spawn point! {0}", biomType), this);
                variant = GetVariantForBiom(BiomType.DEFAULT);
            }
            if (variant == null)
                return null;

            PoolObject polledObject = ObjectPool.Instance.GetFromPool(variant.GetRandomVariantName(), variant.GetCategory());
            polledObject.Prefab.transform.SetParent(transform);
            TC spawnedObject = polledObject.GetComponent<TC>();
            return spawnedObject;
        }

        public TC SpawnAndGetObject<TC>() where TC : Component
        {
            return SpawnAndGetObject<TC>(ArenaManager.Instance.CurrentBiom);
        }

        public void SpawnObject()
        {
            SpawnObject(ArenaManager.Instance.CurrentBiom, true);
        }

        public void SpawnObject(BiomType biomType, bool useObjectPool)
        {
            ClearChildren();

            T variant = GetVariantForBiom(biomType);
            if (variant == null)
            {
                Debug.LogWarning(StringBuilderScaler.GetScaledText("Niepoprawnie ustawiony biom dla spawn point! {0}", biomType), this);
                variant = GetVariantForBiom(BiomType.DEFAULT);
            }
            if (variant == null)
                return;

            string instanceName = variant.GetRandomVariantName();
            GameObject spawnedObject = null;

            if (useObjectPool)
                spawnedObject = ObjectPool.Instance.GetFromPool(instanceName, variant.GetCategory()).Prefab;
            else
            {
                PoolCategoryData poolCategoryData = ObjectPoolDatabase.Instance.FindCategoryData(variant?.GetCategory());
                if (poolCategoryData == null)
                    return;

                PoolInstanceData poolInstanceData = poolCategoryData.FindInstanceData(instanceName);
                if (poolInstanceData == null)
                    return;

                spawnedObject = Instantiate(poolInstanceData.PoolObject);
            }

            if (spawnedObject != null)
            {
                spawnedObject.transform.SetParent(transform);
                spawnedObject.transform.localPosition = Vector3.zero;
            }
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

        public abstract string GetCategory();
        public abstract string[] GetVariants();

        public string GetRandomVariantName()
        {
            return GetVariants()[UnityEngine.Random.Range(0, GetVariants().Length)];
        }

        #endregion
    }
}
