using Gameplay.Character.Controller;
using Gameplay.Character.Module;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character
{
    public abstract class CharacterControllersModule : CharacterModule
    {
        #region VARIABLES

        [SerializeField, HideInInspector] protected List<ControllerBase> controllers;

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        public override void Initialize(CharacterBase character)
        {
            this.character = character;
            SetControllers();

            controllers.ForEach(c => c.Initialize(character));
        }

        public override void CleanUp()
        {
            controllers.ForEach(c => c.CleanUp());
        }

        public override void OnUpdate()
        {
            controllers.ForEach(c => c.OnUpdate());
        }

        public virtual void SetControllers()
        {
            controllers = new();
        }

        #endregion
    }
}