using Gameplay.Character.Camera;
using ObjectPooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class CamerasManager : GameplayManager<CamerasManager>
{
    //Kamera jak narazie jest w controlerze playera. W przypadku potrzeby obs³ugi kamery (wy³aczanie/w³aczanie) obs³ugiwaæ ja bezpoœrednio w elemencie kamery
    #region VARIABLES

    [SerializeField, FoldoutGroup("Character")] private FollowingCamera personCameraInGame;

    private const string CAMERA_POOL_CATEGORY = "CAMERA";
    private const string CAMERA_POOL_INSTANCE = "CharacterCamera";

    #endregion

    #region PROPERTIES

    public FollowingCamera PersonCameraInGame
    {
        get
        {
            if (personCameraInGame == null)
                personCameraInGame = TryGetCamera(CAMERA_POOL_INSTANCE, CAMERA_POOL_CATEGORY);
            return personCameraInGame;
        }
    }

    #endregion

    #region UNITY_METHODS

    #endregion

    #region METHODS

    public override void CleanUp()
    {
        base.CleanUp();

        if (personCameraInGame)
            personCameraInGame.CleanUp();
    }

    private FollowingCamera TryGetCamera(string cameraName, string category)
    {
        return ObjectPool.Instance.GetFromPool(cameraName, category).GetComponent<FollowingCamera>();
    }

    #endregion
}
