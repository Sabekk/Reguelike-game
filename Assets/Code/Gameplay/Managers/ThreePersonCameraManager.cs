using Gameplay.Character.Camera;
using ObjectPooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreePersonCameraManager : GameplayManager<ThreePersonCameraManager>
{
    #region VARIABLES

    [SerializeField] private ThreePersonCamera personCameraPrefab;
    [SerializeField] private ThreePersonCamera personCameraInGame;

    private const string CAMERA_POOL_CATEGORY = "Camera";
    private const string CAMERA_POOL_INSTANCE = "CharacterCamera";
    #endregion

    #region PROPERTIES

    #endregion

    #region UNITY_METHODS

    #endregion

    #region METHODS

    public override void Initialzie()
    {
        base.Initialzie();
        if (personCameraInGame == null)
        {
            personCameraInGame = ObjectPool.Instance.GetFromPool(CAMERA_POOL_INSTANCE, CAMERA_POOL_CATEGORY).GetComponent<ThreePersonCamera>();
        }
        personCameraInGame.Initialzie();
    }

    public override void CleanUp()
    {
        base.CleanUp();

        if (personCameraInGame)
            personCameraInGame.CleanUp();
    }

    #endregion
}
