using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTreeSystem
{
    public class WaitNode : ActionNode
    {
        #region VARIABLES

        [SerializeField] private float duration;
        [SerializeField, HideInInspector] private float currentTime;

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        protected override void OnStart()
        {

        }

        protected override void OnStop()
        {

        }

        protected override NodeState OnUpdate()
        {
            currentTime += Time.deltaTime;
            if (currentTime >= duration)
                return NodeState.SUCCESS;

            return NodeState.RUNNING;
        }

        #endregion
    }
}