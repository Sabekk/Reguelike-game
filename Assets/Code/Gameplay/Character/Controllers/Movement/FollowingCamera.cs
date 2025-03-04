using Cinemachine;
using GlobalEventSystem;
using ObjectPooling;
using System;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Gameplay.Character.Camera
{
    [Serializable]
    public class FollowingCamera : MonoBehaviour, IPoolable
    {
        #region VARIABLES

        [SerializeField] private CinemachineVirtualCamera cvCamera;

        [SerializeField, FoldoutGroup("Settings")] private float timeToResetRotation;
        [SerializeField, FoldoutGroup("Settings")] private float rotationSpeed = 30;
        [SerializeField, FoldoutGroup("Settings")] private float topClamp = 70;
        [SerializeField, FoldoutGroup("Settings")] private float bottomClamp = -40;

        private ICameraFollowTarget followingTarget;

        #endregion

        #region PROPERTIES
        public float RotationSpeed => rotationSpeed;
        public float TopClamp => topClamp;
        public float BottomClamp => bottomClamp;
        public PoolObject Poolable { get; set; }
        public Transform Target => followingTarget.CameraFollowTarget;

        #endregion

        #region UNITY_METHODS

        #endregion

        #region METHODS

        public void Initialzie(ICameraFollowTarget followingTarget)
        {
            if (this.followingTarget != null)
                CleanUp();

            this.followingTarget = followingTarget;
            cvCamera.Follow = this.followingTarget.CameraFollowTarget;
            AttachEvents();
        }

        public void CleanUp()
        {
            followingTarget = null;
            DetachEvents();
        }

        public void AssignPoolable(PoolObject poolable)
        {
            Poolable = poolable;
        }

        public void UpdateXRotation(float xRotation)
        {
            Target.rotation = Quaternion.Euler(xRotation, Target.eulerAngles.y, Target.eulerAngles.z);
        }

        public void UpdateYRotation(float yRotation)
        {
            Target.rotation = Quaternion.Euler(Target.eulerAngles.x, yRotation, Target.eulerAngles.z);
        }

        public void UpdateXYRotation(float xRotation, float yRotation)
        {
            Target.rotation = Quaternion.Euler(xRotation, yRotation, Target.eulerAngles.z);
        }
        public void UpdateRotation(Quaternion rotation)
        {
            Target.rotation = rotation;
        }

        private void AttachEvents()
        {

        }

        private void DetachEvents()
        {

        }


        #endregion
    }
}