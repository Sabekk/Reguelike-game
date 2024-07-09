using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.AIBehaviour
{
    //Dodaæ sprawdzanie czy moze sie wykonac
    public abstract class AIBehaviour
    {
        #region VARIABLES

        //TODO customowe parametry
        //[SerializeField]private IList<>

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS
        public abstract void OnUpdate();

        public virtual void Initialize()
        {
            SetCustomParameters();
            AttachEvents();
        }

        public virtual void OnFinish()
        {
            
        }

        public void CleanUp()
        {
            DetachEvents();
        }

        protected virtual void AttachEvents() { }
        protected virtual void DetachEvents() { }

        private void SetCustomParameters()
        {

        }

        #endregion
    }
}
