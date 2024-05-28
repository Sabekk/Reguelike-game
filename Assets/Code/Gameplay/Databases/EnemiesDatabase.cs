using Sirenix.OdinInspector;
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

    public EnemyBiomCategoryData GetBiomCategoryData(BiomType biomType)
    {
        foreach (EnemyBiomCategoryData enemyBiomData in Instance.EnemyBiomData)
        {
            if (enemyBiomData.BiomType == biomType)
                return enemyBiomData;
        }
        return null;
    }

    public List<EnemyTypeCategoryData> GetCategoriesData(BiomType biomType)
    {
        List<EnemyTypeCategoryData> categoryDatas = new List<EnemyTypeCategoryData>();
        EnemyBiomCategoryData biomCategoryData = GetBiomCategoryData(biomType);

        if (biomCategoryData != null)
            categoryDatas.AddRange(biomCategoryData.EnemyTypes);

        return categoryDatas;
    }

    public List<EnemyData> GetEnemyDatas(BiomType biomType, EnemyType enemyType)
    {
        List<EnemyData> enemyData = new List<EnemyData>();
        List<EnemyTypeCategoryData> categoryDatas = GetCategoriesData(biomType);

        foreach (var categoryData in categoryDatas)
        {
            if (categoryData.EnemyType == enemyType)
                enemyData.AddRange(categoryData.EnemyDatas);
        }

        return enemyData;
    }

    #endregion

    #region EDITOR_METHODS

    public IEnumerable GetCategoriesNamesOfBiom(BiomType biomType)
    {
        ValueDropdownList<EnemyType> values = new();
        foreach (EnemyBiomCategoryData enemyBiomData in Instance.EnemyBiomData)
        {
            if (enemyBiomData.BiomType == biomType)
                foreach (var enemyType in enemyBiomData.EnemyTypes)
                {
                    values.Add(enemyType.EnemyType);
                }
        }

        return values;
    }

    public IEnumerable GetEnemyIds(BiomType biomType, EnemyType enemyType)
    {
        ValueDropdownList<string> values = new();
        List<EnemyData> enemyData = GetEnemyDatas(biomType, enemyType);

        foreach (var enemy in enemyData)
            values.Add(enemy.Id);

        return values;
    }

    #endregion
}
