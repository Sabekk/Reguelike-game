using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Arena
{
    public class EnvironmentManager : SpawningManager
    {
        #region VARIABLES

        [BoxGroup("Cheat")]
        [SerializeField] private BiomType biomTypeCheat;

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        #endregion

        #region EDITOR_METHODS

        [BoxGroup("Cheat")]
        [Button]
        private void SpawnPrefabs()
        {
            foreach (var spawnPoint in spawnPoints)
            {
                spawnPoint.SpawnObject(biomTypeCheat, false);
            }
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
