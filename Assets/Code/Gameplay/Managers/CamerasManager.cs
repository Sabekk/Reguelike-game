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
    [SerializeField, FoldoutGroup("Settings"), ValueDropdown(ObjectPoolDatabase.GET_POOL_CATEGORIES_METHOD)] private int cameraPoolCategoryId;
    [SerializeField, FoldoutGroup("Settings"), ValueDropdown(ObjectPoolDatabase.GET_POOL_CAMERAS_METHOD)] private int characterCameraId;

    #endregion

    #region PROPERTIES

    public FollowingCamera PersonCameraInGame
    {
        get
        {
            if (personCameraInGame == null)
                personCameraInGame = TryGetCamera(characterCameraId, cameraPoolCategoryId);
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

    private FollowingCamera TryGetCamera(int cameraId, int categoryId)
    {
        return ObjectPool.Instance.GetFromPool(cameraId, categoryId).GetComponent<FollowingCamera>();
    }

    #endregion
}
