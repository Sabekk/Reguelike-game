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
            MoveCharacter();
        }

        protected virtual void MoveCharacter()
        {
            if (direction != Vector2.zero)
            {
                moveDirection = transform.right * direction.x + transform.forward * direction.y;
                rb.AddForce(moveDirection.normalized * (isSprinting ? runSpeed : walkSpeed), ForceMode.Force);
            }

            if (!IsGrounded)
            {
                rb.AddForce(Vector3.down * gravity, ForceMode.Force);
                rb.drag = baseDrag / 2;
            }
            else
                rb.drag = baseDrag;
        }

        protected virtual void Jump()
        {
            if (!IsGrounded)
                return;

            rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
        }

        protected virtual void MoveInDirection(Vector2 direction)
        {
            this.direction = direction;
        }

        protected virtual void Sprint(bool isSprinting)
        {
            this.isSprinting = isSprinting;
        }
        #endregion
    }
}