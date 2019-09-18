// <copyright>(c) Frederik Trampenau 2019.</copyright>
// <author>Frederik Trampenau</author>
#pragma warning disable 0649 //Disable "default value is null" warning

using MIM.Interfaces;
using UnityEngine;

namespace MIM.InputTriggers
{
    /// <summary>
    /// Input trigger of the ongoing right tilt.
    /// </summary>
    [CreateAssetMenu(fileName = "OngoingTiltRightInputTrigger", menuName = "InputTrigger/ArgumentTriggers/OngoingTiltRightInputTrigger")]
    sealed class OngoingTiltRightInputTrigger : InputArgsTrigger
    {
        #region Functions And Methods

        /// <summary>
        /// Make the constructor private to prevent multiple instances.
        /// </summary>
        private OngoingTiltRightInputTrigger() { }


        /// <summary>
        /// Calls base input trigger setup and subscribes to the "OnOngoingTiltRight" action.
        /// </summary>
        /// <param name="inputReactionComponent">InputReactionField the trigger is attached to.</param>
        public override void Setup(InputReactionField inputReactionComponent)
        {
            base.Setup(inputReactionComponent);
            TiltEvaluator.OnOngoingTiltRight += Trigger;
        }


        /// <summary>
        /// Unsubscribes from the "OnOngoingTiltRight" action of the SwipeEvaluator.
        /// </summary>
        public override void Shutdown()
        {
            TiltEvaluator.OnOngoingTiltRight -= Trigger;
        }


        /// <summary>
        /// Calls OnArgumentInputTriggered action of the owning InputReactionField and the OnOngoingTiltRight action of the MobileInput class.
        /// </summary>
        /// <param name="args">Input arguments of the ongoing tilt.</param>
        public override void Trigger(IPositionalInputArgs args)
        {
            owningInputReactionFields.ForEach(reactionField => reactionField.OnArgumentInputTriggered?.Invoke(args));
            MobileInput.OnOngoingTiltRight?.Invoke(args);
        }

        #endregion Functions And Methods
    }
}