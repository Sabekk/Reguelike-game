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
        [SerializeField, FoldoutGroup("Values")] float rotationSpeed = 25;

        private bool isSprinting;
        private float baseDrag;

        private Vector2 direction = Vector3.zero;
        private Vector2 lookDirection = Vector3.zero;
        private Vector3 moveDirection = Vector3.zero;

        #endregion

        #region PROPERTIES

        public bool IsMoving => direction != Vector2.zero;
        private bool IsGrounded => Physics.Raycast(Character.transform.position + CapsuleCollider.center, Vector3.down, (CapsuleCollider.height * 0.5f + 0.2f));

        private Rigidbody Rb => Character.Rb;
        private CapsuleCollider CapsuleCollider => Character.CapsuleCollider;
        private Transform CharacterTransform => Character.transform;

        #endregion

        #region METHODS

        public override void Initialize(CharacterBase character)
        {
            base.Initialize(character);
            baseDrag = Rb.drag;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            MoveCharacter();
            RotateCharacter();

            Debug.Log(lookDirection);
        }

        protected virtual void MoveCharacter()
        {
            if (IsMoving)
            {
                moveDirection = CharacterTransform.right * direction.x + CharacterTransform.forward * direction.y;
                Rb.AddForce(moveDirection.normalized * (isSprinting ? runSpeed : walkSpeed), ForceMode.Force);
            }

            if (!IsGrounded)
            {
                Rb.AddForce(Vector3.down * gravity, ForceMode.Force);
                Rb.drag = baseDrag / 2;
            }
            else
                Rb.drag = baseDrag;
        }

        protected virtual void RotateCharacter()
        {
            if (Character.AllowToRotate == false)
                return;

            if (lookDirection != Vector2.zero)
            {
                Quaternion deltaRotation = Quaternion.Euler(0, lookDirection.x * rotationSpeed * Time.fixedDeltaTime, 0);
                Rb.MoveRotation(Rb.rotation * deltaRotation);
            }

        }

        protected virtual void Jump()
        {
            if (!IsGrounded)
                return;

            Rb.AddForce(CharacterTransform.up * jumpPower, ForceMode.Impulse);
        }

        protected virtual void MoveInDirection(Vector2 direction)
        {
            this.direction = direction;
        }

        protected virtual void LookInDirection(Vector2 direction)
        {
            this.lookDirection = direction;
        }

        protected virtual void Sprint(bool isSprinting)
        {
            this.isSprinting = isSprinting;
        }
        #endregion
    }
}