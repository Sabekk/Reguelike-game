using Gameplay.Character.Body;
using Gameplay.Character.Controller;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character
{
    /// <summary>
    /// Controller to collect default body elements
    /// </summary>
    [Serializable]
    public class BodyController : ControllerBase
    {
        #region VARIABLES

        [SerializeField] private BodyType bodyType;
        [SerializeField] private List<BodySocket> itemsInUse;

        #endregion


        #region PROPERTIES

        public BodyType BodyType => bodyType;

        #endregion

        #region CONSTRUCTORS

        #endregion

        #region METHODS


        #endregion
    }
}