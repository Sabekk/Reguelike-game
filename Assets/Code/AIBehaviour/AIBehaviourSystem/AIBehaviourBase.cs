using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Gameplay.AIBehaviours
{
    [Serializable]
    public abstract class AIBehaviourBase
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        public virtual void Initalize() { }
        public virtual void Tick() { }
        public virtual void RareTick() { }
        public virtual void OnFinish() { }
        public virtual bool IsFinished() { return true; }

        #endregion
    }
}
