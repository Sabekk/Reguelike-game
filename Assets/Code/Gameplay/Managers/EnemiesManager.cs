using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Arena
{
    public class EnemiesManager : SpawningManager
    {
        #region VARIABLES

        [SerializeField] private List<Enemy> enemies;

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        public override void Initialize()
        {
            base.Initialize();
            SpawnAllOfEnemies();
        }

        private void SpawnAllOfEnemies()
        {
            foreach (var spawnPoint in spawnPoints)
            {
                enemies.Add(spawnPoint.SpawnAndGetObject<Enemy>());
            }
        }

        #endregion

        #region EDITOR_METHODS

        [Button]
        public void SpawnEnemies()
        {
            SpawnAllOfEnemies();
        }

        #endregion
    }
}
