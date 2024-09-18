using GlobalEventSystem;
using ObjectPooling;
using System;
using UnityEngine;

namespace Gameplay.Character.Camera
{
    [Serializable]
    public class ThreePersonCamera : MonoBehaviour, IPoolable
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES
        public PoolObject Poolable { get; set; }

        #endregion

        #region UNITY_METHODS

        #endregion

        #region METHODS

        public void Initialzie()
        {
            AttachEvents();
        }

        public void CleanUp()
        {
            DetachEvents();
        }

        public void AssignPoolable(PoolObject poolable)
        {
            Poolable = poolable;
        }
        private void AttachEvents()
        {
            Events.Gameplay.Move.OnMoveInDirection += MoveInDirection;
        }

        private void DetachEvents()
        {
            Events.Gameplay.Move.OnMoveInDirection -= MoveInDirection;
        }

        private void MoveInDirection(Vector2 direction)
        {
            //this.direction = direction;
        }

        #endregion
    }
}