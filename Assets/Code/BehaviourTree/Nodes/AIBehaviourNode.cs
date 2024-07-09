using Gameplay.AIBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTreeSystem
{
    public class AIBehaviourNode : ActionNode
    {
        #region VARIABLES

        [SerializeReference] AIBehaviour aibehaviour;

        #endregion

        #region METHODS

        protected override void OnStart()
        {
            aibehaviour.Initialize();
        }

        protected override void OnStop()
        {
            aibehaviour.CleanUp();
        }

        protected override NodeState OnUpdate()
        {
            aibehaviour.OnUpdate();
            return NodeState.RUNNING;
        }

        #endregion
    }
}