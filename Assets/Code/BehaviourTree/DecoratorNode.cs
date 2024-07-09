using Sirenix.OdinInspector;
using UnityEngine;

namespace BehaviourTreeSystem
{
    public abstract class DecoratorNode : Node
    {
        #region VARIABLES

        [SerializeField, ReadOnly] private Node child;

        #endregion

        #region PROPERTIES

        public Node Child => child;

        #endregion

        #region METHODS

        public override Node Clone()
        {
            DecoratorNode node = Instantiate(this);
            node.child = child.Clone();
            return node;
        }

        public void SetChild(Node child)
        {
            this.child = child;
        }

        #endregion
    }
}