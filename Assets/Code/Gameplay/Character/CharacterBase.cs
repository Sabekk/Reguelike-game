using Gameplay.Character.Animations;
using Gameplay.Character.Controller;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character
{
    public abstract class CharacterBase : MonoBehaviour
    {
        #region VARIABLES

        [SerializeField, FoldoutGroup("Values")] private CharacterValues values;
        [SerializeField, FoldoutGroup("Values")] private MovementValues movementValues;

        [SerializeField] private CharacterData data;
        [SerializeField, HideInInspector] private bool isInitialzied;

        [SerializeField, FoldoutGroup("Components")] private Rigidbody rb;
        [SerializeField, FoldoutGroup("Components")] private CapsuleCollider capsuleCollider;

        #endregion

        #region PROPERTIES

        public CharacterValues Values => values;
        public MovementValues MovementValues => movementValues;
        public CapsuleCollider CapsuleCollider => capsuleCollider;
        public Rigidbody Rb => rb;
        public CharacterMovementController MovementController => new();
        public AnimatorStateController AnimatorStateController => new();

        protected bool IsInitialzied => isInitialzied;

        #endregion

        #region CONSTRUCTORS

        public CharacterBase()
        {

        }

        #endregion

        #region UNITY_METHODS

        private void Update()
        {
            MovementController.OnUpdate();
            AnimatorStateController.OnUpdate();
        }

        #endregion

        #region METHODS
        public void Initialize()
        {
            values = new();
            data = new();

            values.Initialze();
            InitializeControllers();

            isInitialzied = true;
        }

        public void SetData(CharacterData data)
        {
            this.data = data;
        }

        public void SetStartingValues()
        {
            Values.SetStartingValues(new List<StartingValue>(data.StartingValues));
        }

        private void InitializeControllers()
        {
            MovementController.Initialize(this);
            AnimatorStateController.Initialize(this);
        }

        #endregion
    }
}
