using Gameplay.Character;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Arena
{
    public class EnemiesManager : MonoSingleton<EnemiesManager>
    {
        #region VARIABLES

        [SerializeField] private List<EnemySpawnPoint> spawnPoints;
        [SerializeField] private List<Enemy> enemies;

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        public void Initialize()
        {
            SpawnAllOfEnemies();
        }

        private void SpawnAllOfEnemies()
        {
            //foreach (var spawnPoint in spawnPoints)
            //{
            //    enemies.Add(spawnPoint.SpawnAndGetObject<Enemy>());
            //}
        }

        #endregion

        #region EDITOR_METHODS

        [Button]
        private void SpawnEnemies()
        {
            SpawnAllOfEnemies();
        }

        [Button]
        private void ValidateSpawnPoints()
        {
            spawnPoints.Clear();

            List<EnemySpawnPoint> foundedSpawnPoints = new List<EnemySpawnPoint>();
            foundedSpawnPoints.AddRange(FindObjectsByType<EnemySpawnPoint>(FindObjectsSortMode.None));

            foreach (var spawnPoint in foundedSpawnPoints)
                spawnPoints.Add(spawnPoint);
        }

        #endregion
    }
}
