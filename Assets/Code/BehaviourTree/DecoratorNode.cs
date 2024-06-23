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

        protected Node Child => node;

        #endregion

        #region METHODS

        #endregion
    }
}