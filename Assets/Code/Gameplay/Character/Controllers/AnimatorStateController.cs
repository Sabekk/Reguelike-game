using Gameplay.Character.Controller;
using GlobalEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character.Animations
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorStateController : ControllerBase
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

        public override void Initialize(CharacterBase character)
        {
            base.Initialize(character);
            animator = Character.GetComponent<Animator>();
        }

        protected virtual void MoveInDirection(Vector2 direction)
        {
            SetMovementAnimation(direction.x, direction.y);
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