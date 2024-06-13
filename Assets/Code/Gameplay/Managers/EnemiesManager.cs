using Gameplay.Character;
using ObjectPooling;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        private async void SpawnAllOfEnemies()
        {
            for (int i = 0; i < spawnPoints.Count; i++)
            {
                EnemyData enemyData = spawnPoints[i].GetVariantData();
                if (enemyData == null)
                    continue;

                Enemy enemy = await CreateEnemy(enemyData);
                if (enemy)
                    enemy.transform.position = spawnPoints[i].transform.position;
            }
        }

        private async Task<Enemy> CreateEnemy(EnemyData enemyData)
        {
            Enemy enemy = ObjectPool.Instance.GetFromPool(enemyData.ModelPool).GetComponent<Enemy>();
            if (enemy == null)
            {
                Debug.LogError(StringBuilderScaler.GetScaledText("Niepoprawny przypisany model enemy dla {0}", enemyData.Id));
                return null;
            }

            enemy.Initialize();
            enemy.SetData(enemyData);
            enemy.SetStartingValues();

            enemies.Add(enemy);

            await Task.Yield();

            return enemy;
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
