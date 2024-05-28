using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Gameplay.Arena
{
    public class EnemySpawnPoint : SpawnPoint<EnemySpawnVariant>
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        #endregion
    }

    [Serializable]
    public class EnemySpawnVariant : SpawnVariant
    {
        #region VARIABLES

        [SerializeField]
        [ValueDropdown("@EnemiesDatabase.Instance.GetCategoriesNamesOfBiom(biomType)")]
        private EnemyType enemyType;
        [SerializeField]
        [ValueDropdown("@EnemiesDatabase.Instance.GetEnemyIds(biomType, enemyType)")]
        private string[] enemyIds;

        #endregion

        #region PROPERTIES

        public EnemyType EnemyType => enemyType;
        public string[] EnemyIds => enemyIds;

        #endregion

        #region METHODS

        #endregion
    }
}