using Gameplay.Character.Module;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character.Controller
{
    public abstract class CharacterControllerWithModules : CharacterControllerBase
    {
        #region VARIABLES

        [SerializeField, HideInInspector] protected List<ModuleBase> modules;

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        public override void Initialize(CharacterBase character)
        {
            this.character = character;
            SetModules();

            modules.ForEach(c => c.Initialize(character));
        }

        public override void CleanUp()
        {
            modules.ForEach(c => c.CleanUp());
        }

        public override void OnUpdate()
        {
            modules.ForEach(c => c.OnUpdate());
        }

        public virtual void SetModules()
        {
            modules = new();
        }

        #endregion
    }
}