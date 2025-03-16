using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character.Module
{
    public abstract class ModuleBase
    {
        #region VARIABLES

        [SerializeField, HideInInspector] private CharacterBase character;

        #endregion

        #region PROPERTIES

        public virtual bool WaitingForCharacterInGame => false;
        public CharacterBase Character => character;
        public bool IsInitialized { get; set; }

        #endregion

        #region METHODS

        /// <summary>
        /// Initialization when character (in manager) is created
        /// </summary>
        /// <param name="character"></param>
        public virtual void Initialize(CharacterBase character)
        {
            if (WaitingForCharacterInGame == false)
                this.character = character;
        }

        /// <summary>
        /// Initialization when character in game was created
        /// </summary>
        public virtual void LateInitialize()
        {
            if (WaitingForCharacterInGame)
                IsInitialized = true;
        }

        public virtual void CleanUp()
        {
            DetachEvents();
        }

        public virtual void OnUpdate()
        {

        }

        public virtual void AttachEvents()
        {

        }
        public virtual void DetachEvents()
        {

        }

        #endregion
    }
}
