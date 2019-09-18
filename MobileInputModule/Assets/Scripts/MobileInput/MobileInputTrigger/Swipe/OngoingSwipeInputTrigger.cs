// <copyright>(c) Frederik Trampenau 2019.</copyright>
// <author>Frederik Trampenau</author>
#pragma warning disable 0649 //Disable "default value is null" warning

using MIM.Interfaces;
using UnityEngine;

namespace MIM.InputTriggers
{
    /// <summary>
    /// Input trigger of the ongoing swipe
    /// </summary>
    [CreateAssetMenu(fileName = "OngoingSwipeInputTrigger", menuName = "InputTrigger/ArgumentTriggers/OngoingSwipeInputTrigger")]
    sealed class OngoingSwipeInputTrigger : InputArgsTrigger
    {
        #region Functions And Methods

        /// <summary>
        /// Make the constructor private to prevent multiple instances.
        /// </summary>
        private OngoingSwipeInputTrigger() { }


        /// <summary>
        /// Calls base input trigger setup and subscribes to the "OnSwipeDetected" action.
        /// </summary>
        /// <param name="inputReactionComponent">InputReactionField the trigger is attached to.</param>
        public override void Setup(InputReactionField inputReactionComponent)
        {
            base.Setup(inputReactionComponent);
            SwipeEventDetector.OnSwipeDetected += Trigger;
        }


        /// <summary>
        /// Unsubscribes from the "OnSwipeDetected" action of the SwipeEventDetector.
        /// </summary>
        public override void Shutdown() => SwipeEventDetector.OnSwipeDetected -= Trigger;


        /// <summary>
        /// Calls OnAxisInputTriggered action of the owning InputReactionField with the ongoing pinch movement of the last frame.
        /// </summary>
        /// <param name="args">Input arguments of the ongoing pinch.</param>
        public override void Trigger(IPositionalInputArgs args) => owningInputReactionFields.ForEach(reactionField => reactionField.OnArgumentInputTriggered?.Invoke(args));

        #endregion Functions And Methods
    }
}