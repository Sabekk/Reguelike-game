using Gameplay.Character;
using ObjectPooling;
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

        public EnemyData GetVariantData()
        {
            return GetVariantData(ArenaManager.Instance.CurrentBiom);
        }

        public EnemyData GetVariantData(BiomType biomType)
        {
            ClearChildren();

            EnemySpawnVariant variant = GetVariantForBiom(biomType);
            if (variant == null)
            {
                Debug.LogWarning(StringBuilderScaler.GetScaledText("Niepoprawnie ustawiony biom dla enemy spawn point! {0}", biomType), this);
                variant = GetVariantForBiom(BiomType.DEFAULT);
            }

            if (variant == null)
                return null;

            return MainDatabases.Instance.EnemiesDatabase.GetEnemyData(biomType, variant.EnemyType, variant.GetRandomVariantId());
        }

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
        private int[] enemyIds;

        #endregion

        #region PROPERTIES

        public EnemyType EnemyType => enemyType;
        public int[] EnemyIds => enemyIds;

        #endregion

        #region METHODS

        public override int GetCategoryId()
        {
            return MainDatabases.Instance.EnemiesDatabase.EnemyPoolCategoryId;
        }

        public override int[] GetVariantIds()
        {
            return EnemyIds;
        }

        #endregion
    }
}