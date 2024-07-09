using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTreeSystem
{
    public abstract class Node : ScriptableObject
    {
        #region VARIABLES

        [SerializeField, ReadOnly] private string guid;
        [SerializeField, HideInInspector] private NodeState state;
        [SerializeField, HideInInspector] private bool started = false;
        [SerializeField, HideInInspector] private Vector2 position;

        #endregion

        #region PROPERTIES

        public Vector2 Position { get { return position; } set { position = value; } }
        public string Guid { get { return guid; } set { guid = value; } }
        public NodeState State => state;

        #endregion

        #region METHODS

        public virtual Node Clone()
        {
            return Instantiate(this);
        }

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
        public void SetPosition(Vector2 position)
        {
            this.position = position;
        }
        protected abstract void OnStart();
        protected abstract void OnStop();
        protected abstract NodeState OnUpdate();

        #endregion
    }
}
