using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.AIBehaviour
{
    public class AIBehaviour_DebugTest : AIBehaviour
    {
        #region VARIABLES

        [SerializeField] private string debugText;

        #endregion

        #region METHODS

        public override void OnUpdate()
        {
            Debug.Log(debugText);
        }

        #endregion
    }
}