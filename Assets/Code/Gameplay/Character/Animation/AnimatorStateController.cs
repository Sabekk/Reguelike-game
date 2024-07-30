using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character.Animations
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorStateController
    {
        #region VARIABLES

        [SerializeField] private Animator animator;

        #endregion

        #region PROPERTIES

        private int MoveDirectionX => Animator.StringToHash("MoveDirX");
        private int MoveDirectionY => Animator.StringToHash("MoveDirY");

        #endregion

        #region UNITY_METHODS

        #endregion

        #region METHODS

        public void Initialzie(Animator animator)
        {
            this.animator = animator;
        }

        //TODO przyspieszenie?
        public void SetMovementAnimation(float xDir, float yDir)
        {
            animator.SetFloat(MoveDirectionX, xDir);
            animator.SetFloat(MoveDirectionY, yDir);
        }

        #endregion
    }
}