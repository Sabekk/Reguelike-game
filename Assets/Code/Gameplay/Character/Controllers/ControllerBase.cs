using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character.Controller
{
    public abstract class ControllerBase
    {
        #region VARIABLES

        [SerializeField] private CharacterBase character;

        #endregion

        #region PROPERTIES

        public CharacterBase Character => character;

        #endregion

        #region METHODS

        public virtual void Initialize(CharacterBase character)
        {
            this.character = character;
            AttachEvents();
        }

        public virtual void CleanUp()
        {
            DetachEvents();
        }

        public virtual void OnUpdate()
        {

        }

        protected virtual void AttachEvents()
        {

        }
        protected virtual void DetachEvents()
        {

        }

        #endregion
    }
}
