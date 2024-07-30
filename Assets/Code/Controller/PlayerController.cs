using GlobalEventSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Gameplay.Character.Animations;

namespace Gameplay.Character
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        #region VARIABLES

        [SerializeField, FoldoutGroup("Values")] float walkSpeed;
        [SerializeField, FoldoutGroup("Values")] float runSpeed;
        [SerializeField, FoldoutGroup("Values")] float jumpPower;
        [SerializeField, FoldoutGroup("Values")] float gravity;

        [SerializeField, FoldoutGroup("Components")] private Animator animator;
        [SerializeField, FoldoutGroup("Components")] private Rigidbody rb;
        [SerializeField, FoldoutGroup("Components")] private CapsuleCollider capsuleCollider;
        [SerializeField, FoldoutGroup("Components")] private AnimatorStateController stateController;

        private bool isSprinting;
        private float baseDrag;

        private Vector2 direction = Vector3.zero;
        private Vector3 moveDirection = Vector3.zero;
        private Vector3 velocity = Vector3.zero;

        #endregion

        #region PROPERTIES

        private bool IsGrounded => Physics.Raycast(transform.position + capsuleCollider.center, Vector3.down, (capsuleCollider.height * 0.5f + 0.2f));

        #endregion

        #region UNITY_METHODS

        private void Start()
        {
            stateController = new AnimatorStateController();
            stateController.Initialzie(animator);

            AttachEvents();
            baseDrag = rb.drag;
        }

        private void OnDestroy()
        {
            DetachEvents();
        }

        private void Update()
        {
            MovePlayer();
        }

        #endregion

        #region METHODS

        void MovePlayer()
        {
            if (direction != Vector2.zero)
            {
                moveDirection = transform.right * direction.x + transform.forward * direction.y;
                rb.AddForce(moveDirection.normalized * (isSprinting ? runSpeed : walkSpeed), ForceMode.Force);
            }

            if (!IsGrounded)
            {
                rb.AddForce(Vector3.down * gravity, ForceMode.Force);
                rb.drag = baseDrag/2;
            }
            else
                rb.drag = baseDrag;
        }

        private void AttachEvents()
        {
            Events.Gameplay.Move.OnJump += Jump;
            Events.Gameplay.Move.OnMoveInDirection += MoveInDirection;
            Events.Gameplay.Move.OnSprint += Sprint;
        }

        private void DetachEvents()
        {
            Events.Gameplay.Move.OnJump -= Jump;
            Events.Gameplay.Move.OnMoveInDirection -= MoveInDirection;
            Events.Gameplay.Move.OnSprint -= Sprint;
        }

        private void Jump()
        {
            if (!IsGrounded)
                return;

            rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);

        }

        private void MoveInDirection(Vector2 direction)
        {
            this.direction = direction;
            stateController.SetMovementAnimation(direction.x, direction.y);
        }

        private void Sprint(bool isSprinting)
        {
            this.isSprinting = isSprinting;
        }

        [Button]
        private void ToggleRun()
        {
            bool currentState = animator.GetBool("Run");
            animator.SetBool("Run", !currentState);
        }

        #endregion
    }
}


