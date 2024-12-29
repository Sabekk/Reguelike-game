using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Window
{
    public abstract class UIWindowBase : MonoBehaviour
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        #endregion

        #region UNITY_METHODS

        protected virtual void OnEnable()
        {
            AttachEvents();
            Refresh();
        }

        protected virtual void OnDisable()
        {
            DetachEvents();
        }

        #endregion

        #region METHODS

        protected virtual void Initialize()
        {
            InitializingAttachEvents();
        }

        protected virtual void CleanUp()
        {
            InitializingDetachEvents();
        }

        /// <summary>
        /// Refreshing window. Called every on enable
        /// </summary>
        protected virtual void Refresh()
        {

        }

        /// <summary>
        /// Attaching events on enable
        /// </summary>
        protected virtual void AttachEvents()
        {

        }

        /// <summary>
        /// Detaching events on enable
        /// </summary>
        protected virtual void DetachEvents()
        {

        }

        /// <summary>
        /// Attaching events in initialize
        /// </summary>
        protected virtual void InitializingAttachEvents()
        {

        }

        /// <summary>
        /// Detaching events from initialize. Calleon in CleanUp
        /// </summary>
        protected virtual void InitializingDetachEvents()
        {

        }

        #endregion
    }
}
