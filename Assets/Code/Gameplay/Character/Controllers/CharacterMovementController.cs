using Gameplay.Character.Animations;
using GlobalEventSystem;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character.Controller
{
    public class CharacterMovementController : ControllerBase
    {
        #region ACTIONS

        #endregion

        #region VARIABLES

        //TODO baza danych z wartosciami startowymi postaci
        [SerializeField, FoldoutGroup("Values")] protected float walkSpeed = 4;
        [SerializeField, FoldoutGroup("Values")] protected float runSpeed = 7;
        [SerializeField, FoldoutGroup("Values")] protected float jumpPower = 10;
        [SerializeField, FoldoutGroup("Values")] protected float gravity = 5;
        [SerializeField, FoldoutGroup("Values")] protected float rotationSpeed = 25;

        protected bool isSprinting;
        protected float baseDrag;

        protected Vector2 direction = Vector3.zero;
        protected Vector2 lookDirection = Vector3.zero;
        protected Vector3 moveDirection = Vector3.zero;

        #endregion

        #region PROPERTIES

        public bool IsMoving => direction != Vector2.zero;
        protected Transform CharacterTransform => Character.transform;
        protected bool IsGrounded => Physics.Raycast(Character.transform.position + CapsuleCollider.center, Vector3.down, (CapsuleCollider.height * 0.5f + 0.2f));
        protected Rigidbody Rb => Character.Rb;
        protected CapsuleCollider CapsuleCollider => Character.CapsuleCollider;

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
            RotateCharacterByLookDirection();
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

        protected virtual void RotateCharacterByLookDirection()
        {
            if (Character.AllowToRotate == false)
                return;

            if (lookDirection != Vector2.zero)
            {
                Quaternion deltaRotation = Quaternion.Euler(0, lookDirection.x * rotationSpeed * Time.fixedDeltaTime, 0);
                RotateCharacter(deltaRotation);
            }

        }

        protected virtual void RotateCharacter(Quaternion rotation)
        {
            Rb.MoveRotation(Rb.rotation * rotation);
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