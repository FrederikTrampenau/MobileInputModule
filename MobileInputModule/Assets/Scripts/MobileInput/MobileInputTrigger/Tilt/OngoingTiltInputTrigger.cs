// <copyright>(c) Frederik Trampenau 2019.</copyright>
// <author>Frederik Trampenau</author>
#pragma warning disable 0649 //Disable "default value is null" warning

using MIM.Interfaces;
using UnityEngine;

namespace MIM.InputTriggers
{
    /// <summary>
    /// Input trigger of the ongoing tilt.
    /// </summary>
    [CreateAssetMenu(fileName = "OngoingTiltInputTrigger", menuName = "InputTrigger/ArgumentTriggers/OngoingTiltInputTrigger")]
    sealed class OngoingTiltInputTrigger : InputArgsTrigger
    {
        #region Functions And Methods

        /// <summary>
        /// Make the constructor private to prevent multiple instances.
        /// </summary>
        private OngoingTiltInputTrigger() { }


        /// <summary>
        /// Calls base input trigger setup and subscribes to the "OnPinchDetected" action.
        /// </summary>
        /// <param name="inputReactionComponent">InputReactionField the trigger is attached to.</param>
        public override void Setup(InputReactionField inputReactionComponent)
        {
            base.Setup(inputReactionComponent);
            TiltEvaluator.OnOngoingTilt += Trigger;
        }


        /// <summary>
        /// Unsubscribes from the "OnPinchDetected" action of the SwipeEventDetector.
        /// </summary>
        public override void Shutdown()
        {
            TiltEvaluator.OnOngoingTilt -= Trigger;
        }


        /// <summary>
        /// Calls OnAxisInputTriggered action of the owning InputReactionField and the OnPinchMoving action MobileInput class with the given input arguments.
        /// </summary>
        /// <param name="args">Input arguments of the ongoing tilt.</param>
        public override void Trigger(IPositionalInputArgs args)
        {
            owningInputReactionFields.ForEach(reactionField => reactionField.OnArgumentInputTriggered?.Invoke(args));
            MobileInput.OnOngoingTilt?.Invoke(args);
        }

        #endregion Functions And Methods
    }
}