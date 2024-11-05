using Gameplay.Arena;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoSingleton<MainManager>, IGlobalManager
{
    #region VARIABLES

    private List<IGameplayManager> managers = new();

    #endregion

    #region PROPERTIES

    #endregion

    #region UNITY_METHODS

    //Tymczasowe - przeniesc w przysz³oœci do miejsca startu gry/³adowania sceny gry
    protected override void Awake()
    {
        base.Awake();
        InitializeManagers();
    }

    private void OnDestroy()
    {
        CleanUpManagers();
    }

    #endregion

    #region METHODS

    public void InitializeManagers()
    {
        if (managers == null)
            managers = new();

        managers.Add(ArenaManager.Instance);
        managers.Add(ArenaChooserManager.Instance);
        managers.Add(EnemiesManager.Instance);
        managers.Add(EnvironmentManager.Instance);
        managers.Add(CamerasManager.Instance);


        //Tymczasowe - przeniesc w przyszlosci do miejsca prze³aczania stanu gry (typu: menu, ³adowanie gry)
        for (int i = 0; i < managers.Count; i++)
        {
            managers[i].Initialzie();
        }
    }

    public void CleanUpManagers()
    {
        for (int i = 0; i < managers.Count; i++)
        {
            managers[i].CleanUp();
        }
    }

    #endregion
}
