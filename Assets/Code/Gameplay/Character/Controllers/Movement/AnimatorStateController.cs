using Gameplay.Character.Controller;
using Gameplay.Character.Module;
using System;
using UnityEngine;

namespace Gameplay.Character.Animations
{
    [RequireComponent(typeof(Animator))]

    [Serializable]
    public class AnimatorStateController : ModuleBase
    {
        #region VARIABLES

        [SerializeField] private Animator animator;
        [SerializeField] private float animationBlendingSpeed = 5;

        private float dirX;
        private float dirY;

        private float currentDirX;
        private float currentDirY;

        #endregion

        #region PROPERTIES

        public Animator ChracterAnimator => animator;
        private int MoveDirectionX => Animator.StringToHash("MoveDirX");
        private int MoveDirectionY => Animator.StringToHash("MoveDirY");

        #endregion

        #region UNITY_METHODS

        #endregion

        #region METHODS

        public override void OnUpdate()
        {
            base.OnUpdate();

            currentDirX = Mathf.MoveTowards(currentDirX, dirX, animationBlendingSpeed * Time.deltaTime);
            currentDirY = Mathf.MoveTowards(currentDirY, dirY, animationBlendingSpeed * Time.deltaTime);

            SetMovementAnimation(currentDirX, currentDirY);
        }

        public override void Initialize(CharacterBase character)
        {
            base.Initialize(character);
            if (animator == null)
                animator = Character.GetComponent<Animator>();

            animator.avatar = Character.BodyContainer.AnimatorAvatar;
        }

        protected virtual void MoveInDirection(Vector2 direction)
        {
            dirX = direction.x;
            dirY = direction.y;
        }

        //TODO przyspieszenie - update do podanych wartosci w OnUpdate
        protected virtual void SetMovementAnimation(float xDir, float yDir)
        {
            animator.SetFloat(MoveDirectionX, xDir);
            animator.SetFloat(MoveDirectionY, yDir);
        }

        #endregion
    }
}