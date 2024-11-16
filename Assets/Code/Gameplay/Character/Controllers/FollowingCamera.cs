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

        private void FixedUpdate()
        {

        }

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

        public void UpdatePosition(float xRotation)
        {
            followingTarget.CameraFollowTarget.rotation = Quaternion.Euler(xRotation, followingTarget.CameraFollowTarget.eulerAngles.y, followingTarget.CameraFollowTarget.eulerAngles.z);
        }

        public void UpdatePosition(float xRotation, float yRotation)
        {
            followingTarget.CameraFollowTarget.rotation = Quaternion.Euler(xRotation, yRotation, followingTarget.CameraFollowTarget.eulerAngles.z);
        }

        public void ResetLocal()
        {
            followingTarget.CameraFollowTarget.localRotation = Quaternion.Euler(0, 0, 0);
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