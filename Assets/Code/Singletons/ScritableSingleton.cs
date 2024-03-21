using UnityEngine;

public class ScritableSingleton<T> : ScriptableObject where T : ScritableSingleton<T>
{
    #region VARIABLES

    protected static T _instance;

    #endregion

    #region PROPERTIES

    public static T Instance
    {
        get
        {
            if (_instance == null)
                SetInstane();
            return _instance;
        }
    }

    #endregion

    #region METHODS

    private static void SetInstane()
    {
        _instance = Resources.Load("Singletons/" + typeof(T).ToString(), typeof(T)) as T;
        if (_instance == null)
            Debug.LogError("Called singleton doesn't exist!: " + typeof(T).ToString());
    }

    #endregion
}