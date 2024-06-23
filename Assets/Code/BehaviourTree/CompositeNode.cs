using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTreeSystem
{
    public abstract class CompositeNode : Node
    {
        #region VARIABLES

        [SerializeField] private List<Node> nodes;

        #endregion

        #region PROPERTIES

        protected List<Node> Childs => nodes;

        #endregion

        #region METHODS

        #endregion
    }
}
