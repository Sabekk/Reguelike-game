using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class EnemyTypeCategoryData
{
    #region VARIABLES

    [SerializeField] private EnemyType enemyType;
    [SerializeField] private List<EnemyData> enemyData;

    #endregion

    #region PROPERTIES

    public EnemyType EnemyType => enemyType;
    public List<EnemyData> EnemyDatas => enemyData;

    #endregion

    #region METHODS

    public void AddEnemyData(EnemyData data)
    {
        EnemyDatas.Add(data);
    }

    public void RemoveEnemyData(EnemyData data)
    {
        EnemyDatas.Remove(data);
    }

    #endregion
}
