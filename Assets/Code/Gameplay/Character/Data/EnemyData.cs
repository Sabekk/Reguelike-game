using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
using ObjectPooling;

[CreateAssetMenu(menuName = "Data/Character/EnemyData", fileName = "EnemyData")]
[Serializable]
public class EnemyData : CharacterData
{
    #region VARIABLES

    [SerializeField, ValueDropdown(ObjectPoolDatabase.GET_POOL_ENEMY_METHOD)] private int modelPoolId;

    #endregion

    #region PROPERTIES

    public int ModelPoolId => modelPoolId;

    #endregion
}
