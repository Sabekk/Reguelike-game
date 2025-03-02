using Gameplay.Items;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character.Data
{
    [CreateAssetMenu(menuName = "Data/Character/StartValues/Body/StartingBodyItems", fileName = "StartingBodyItems")]
    public class StartingBodyItems : ScriptableObject
    {
        #region VARIABLES

        [SerializeField, OnValueChanged(nameof(SetTypesForElements)), OnCollectionChanged(nameof(ResetElementTypes))] private SerializableDictionary<BodyElementType, StartingBodyItem> bodyElements;

        #endregion

        #region PROPERTIES

        public SerializableDictionary<BodyElementType, StartingBodyItem> BodyElements => bodyElements;

        #endregion

        #region METHODS
        private void SetTypesForElements()
        {
            foreach (var bodyElement in bodyElements)
            {
                bodyElement.Value.TrySetElementType(bodyElement.Key);
            }
        }

        private void ResetElementTypes()
        {
            foreach (var bodyElement in bodyElements)
            {
                bodyElement.Value.TrySetElementType(bodyElement.Key, true);
            }
        }

        #endregion
    }
}
