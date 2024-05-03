using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Arena
{
    public class EnvironmentManager : SpawningManager
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        #endregion

        #region EDITOR_METHODS
        [Button]
        public void SpawnPrefabs()
        {
            foreach (var spawnPoint in spawnPoints)
            {
                spawnPoint.SpawnObject();
            }
        }
        #endregion
    }
}
