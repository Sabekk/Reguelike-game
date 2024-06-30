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

        public List<Node> Childs => nodes;

        #endregion

        #region METHODS

        public void AddChild(Node child)
        {
            nodes.Add(child);
        }

        public void RemoveChild(Node child)
        {
            nodes.Remove(child);
        }

        #endregion
    }
}
