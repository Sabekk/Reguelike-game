using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTreeSystem
{
    [CreateAssetMenu()]
    public class BehaviourTree : ScriptableObject
    {
        #region VARIABLES

        [SerializeField] private Node node;
        [SerializeField] private NodeState nodeState;

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        public NodeState Update()
        {
            if (node.State == NodeState.RUNNING)
                nodeState = node.Update();

            return nodeState;
        }

        #endregion
    }
}
