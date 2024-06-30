using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTreeSystem
{
    public abstract class DecoratorNode : Node
    {
        #region VARIABLES

        [SerializeField] private Node node;

        #endregion

        #region PROPERTIES

        public Node Child => node;

        #endregion

        #region METHODS

        public void SetChild(Node child)
        {
            this.node = child;
        }

        #endregion
    }
}