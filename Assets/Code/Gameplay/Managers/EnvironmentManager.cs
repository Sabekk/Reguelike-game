using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Arena
{
    public class EnvironmentManager : MonoSingleton<EnvironmentManager>
    {
        #region VARIABLES

        [SerializeField] private List<PrefabSpawnPoint> spawnPoints;
        [BoxGroup("Cheat")]
        [SerializeField] private BiomType biomTypeCheat;

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        #endregion

        #region EDITOR_METHODS

        [Button]
        private void ValidateSpawnPoints()
        {
            spawnPoints.Clear();

            List<PrefabSpawnPoint> foundedSpawnPoints = new List<PrefabSpawnPoint>();
            foundedSpawnPoints.AddRange(FindObjectsByType<PrefabSpawnPoint>(FindObjectsSortMode.None));

            foreach (var spawnPoint in foundedSpawnPoints)
                spawnPoints.Add(spawnPoint);
        }

        [BoxGroup("Cheat")]
        [Button]
        private void SpawnPrefabs()
        {
            foreach (var spawnPoint in spawnPoints)
                spawnPoint.SpawnObject(biomTypeCheat, false);
        }

        [BoxGroup("Cheat")]
        [Button]
        private void ClearSpawnPoints()
        {
            foreach (var spawnPoint in spawnPoints)
            {
                spawnPoint.ClearChildren();
            }
        }

        #endregion
    }
}
