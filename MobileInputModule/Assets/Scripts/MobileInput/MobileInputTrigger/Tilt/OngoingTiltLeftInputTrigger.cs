// <copyright>(c) Frederik Trampenau 2019.</copyright>
// <author>Frederik Trampenau</author>
#pragma warning disable 0649 //Disable "default value is null" warning

using MIM.Interfaces;
using UnityEngine;

namespace MIM.InputTriggers
{
    /// <summary>
    /// Input trigger of the ongoing left tilt.
    /// </summary>
    [CreateAssetMenu(fileName = "OngoingTiltLeftInputTrigger", menuName = "InputTrigger/ArgumentTriggers/OngoingTiltLeftInputTrigger")]
    sealed class OngoingTiltLeftInputTrigger : InputArgsTrigger
    {
        #region Functions And Methods

        /// <summary>
        /// Make the constructor private to prevent multiple instances.
        /// </summary>
        private OngoingTiltLeftInputTrigger() { }


        /// <summary>
        /// Calls base input trigger setup and subscribes to the "OnOngoingTiltLeft" action.
        /// </summary>
        /// <param name="inputReactionComponent">InputReactionField the trigger is attached to.</param>
        public override void Setup(InputReactionField inputReactionComponent)
        {
            base.Setup(inputReactionComponent);
            TiltEvaluator.OnOngoingTiltLeft += Trigger;
        }


        /// <summary>
        /// Unsubscribes from the "OnOngoingTiltLeft" action of the SwipeEvaluator.
        /// </summary>
        public override void Shutdown()
        {
            TiltEvaluator.OnOngoingTiltLeft -= Trigger;
        }


        /// <summary>
        /// Calls OnArgumentInputTriggered action of the owning InputReactionField and the OnOngoingTiltLeft action of the MobileInput class.
        /// </summary>
        /// <param name="args">Input arguments of the ongoing tilt.</param>
        public override void Trigger(IPositionalInputArgs args)
        {
            owningInputReactionFields.ForEach(reactionField => reactionField.OnArgumentInputTriggered?.Invoke(args));
            MobileInput.OnOngoingTiltLeft?.Invoke(args);
        }

        #endregion Functions And Methods
    }
}