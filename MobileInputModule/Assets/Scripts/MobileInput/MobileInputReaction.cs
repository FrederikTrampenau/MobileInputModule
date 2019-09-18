// <copyright>(c) Frederik Trampenau 2019.</copyright>
// <author>Frederik Trampenau</author>
#pragma warning disable 0649 //Disable "default value is null" warning

using System.Collections.Generic;
using UnityEngine;

namespace MIM
{
    /// <summary>
    /// Component which gets used to react to mobile input from inside the editor.
    /// </summary>
    [AddComponentMenu("Event/Mobile Input Reaction")]
    public class MobileInputReaction : MonoBehaviour
    {
        #region Serialized Fields

        /// <summary>
        /// Container for the input reaction fields.
        /// </summary>
        [SerializeField]
        public List<InputReactionField> inputReactionFields = new List<InputReactionField>();

        /// <summary>
        /// Index container for all reaction fields that use action triggers.
        /// </summary>
        [SerializeField]
        public List<int> reactionFieldsUsingActions = new List<int>();

        /// <summary>
        /// Index container for all reaction fields that use argument triggers.
        /// </summary>
        [SerializeField]
        public List<int> reactionFieldsUsingArguments = new List<int>();

        /// <summary>
        /// Container for all reaction field indices and if they use action triggers (assigned in the inspector).
        /// </summary>
        [HideInInspector, SerializeField]
        public Dictionary<int, bool> useActionTriggerContainer = new Dictionary<int, bool>();

        /// <summary>
        /// Container for all reaction field indices and if they use argument triggers (assigned in the inspector).
        /// </summary>
        [HideInInspector, SerializeField]
        public Dictionary<int, bool> useArgumentTriggerContainer = new Dictionary<int, bool>();

        #endregion Serialized Fields



        #region Functions And Methods

        /// <summary>
        /// Make the constructor private to prevent multiple instances.
        /// </summary>
        private MobileInputReaction() { }


        /// <summary>
        /// Calls setup of all input reaction fields.
        /// Using the Start method because some reaciton fields have to be created in Awake first.
        /// </summary>
        private void Start()
        {
            Setup();

            foreach (var reactionField in inputReactionFields)
                reactionField.Setup();
        }


        /// <summary>
        /// Set up the action and argument trigger dictionaries.
        /// </summary>
        private void Setup()
        {
            var updatedUseActionTriggerContainer = new Dictionary<int, bool>();
            foreach (var trigger in useActionTriggerContainer)
                updatedUseActionTriggerContainer.Add(trigger.Key, reactionFieldsUsingActions.Contains(trigger.Key));

            useActionTriggerContainer = updatedUseActionTriggerContainer;

            var updatedUseArgumentTriggerContainer = new Dictionary<int, bool>();
            foreach (var trigger in useArgumentTriggerContainer)
                updatedUseArgumentTriggerContainer.Add(trigger.Key, reactionFieldsUsingArguments.Contains(trigger.Key));

            useArgumentTriggerContainer = updatedUseArgumentTriggerContainer;
        }


        /// <summary>
        /// Initialize shutdown of all attached reactions.
        /// </summary>
        private void OnDestroy()
        {
            foreach (var reactionField in inputReactionFields)
                reactionField.Shutdown();
        }

        #endregion Functions And Methods
    }
}