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
        [SerializeField] private string guid;
        [SerializeField] private Vector2 position;

        #endregion

        #region PROPERTIES

        public Vector2 Position { get { return position; } set { position = value; } }
        public string Guid { get { return guid; } set { guid = value; } }
        public NodeState State => state;

        #endregion

        #region METHODS

        public NodeState Update()
        {
            if (started == false)
            {
                started = true;
                OnStart();
            }

            state = OnUpdate();

            if (state != NodeState.RUNNING)
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
