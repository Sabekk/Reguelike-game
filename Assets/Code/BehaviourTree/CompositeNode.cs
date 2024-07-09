using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTreeSystem
{
    public abstract class CompositeNode : Node
    {
        #region VARIABLES

        [SerializeField, ReadOnly] private List<Node> children;

        #endregion

        #region PROPERTIES

        public List<Node> Children => children;

        #endregion

        #region METHODS

        public override Node Clone()
        {
            CompositeNode node = Instantiate(this);
            node.children = Children.ConvertAll(c => c.Clone());
            return node;
        }

        public void AddChild(Node child)
        {
            children.Add(child);
        }

        public void RemoveChild(Node child)
        {
            children.Remove(child);
        }

        #endregion
    }
}
