using Gameplay.Character.Data;
using Gameplay.Items;
using ObjectPooling;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/MainDatabases", fileName = "MainDatabases")]
public class MainDatabases : ScriptableSingleton<MainDatabases>
{
    #region VARIABLES

    [SerializeField] private ModifiableValuesDatabase modifiableValuesDatabase;
    [SerializeField] private ObjectPoolDatabase objectPoolDatabase;
    [SerializeField] private EnemiesDatabase enemiesDatabase;
    [SerializeField] private ValuesDatabase valuesDatabase;
    [SerializeField] private CharacterDatabase characterDatabase;
    [SerializeField] private ItemsDatabase itemsDatabase;

    #endregion

    #region PROPERTIES

    public new static MainDatabases Instance => GetInstance("Singletons/MainDatabases");

    public ModifiableValuesDatabase ModifiableValuesDatabase => modifiableValuesDatabase;
    public ObjectPoolDatabase ObjectPoolDatabase => objectPoolDatabase;
    public EnemiesDatabase EnemiesDatabase => enemiesDatabase;
    public ValuesDatabase ValuesDatabase => valuesDatabase;
    public CharacterDatabase CharacterDatabase => characterDatabase;
    public ItemsDatabase ItemsDatabase => itemsDatabase;

    #endregion

    #region METHODS

    #endregion
}
