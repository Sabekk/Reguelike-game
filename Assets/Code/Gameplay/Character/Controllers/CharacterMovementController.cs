using Gameplay.Character.Animations;
using GlobalEventSystem;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character.Controller
{
    public class CharacterMovementController : ControllerBase
    {
        #region VARIABLES

        //TODO baza danych z wartosciami startowymi postaci
        [SerializeField, FoldoutGroup("Values")] float walkSpeed = 4;
        [SerializeField, FoldoutGroup("Values")] float runSpeed = 7;
        [SerializeField, FoldoutGroup("Values")] float jumpPower = 10;
        [SerializeField, FoldoutGroup("Values")] float gravity = 5;


        [SerializeField, FoldoutGroup("Components")] private AnimatorStateController stateController;
        [SerializeField, FoldoutGroup("Components")] private Animator animator;

        private bool isSprinting;
        private float baseDrag;

        private Vector2 direction = Vector3.zero;
        private Vector3 moveDirection = Vector3.zero;
        private Vector3 velocity = Vector3.zero;

        #endregion

        #region PROPERTIES

        private Rigidbody rb => Character.Rb;
        private CapsuleCollider CapsuleCollider => Character.CapsuleCollider;
        private Transform transform => Character.transform;
        private bool IsGrounded => Physics.Raycast(Character.transform.position + CapsuleCollider.center, Vector3.down, (CapsuleCollider.height * 0.5f + 0.2f));

        #endregion

        #region METHODS

        public override void Initialize(CharacterBase character)
        {
            base.Initialize(character);
            baseDrag = rb.drag;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            MovePlayer();
        }

        protected override void AttachEvents()
        {
            base.AttachEvents();
            Events.Gameplay.Move.OnJump += Jump;
            Events.Gameplay.Move.OnMoveInDirection += MoveInDirection;
            Events.Gameplay.Move.OnSprint += Sprint;
        }

        protected override void DetachEvents()
        {
            Events.Gameplay.Move.OnJump -= Jump;
            Events.Gameplay.Move.OnMoveInDirection -= MoveInDirection;
            Events.Gameplay.Move.OnSprint -= Sprint;
        }

        private void MovePlayer()
        {
            if (direction != Vector2.zero)
            {
                moveDirection = transform.right * direction.x + transform.forward * direction.y;
                //rb.AddForce(moveDirection.normalized * (isSprinting ? runSpeed : walkSpeed), ForceMode.Force);
            }

            if (!IsGrounded)
            {
                //rb.AddForce(Vector3.down * gravity, ForceMode.Force);
                rb.drag = baseDrag / 2;
            }
            else
                rb.drag = baseDrag;
        }

        private void Jump()
        {
            if (!IsGrounded)
                return;

            //rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
        }

        private void MoveInDirection(Vector2 direction)
        {
            this.direction = direction;
        }

        private void Sprint(bool isSprinting)
        {
            this.isSprinting = isSprinting;
        }
        #endregion
    }
}