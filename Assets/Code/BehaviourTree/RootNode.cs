using Sirenix.OdinInspector;
using UnityEngine;

namespace BehaviourTreeSystem
{
    public class RootNode : Node
    {
        #region VARIABLES

        [SerializeField, ReadOnly] private Node child;

        #endregion

        #region PROPERTIES

        public Node Child => child;

        #endregion

        #region METHODS

        public void SetChild(Node child)
        {
            this.child = child;
        }

        public override Node Clone()
        {
            RootNode node = Instantiate(this);
            node.child = child.Clone();
            return node;
        }

        protected override void OnStart()
        {

        }

        protected override void OnStop()
        {

        }

        protected override NodeState OnUpdate()
        {
            return child.Update();
        }

        #endregion
    }
}