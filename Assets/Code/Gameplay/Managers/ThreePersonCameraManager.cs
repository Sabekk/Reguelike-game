using Gameplay.Character.Camera;
using ObjectPooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreePersonCameraManager : GameplayManager<ThreePersonCameraManager>
{
    //Kamera jak narazie jest w controlerze playera. W przypadku potrzeby obs³ugi kamery (wy³aczanie/w³aczanie) obs³ugiwaæ ja bezpoœrednio w elemencie kamery
    #region VARIABLES

    [SerializeField] private ThreePersonCamera personCameraPrefab;
    [SerializeField] private ThreePersonCamera personCameraInGame;

    private const string CAMERA_POOL_CATEGORY = "Camera";
    private const string CAMERA_POOL_INSTANCE = "CharacterCamera";
    #endregion

    #region PROPERTIES

    public ThreePersonCamera PersonCameraInGame
    {
        get
        {
            if (personCameraInGame == null)
                TryInitializeCamera();
            return personCameraInGame;
        }
    }

    #endregion

    #region UNITY_METHODS

    #endregion

    #region METHODS

    public override void Initialzie()
    {
        base.Initialzie();
        if (personCameraInGame == null)
            TryInitializeCamera();

        if (personCameraInGame)
            personCameraInGame.Initialzie();
    }

    public override void CleanUp()
    {
        base.CleanUp();

        if (personCameraInGame)
            personCameraInGame.CleanUp();
    }

    private void TryInitializeCamera()
    {
        personCameraInGame = ObjectPool.Instance.GetFromPool(CAMERA_POOL_INSTANCE, CAMERA_POOL_CATEGORY).GetComponent<ThreePersonCamera>();
    }

    #endregion
}
