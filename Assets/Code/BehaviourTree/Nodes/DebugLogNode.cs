using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTreeSystem
{
    public class DebugLogNode : ActionNode
    {
        #region VARIABLES

        [SerializeField] private string message;

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        protected override void OnStart()
        {
            Debug.Log(StringBuilderScaler.GetScaledText("OnStart: ", message));
        }

        protected override void OnStop()
        {
            Debug.Log(StringBuilderScaler.GetScaledText("OnStop: ", message));
        }

        protected override NodeState OnUpdate()
        {
            Debug.Log(StringBuilderScaler.GetScaledText("OnUpdate: ", message));
            return NodeState.SUCCESS;
        }

        #endregion
    }
}