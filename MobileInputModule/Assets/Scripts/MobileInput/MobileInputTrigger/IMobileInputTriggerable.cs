// <copyright>(c) Frederik Trampenau 2019.</copyright>
// <author>Frederik Trampenau</author>
#pragma warning disable 0649 //Disable "default value is null" warning

namespace MIM.Interfaces
{
    /// <summary>
    /// Interface for all input triggers.
    /// </summary>
    interface IMobileInputTriggerable
    {
        /// <summary>
        /// Set up of all input triggers, sets the owning input reaction component to be able to message it.
        /// </summary>
        /// <param name="inputReactionField">InputReactionField the trigger is attached to.</param>
        void Setup(InputReactionField inputReactionField);

        /// <summary>
        /// Shutdown the trigger and unsubscribe from all actions (unsubscribe manually in the "Shutdown" of child classes).
        /// </summary>
        void Shutdown();

        /// <summary>
        /// Gets called when the input triggers and calls actions according to this input.
        /// </summary>
        void Trigger();

        /// <summary>
        /// Gets called when the input triggers and calls actions according to this input with evaluated input arguments.
        /// </summary>
        /// <param name="args">Evaluated arguments of the input.</param>
        void Trigger(IPositionalInputArgs args);
    }
}