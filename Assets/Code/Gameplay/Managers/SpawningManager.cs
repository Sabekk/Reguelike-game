using ObjectPooling;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Arena
{
    public abstract class SpawningManager : MonoSingleton<SpawningManager>
    {
        #region VARIABLES

        [SerializeReference] protected List<SpawnPoint> spawnPoints;
        [SerializeField]
        [ValueDropdown(ObjectPoolDatabase.GET_POOL_CATEGORIES_METHOD)]
        private string categoryOfObjects;

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        public virtual void Initialize()
        {

        }

        #endregion

        #region EDITOR_METHODS

        [Button]
        private void ValidateSpawnPoints()
        {
            spawnPoints.Clear();

            List<SpawnPoint> foundedSpawnPoints = new List<SpawnPoint>();
            foundedSpawnPoints.AddRange(FindObjectsByType<SpawnPoint>(FindObjectsSortMode.None));

            foreach (var spawnPoint in foundedSpawnPoints)
                if (spawnPoint.VariantsContainCategory(categoryOfObjects))
                    spawnPoints.Add(spawnPoint);
        }

        #endregion
    }
}
