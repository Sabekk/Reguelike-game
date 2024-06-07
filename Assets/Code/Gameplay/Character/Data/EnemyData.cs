using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

[Serializable]
public class EnemyData : CharacterData
{
    #region VARIABLES

    [ValueDropdown("@ObjectPoolDatabase.Instance.GetCategoryInstancesNames(\"ENEMY\")")]
    [SerializeField] private string modelPool;

    #endregion

    #region PROPERTIES

    public string ModelPool => modelPool;

    #endregion
}
