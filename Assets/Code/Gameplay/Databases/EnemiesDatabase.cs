using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/EnemiesDatabase", fileName = "EnemiesDatabase")]
public class EnemiesDatabase : ScriptableSingleton<EnemiesDatabase>
{
    #region VARIABLES

    [SerializeField] private List<EnemyBiomCategoryData> enemyBiomData;

    #endregion

    #region PROPERTIES
    public new static EnemiesDatabase Instance => GetInstance("Singletons/EnemiesDatabase");

    public List<EnemyBiomCategoryData> EnemyBiomData => enemyBiomData;

    #endregion

    #region METHODS

    public void AddBiomCategory(EnemyBiomCategoryData category)
    {
        EnemyBiomData.Add(category);
    }

    public void DeleteBiomCategory(EnemyBiomCategoryData category)
    {
        EnemyBiomData.Remove(category);
    }

    #endregion

    #region EDITOR_METHODS

    #endregion
}
