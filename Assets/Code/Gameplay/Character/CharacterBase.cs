using Gameplay.Character.Body;
using Gameplay.Character.Data;
using Gameplay.Character.Controller;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.Character.Equipment;

namespace Gameplay.Character
{
    public abstract class CharacterBase : MonoBehaviour, ICameraFollowTarget
    {
        #region VARIABLES

        [SerializeField, FoldoutGroup("Values")] private CharacterValues values;
        [SerializeField, FoldoutGroup("Values")] private MovementValues movementValues;

        [SerializeField, ReadOnly] private int characterDataId;
        [SerializeField, HideInInspector] private bool isInitialzied;

        [SerializeField] private CharacterBodyContainer bodyContainer;
        [SerializeField, FoldoutGroup("Components")] private Rigidbody rb;
        [SerializeField, FoldoutGroup("Components")] private CapsuleCollider capsuleCollider;
        [SerializeField, FoldoutGroup("Components")] private Transform cameraFollowTarget;

        [SerializeField, FoldoutGroup("Controllers")] private EquipmentController equipmentController;
        [SerializeField, HideInInspector] protected List<CharacterControllerBase> controllers;

        private CharacterData data;

        #endregion

        #region PROPERTIES

        public CharacterBodyContainer BodyContainer => bodyContainer;
        public CharacterValues Values => values;
        public MovementValues MovementValues => movementValues;
        public CapsuleCollider CapsuleCollider => capsuleCollider;
        public Rigidbody Rb => rb;
        public Transform CameraFollowTarget => cameraFollowTarget;
        public EquipmentController EquipmentController => equipmentController;

        public CharacterData Data
        {
            get
            {
                if (data == null)
                    data = MainDatabases.Instance.CharacterDatabase.GetData(characterDataId);
                return data;
            }
        }

        public abstract bool IsMoving { get; }
        public bool AllowToRotate => IsMoving;// + inne warunki typu celowanie aby obracaæ postaæ celuj¹c¹
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
            if (!isInitialzied)
                return;

            UpdateControllers();
        }

        #endregion

        #region METHODS

        public void Initialize()
        {
            values = new();
            values.Initialze();

            InitializeBodyContainer();
            SetControllers();
            InitializeControllers();

            isInitialzied = true;
        }

        public void SetData(CharacterData data)
        {
            this.data = data;
            characterDataId = data.Id;
        }

        public void SetStartingValues()
        {
            if (Data.StartingValues != null)
                Values.SetStartingValues(new List<StartingValue>(Data.StartingValues));
        }

        protected virtual void SetControllers()
        {
            controllers = new();
            controllers.Add(equipmentController);
        }

        protected void InitializeControllers()
        {
            controllers.ForEach(m => m.Initialize(this));
        }

        protected void UpdateControllers()
        {
            controllers?.ForEach(m => m.OnUpdate());
        }

        protected void CleanUpControllers()
        {
            controllers.ForEach(m => m.CleanUp());
        }

        private void InitializeBodyContainer()
        {
            if (bodyContainer == null)
            {
                if (Data == null)
                    return;

                bodyContainer = ObjectPooling.ObjectPool.Instance.GetFromPool(Data.StartingBody.BodyContainerId).GetComponent<CharacterBodyContainer>();
                if (bodyContainer == null)
                {
                    Debug.LogError($"Wrong settings in character data ID [{Data.Id}]");
                }
            }
            bodyContainer.transform.SetParent(transform);
        }

        #endregion
    }
}
