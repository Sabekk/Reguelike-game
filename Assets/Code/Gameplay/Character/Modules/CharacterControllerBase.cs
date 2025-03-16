using Gameplay.Character.Module;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character.Controller
{
    public abstract class CharacterControllerBase
    {
        #region VARIABLES

        [SerializeField, HideInInspector] protected CharacterBase character;
        [SerializeField, HideInInspector] protected List<ModuleBase> modules;

        #endregion

        #region PROPERTIES

        #endregion

        #region CONSTRUCTORS

        public CharacterControllerBase()
        {
            CreateModules();
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Initialization when character (in manager) is created
        /// </summary>
        /// <param name="character"></param>
        public virtual void Initialize(CharacterBase character)
        {
            this.character = character;
            SetModules();

            modules.ForEach(c => c.Initialize(character));
        }

        /// <summary>
        /// Initialization when character in game was created
        /// </summary>
        public virtual void LateInitialize()
        {
            modules.ForEach(c => c.LateInitialize());
        }

        public virtual void CleanUp()
        {
            modules.ForEach(c => c.CleanUp());
        }

        public virtual void OnUpdate()
        {
            foreach (var module in modules)
            {
                if (module.IsInitialized)
                    module.OnUpdate();
            }
        }

        public virtual void SetModules()
        {
            modules = new();
        }

        public virtual void CreateModules()
        {

        }

        public virtual void AttachEvents()
        {
            modules.ForEach(m => m.AttachEvents());
        }

        public virtual void DetachEvents()
        {
            modules.ForEach(m => m.DetachEvents());
        }


        #endregion
    }
}