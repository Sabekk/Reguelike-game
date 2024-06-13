using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Gameplay.AIBehaviours
{
    [Serializable]
    public class AIBehaviour : AIBehaviourBase
    {
        #region VARIABLES

        [SerializeField] private List<AIBehaviourPart> parts;
        [SerializeField] private int currentPart;

        #endregion

        #region PROPERTIES

        public List<AIBehaviourPart> Parts => parts;
        public int CurrentPart => currentPart;

        #endregion

        #region CONSTRUCTORS

        public AIBehaviour() { /*Serialization*/ }

        #endregion

        #region METHODS

        #endregion

    }
}