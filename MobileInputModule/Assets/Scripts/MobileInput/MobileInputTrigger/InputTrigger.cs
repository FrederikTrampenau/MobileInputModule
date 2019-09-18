// <copyright>(c) Frederik Trampenau 2019.</copyright>
// <author>Frederik Trampenau</author>
#pragma warning disable 0649 //Disable "default value is null" warning

using MIM.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace MIM.InputTriggers
{
    /// <summary>
    /// Base class for mobile input triggers.
    /// </summary>
    public class InputTrigger : ScriptableObject, IMobileInputTriggerable
    {
        #region Properties

        /// <summary>
        /// Container for owning reaction fields that get messaged when the specific input of the trigger gets triggered.
        /// </summary>
        protected List<InputReactionField> owningInputReactionFields = new List<InputReactionField>();

        #endregion Properties



        #region Functions And Methods

        /// <summary>
        /// Resets the owning input reaction fields container.
        /// </summary>
        private void OnEnable() => owningInputReactionFields.Clear();


        /// <summary>
        /// Set up of all input triggers, sets the owning input reaction component to be able to message it.
        /// </summary>
        /// <param name="inputReactionField">InputReactionField the trigger is attached to.</param>
        virtual public void Setup(InputReactionField inputReactionField) => owningInputReactionFields.Add(inputReactionField);


        /// <summary>
        /// Gets called when the input triggers and calls actions according to this input.
        /// </summary>
        virtual public void Trigger() { }


        /// <summary>
        /// Gets called when the input triggers and calls actions according to this input with evaluated input arguments.
        /// </summary>
        /// <param name="args">Evaluated arguments of the input.</param>
        virtual public void Trigger(IPositionalInputArgs args) { }


        /// <summary>
        /// Shutdown the trigger and unsubscribe from all actions (unsubscribe manually in the "Shutdown" of child classes).
        /// </summary>
        virtual public void Shutdown() { }

        #endregion Functions And Methods
    }
}