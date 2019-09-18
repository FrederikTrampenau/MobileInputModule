// <copyright>(c) Frederik Trampenau 2019.</copyright>
// <author>Frederik Trampenau</author>
#pragma warning disable 0649 //Disable "default value is null" warning

using MIM.Interfaces;
using UnityEngine;

namespace MIM.InputTriggers
{
    /// <summary>
    /// Input trigger of the ongoing forward tilt.
    /// </summary>
    [CreateAssetMenu(fileName = "OngoingTiltForwardInputTrigger", menuName = "InputTrigger/ArgumentTriggers/OngoingTiltForwardInputTrigger")]
    sealed class OngoingTiltForwardInputTrigger : InputArgsTrigger
    {
        #region Functions And Methods

        /// <summary>
        /// Make the constructor private to prevent multiple instances.
        /// </summary>
        private OngoingTiltForwardInputTrigger() { }


        /// <summary>
        /// Calls base input trigger setup and subscribes to the "OnOngoingTiltForward" action.
        /// </summary>
        /// <param name="inputReactionComponent">InputReactionField the trigger is attached to.</param>
        public override void Setup(InputReactionField inputReactionComponent)
        {
            base.Setup(inputReactionComponent);
            TiltEvaluator.OnOngoingTiltForward += Trigger;
        }


        /// <summary>
        /// Unsubscribes from the "OnOngoingTiltForward" action of the SwipeEvaluator.
        /// </summary>
        public override void Shutdown() => TiltEvaluator.OnOngoingTiltForward -= Trigger;


        /// <summary>
        /// Calls OnArgumentInputTriggered action of the owning InputReactionField and the OnOngoingTiltForward action of the MobileInput class.
        /// </summary>
        /// <param name="args">Arguments of the input event.</param>
        public override void Trigger(IPositionalInputArgs args)
        {
            owningInputReactionFields.ForEach(reactionField => reactionField.OnArgumentInputTriggered?.Invoke(args));
            MobileInput.OnOngoingTiltForward?.Invoke(args);
        }

        #endregion Functions And Methods
    }
}