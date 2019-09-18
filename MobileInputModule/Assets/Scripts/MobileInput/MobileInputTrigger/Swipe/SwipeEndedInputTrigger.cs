// <copyright>(c) Frederik Trampenau 2019.</copyright>
// <author>Frederik Trampenau</author>
#pragma warning disable 0649 //Disable "default value is null" warning

using MIM.Interfaces;
using UnityEngine;

namespace MIM.InputTriggers
{
    /// <summary>
    /// Input trigger of the ended swipe.
    /// </summary>
    [CreateAssetMenu(fileName = "SwipeEndedInputTrigger", menuName = "InputTrigger/ArgumentTriggers/SwipeEndedInputTrigger")]
    class SwipeEndedInputTrigger : InputArgsTrigger
    {
        #region Functions And Methods

        /// <summary>
        /// Make the constructor private to prevent multiple instances.
        /// </summary>
        private SwipeEndedInputTrigger() { }


        /// <summary>
        /// Calls base input trigger setup and subscribes to the "OnSwipeEndedDetected" action.
        /// </summary>
        /// <param name="inputReactionComponent">InputReactionField the trigger is attached to.</param>
        public override void Setup(InputReactionField inputReactionComponent)
        {
            base.Setup(inputReactionComponent);
            SwipeEvaluator.OnSwipeEnded += Trigger;
        }


        /// <summary>
        /// Unsubscribes from the "OnSwipeDetected" action of the SwipeEventDetector.
        /// </summary>
        public override void Shutdown() => SwipeEvaluator.OnSwipeEnded -= Trigger;


        /// <summary>
        /// Calls OnAxisInputTriggered action of the owning InputReactionField and the OnSwipeMoving action MobileInput class with the finished swipe arguments.
        /// </summary>
        /// <param name="args">Arguments of the completed swipe.</param>
        public override void Trigger(IPositionalInputArgs args)
        {
            owningInputReactionFields.ForEach(reactionField => reactionField.OnArgumentInputTriggered?.Invoke(args));
            MobileInput.OnSwipeEnded?.Invoke(args);
        }

        #endregion Functions And Methods
    }
}