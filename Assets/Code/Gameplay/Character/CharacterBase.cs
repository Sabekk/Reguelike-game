using Gameplay.Character.Body;
using Gameplay.Character.Module;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character
{
    public abstract class CharacterBase : MonoBehaviour, ICameraFollowTarget
    {
        #region VARIABLES

        [SerializeField, FoldoutGroup("Values")] private CharacterValues values;
        [SerializeField, FoldoutGroup("Values")] private MovementValues movementValues;

        [SerializeField] private CharacterData data;
        [SerializeField, HideInInspector] private bool isInitialzied;

        [SerializeField, FoldoutGroup("Components")] private Rigidbody rb;
        [SerializeField, FoldoutGroup("Components")] private CapsuleCollider capsuleCollider;
        [SerializeField, FoldoutGroup("Components")] private Transform cameraFollowTarget;

        [SerializeField, HideInInspector] private CharacterBodyContainer bodyContainer;
        [SerializeField, HideInInspector] protected List<CharacterModule> modules;

        [SerializeField, FoldoutGroup("Modules")] private EquipmentModule equipmentModule;


        #endregion

        #region PROPERTIES

        public CharacterValues Values => values;
        public MovementValues MovementValues => movementValues;
        public CapsuleCollider CapsuleCollider => capsuleCollider;
        public Rigidbody Rb => rb;
        public Transform CameraFollowTarget => cameraFollowTarget;

        public EquipmentModule EquipmentModule => equipmentModule;

        public abstract bool IsMoving { get; }
        public bool AllowToRotate => IsMoving;// + inne warunki typu celowanie aby obracaæ postaæ lecuj¹c¹
        protected bool IsInitialzied => isInitialzied;


        #endregion

        #region CONSTRUCTORS

        public CharacterBase()
        {

        }

        #endregion

        #region UNITY_METHODS

        protected virtual void Update()
        {
            UpdateModules();
        }

        #endregion

        #region METHODS
        public void Initialize()
        {
            values = new();
            data = new();

            values.Initialze();
            SetModules();
            InitializeModules();

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

        protected virtual void SetModules()
        {
            modules = new();
        }

        protected void InitializeModules()
        {
            modules.ForEach(m => m.Initialize(this));
        }

        protected void UpdateModules()
        {
            modules?.ForEach(m => m.OnUpdate());
        }

        protected void CleanUpModules()
        {
            modules.ForEach(m => m.CleanUp());
        }

        #endregion
    }
}
