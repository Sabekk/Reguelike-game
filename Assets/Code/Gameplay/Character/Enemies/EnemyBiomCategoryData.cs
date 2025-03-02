using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Gameplay.Character.Data
{
    [Serializable]
    public class EnemyBiomCategoryData
    {
        #region VARIABLES

        [SerializeField] private BiomType biomType;
        [SerializeField] private List<EnemyTypeCategoryData> enemyTypes;

        #endregion

        #region PROPERTIES

        public BiomType BiomType => biomType;
        public List<EnemyTypeCategoryData> EnemyTypes => enemyTypes;

        #endregion

        #region METHODS

        public void AddEnemyTypeData(EnemyTypeCategoryData data)
        {
            EnemyTypes.Add(data);
        }

        public void RemoveEnemyTypeData(EnemyTypeCategoryData data)
        {
            EnemyTypes.Remove(data);
        }

        #endregion
    }
}
