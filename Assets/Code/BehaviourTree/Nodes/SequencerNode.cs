using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTreeSystem
{
    public class SequencerNode : CompositeNode
    {
        #region VARIABLES

        [SerializeField] private int current;

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        protected override void OnStart()
        {
            current = 0;
        }

        protected override void OnStop()
        {

        }

        protected override NodeState OnUpdate()
        {
            var child = Children[current];
            switch (child.Update())
            {
                case NodeState.RUNNING:
                    return NodeState.RUNNING;
                case NodeState.SUCCESS:
                    current++;
                    break;
                case NodeState.FAILURE:
                    return NodeState.FAILURE;
                default:
                    break;
            }

            return current == Children.Count ? NodeState.SUCCESS : NodeState.RUNNING;
        }

        #endregion
    }
}
