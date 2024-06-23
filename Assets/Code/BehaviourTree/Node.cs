using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTreeSystem
{
    public abstract class Node : ScriptableObject
    {
        #region VARIABLES

        [SerializeField] private NodeState state;
        [SerializeField] private bool started = false;

        #endregion

        #region PROPERTIES

        public NodeState State => state;

        #endregion

        #region METHODS

        public NodeState Update()
        {
            if(started == false)
            {
                started = true;
                OnStart();
            }

            state = OnUpdate();

            if(state!= NodeState.RUNNING)
            {
                started = false;
                OnStop();
            }

            return state;
        }

        protected abstract void OnStart();
        protected abstract void OnStop();
        protected abstract NodeState OnUpdate();

        #endregion
    }
}
