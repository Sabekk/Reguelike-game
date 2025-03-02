using ObjectPooling;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character.Data
{
    [CreateAssetMenu(menuName = "Database/EnemiesDatabase", fileName = "EnemiesDatabase")]
    public class EnemiesDatabase : ScriptableObject
    {
        #region VARIABLES

        [SerializeField, ValueDropdown(ObjectPoolDatabase.GET_POOL_CATEGORIES_METHOD)] private int enemyPoolCategoryId;
        [SerializeField] private List<EnemyBiomCategoryData> enemyBiomData;

        #endregion

        #region PROPERTIES

        public int EnemyPoolCategoryId => enemyPoolCategoryId;
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
            foreach (EnemyBiomCategoryData enemyBiomData in MainDatabases.Instance.EnemiesDatabase.EnemyBiomData)
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

        public EnemyData GetEnemyData(BiomType biomType, EnemyType enemyType, int enemyId)
        {
            List<EnemyTypeCategoryData> categoryDatas = GetCategoriesData(biomType);

            foreach (var categoryData in categoryDatas)
            {
                if (categoryData.EnemyType == enemyType)
                {
                    EnemyData data = categoryData.EnemyDatas.Find(x => x.Id == enemyId);
                    if (data == null)
                        Debug.LogError($"Nie znaleziono EnemyData dla BiomType:{biomType} - EnemyType:{enemyType} - Id{enemyId}");
                    return data;
                }
            }
            return null;
        }


        #endregion

        #region EDITOR_METHODS

        public IEnumerable GetCategoriesNamesOfBiom(BiomType biomType)
        {
            ValueDropdownList<EnemyType> values = new();
            foreach (EnemyBiomCategoryData enemyBiomData in MainDatabases.Instance.EnemiesDatabase.EnemyBiomData)
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
            ValueDropdownList<int> values = new();
            List<EnemyData> enemyData = GetEnemyDatas(biomType, enemyType);

            foreach (var enemy in enemyData)
                if (enemy != null)
                    values.Add(enemy.CharacterName, enemy.Id);

            return values;
        }

        #endregion
    }
}