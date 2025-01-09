using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character.Body
{
    public class CharacterBodyContainer : MonoBehaviour
    {
        #region VARIABLES

        [SerializeField] private List<BodyElement> bodyElements;

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        [Button]
        private void FindAllBodyElements()
        {
            bodyElements.Clear();
            bodyElements.AddRange(transform.GetComponentsInChildren<BodyElement>());
        }

        #endregion
    }
}