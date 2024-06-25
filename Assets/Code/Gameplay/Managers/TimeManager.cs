using GlobalEventSystem;
using UnityEngine;

public class TimeManager : MonoSingleton<TimeManager>
{
    #region VARIABLES


    [SerializeField] private float rareTickTime = 1;
    [SerializeField, HideInInspector] private float currentTime = 0;

    #endregion

    #region PROPERTIES

    #endregion

    #region UNITY_METHODS

    private void Update()
    {
        currentTime += Time.deltaTime;
        Events.Gameplay.Time.OnTick?.Invoke();

        if (currentTime >= rareTickTime)
        {
            Events.Gameplay.Time.OnRareTick?.Invoke();
            currentTime = 0;
        }
    }

    #endregion

    #region METHODS

    #endregion
}
