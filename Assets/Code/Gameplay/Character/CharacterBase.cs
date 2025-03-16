using Gameplay.Character.Body;
using Gameplay.Character.Data;
using Gameplay.Character.Controller;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.Character.Equipment;
using Gameplay.Character.Movement;
using ObjectPooling;
using System;

namespace Gameplay.Character
{
    public abstract class CharacterBase : ICameraFollowTarget
    {
        #region ACTIONS

        public event Action OnCharacterInGameCreated;

        #endregion

        #region VARIABLES

        [SerializeField, FoldoutGroup("Values")] private CharacterValues values;
        [SerializeField, FoldoutGroup("Values")] private MovementValues movementValues;

        [SerializeField, ReadOnly] private int characterDataId;
        [SerializeField, HideInInspector] private bool isInitialzied;

        [SerializeField] private CharacterInGame characterInGame;
        [SerializeField, FoldoutGroup("Components")] private Transform cameraFollowTarget;

        [SerializeField, FoldoutGroup("Controllers")] protected EquipmentController equipmentController;
        [SerializeField, FoldoutGroup("Controllers")] protected CharacterMovementController movementController;

        [SerializeField, HideInInspector] protected List<CharacterControllerBase> controllers;

        private CharacterData data;

        #endregion

        #region PROPERTIES

        public CharacterInGame CharacterInGame => characterInGame;
        public CharacterValues Values => values;
        public MovementValues MovementValues => movementValues;
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
            CreateControllers();
        }

        #endregion

        #region METHODS

        public void Initialize()
        {
            values = new();
            values.Initialze();

            //InitializeBodyContainer();
            SetControllers();
            InitializeControllers();

            isInitialzied = true;
        }


        public void CleanUp()
        {
            CleanUpControllers();
            if (CharacterInGame)
            {
                CharacterInGame.OnKill -= HandleCharacterKill;
                ObjectPool.Instance.ReturnToPool(CharacterInGame);
            }
        }


        public void OnUpdate()
        {
            if (!isInitialzied)
                return;

            UpdateControllers();
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

        public void AttachEvents()
        {
            AttachControllers();
        }

        public void DetachEvents()
        {
            DetachControllers();
        }


        protected virtual void CreateControllers()
        {
            equipmentController = new EquipmentController();
            movementController = new CharacterMovementController();
        }

        protected virtual void SetControllers()
        {
            controllers = new();
            controllers.Add(equipmentController);
            controllers.Add(movementController);
        }

        protected void AttachControllers()
        {
            controllers.ForEach(c => c.AttachEvents());
        }

        protected void DetachControllers()
        {
            controllers.ForEach(c => c.DetachEvents());
        }

        protected void InitializeControllers()
        {
            controllers.ForEach(c => c.Initialize(this));
        }

        protected void UpdateControllers()
        {
            controllers.ForEach(c => c.OnUpdate());
        }

        protected void CleanUpControllers()
        {
            controllers.ForEach(c => c.CleanUp());
        }


        public bool TryCreateVisualization(Transform parent = null)
        {
            if (Data == null)
                return false;

            if (characterInGame != null)
                return true;

            characterInGame = ObjectPool.Instance.GetFromPool(Data.CharacterInGamePoolId, Data.CharacterCategoryPoolId).GetComponent<CharacterInGame>();
            if (characterInGame != null)
            {
                characterInGame.Initialize(this);
                //characterInGame.OnKill += HandleCharacterKill;
                characterInGame.transform.SetParent(parent);
                OnCharacterInGameCreated?.Invoke();
                return true;
            }
            else
                return false;
        }

        private void InitializeBodyContainer()
        {
            if (characterInGame == null)
            {
                if (Data == null)
                    return;

                characterInGame = ObjectPool.Instance.GetFromPool(Data.StartingBody.BodyContainerId).GetComponent<CharacterInGame>();
                if (characterInGame == null)
                {
                    Debug.LogError($"Wrong settings in character data ID [{Data.Id}]");
                }
            }
        }

        #region HANDLERS

        private void HandleCharacterKill()
        {
            if (CharacterInGame)
                CharacterInGame.OnKill -= HandleCharacterKill;
            Debug.Log("DEATH");

            CharacterManager.Instance.RemoveCharacter(this);
        }

        #endregion

        #endregion
    }
}
