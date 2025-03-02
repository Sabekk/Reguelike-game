using ObjectPooling;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character.Data
{
    [CreateAssetMenu(menuName = "Data/Character/StartValues/Body/StartingBodyData", fileName = "StartingBodyData")]
    public class StartingBodyData : ScriptableObject
    {
        #region VARIABLES

        [SerializeField, ValueDropdown(ObjectPoolDatabase.GET_POOL_CONTAINER_METHOD)] private int bodyContainerId;
        [SerializeField] private BodyType bodyType;
        [SerializeReference] private StartingBodyItems startingBody;

        #endregion

        #region PROPERTIES

        public int BodyContainerId => bodyContainerId;
        public BodyType BodyType => bodyType;
        public StartingBodyItems StartingBody => startingBody;

        #endregion

        #region METHODS

        #endregion
    }
}