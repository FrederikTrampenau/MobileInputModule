// <copyright>(c) Frederik Trampenau 2019.</copyright>
// <author>Frederik Trampenau</author>
#pragma warning disable 0649 //Disable "default value is null" warning

using MIM.Interfaces;
using UnityEngine;

namespace MIM.InputTriggers
{
    /// <summary>
    /// Input trigger of the ongoing backward tilt.
    /// </summary>
    [CreateAssetMenu(fileName = "OngoingTiltBackwardInputTrigger", menuName = "InputTrigger/ArgumentTriggers/OngoingTiltBackwardInputTrigger")]
    sealed class OngoingTiltBackwardInputTrigger : InputArgsTrigger
    {
        #region Functions And Methods

        /// <summary>
        /// Make the constructor private to prevent multiple instances.
        /// </summary>
        private OngoingTiltBackwardInputTrigger() { }


        /// <summary>
        /// Calls base input trigger setup and subscribes to the "OnRightSwipeDetected" action.
        /// </summary>
        /// <param name="inputReactionComponent">InputReactionField the trigger is attached to.</param>
        public override void Setup(InputReactionField inputReactionComponent)
        {
            base.Setup(inputReactionComponent);
            TiltEvaluator.OnOngoingTiltBackward += Trigger;
        }


        /// <summary>
        /// Unsubscribes from the "OnRightSwipeDetected" action of the SwipeEvaluator.
        /// </summary>
        public override void Shutdown() => TiltEvaluator.OnOngoingTiltBackward -= Trigger;


        /// <summary>
        /// Calls OnArgumentInputTriggered action of the owning InputReactionField and the OnOngoingTiltBackward action of the MobileInput class.
        /// </summary>
        public override void Trigger(IPositionalInputArgs args)
        {
            owningInputReactionFields.ForEach(reactionField => reactionField.OnArgumentInputTriggered?.Invoke(args));
            MobileInput.OnOngoingTiltBackward?.Invoke(args);
        }

        #endregion Functions And Methods
    }
}