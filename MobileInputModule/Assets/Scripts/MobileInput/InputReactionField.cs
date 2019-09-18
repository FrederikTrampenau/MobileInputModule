// <copyright>(c) Frederik Trampenau 2019.</copyright>
// <author>Frederik Trampenau</author>
#pragma warning disable 0649 //Disable "default value is null" warning

using MIM.InputTriggers;
using MIM.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MIM
{
    /// <summary>
    /// Component which reacts to mobile input.
    /// </summary>
    [Serializable]
    public class InputReactionField
    {
        #region Public Fields

        /// <summary>
        /// Container for the triggers of the action input event.
        /// </summary>
        [Header("Mobile Input Action Triggers")]
        [SerializeField]
        public List<InputActionTrigger> inputActionTriggers = new List<InputActionTrigger>();

        /// <summary>
        /// Container for the triggers of the argument input event.
        /// </summary>
        [Header("Mobile Input Argument Triggers")]
        [SerializeField]
        public List<InputArgsTrigger> inputArgumentTriggers = new List<InputArgsTrigger>();

        #endregion Public Fields



        #region Events

        /// <summary>
        /// Unity event for input triggers without arguments.
        /// </summary>
        [Serializable]
        public class MobileActionEvent : UnityEvent { }

        /// <summary>
        /// Unity event for input triggers which contain arguments.
        /// </summary>
        [Serializable]
        public class MobileArgumentEvent : UnityEvent<IPositionalInputArgs> { }

        /// <summary>
        /// Unity event for the inspector which gets called by input triggers which have no arguments.
        /// </summary>
        [SerializeField]
        public MobileActionEvent m_OnActionTriggered = new MobileActionEvent();

        /// <summary>
        /// Unity event for input triggers which have no arguments.
        /// </summary>
        public MobileActionEvent OnActionTriggered
        {
            get { return m_OnActionTriggered; }
            set { m_OnActionTriggered = value; }
        }

        /// <summary>
        /// Unity event for the inspector which gets called by input triggers which have arguments.
        /// </summary>
        [SerializeField]
        public MobileArgumentEvent m_OnArgumentInputTriggered = new MobileArgumentEvent();

        /// <summary>
        /// Unity event for input triggers which have arguments.
        /// </summary>
        public MobileArgumentEvent OnArgumentInputTriggered
        {
            get { return m_OnArgumentInputTriggered; }
            set { m_OnArgumentInputTriggered = value; }
        }

        #endregion Events



        #region Functions And Methods

        /// <summary>
        /// Set up all triggers added to this reaction field.
        /// </summary>
        public void Setup()
        {
            // Set up action triggers
            foreach (var actionTrigger in inputActionTriggers)
                actionTrigger?.Setup(this);

            // Set up argument triggers
            foreach (var argumentTrigger in inputArgumentTriggers)
                argumentTrigger?.Setup(this);
        }


        /// <summary>
        /// Let all triggers unsubscribe from their subscribed actions.
        /// </summary>
        public void Shutdown()
        {
            // Shutdown action triggers
            foreach (var actionTrigger in inputActionTriggers)
                actionTrigger?.Shutdown();
            // Shutdown argument triggers
            foreach (var argumentTrigger in inputArgumentTriggers)
                argumentTrigger?.Shutdown();
        }

        #endregion Functions And Methods
    }
}