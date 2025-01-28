using ObjectPooling;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Items
{
    [CreateAssetMenu(menuName = "Body/BodyElementData", fileName = "BodyElementData")]
    public class BodyElementData : ElementDataBase
    {
        #region VARIABLES

        [SerializeField, FoldoutGroup("BaseInfo")] private BodyElementType elementType;
        [SerializeField, FoldoutGroup("Visualization"), ValueDropdown(ObjectPoolDatabase.GET_POOL_BODY_ELEMENTS_METHOD)] private int visualizationsId;

        #endregion

        #region PROPERTIES

        public BodyElementType ElementType => elementType;
        public int VisualizationsId => visualizationsId;

        #endregion

        #region METHODS


        #endregion
    }
}