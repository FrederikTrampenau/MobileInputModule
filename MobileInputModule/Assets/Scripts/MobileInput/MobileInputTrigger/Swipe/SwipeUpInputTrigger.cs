// <copyright>(c) Frederik Trampenau 2019.</copyright>
// <author>Frederik Trampenau</author>
#pragma warning disable 0649 //Disable "default value is null" warning

using UnityEngine;

namespace MIM.InputTriggers
{
    /// <summary>
    /// Input trigger of the up swipe.
    /// </summary>
    [CreateAssetMenu(fileName = "SwipeUpInputTrigger", menuName = "InputTrigger/ActionTriggers/SwipeUpInputTrigger")]
    sealed class SwipeUpInputTrigger : InputActionTrigger
    {
        #region Functions And Methods

        /// <summary>
        /// Make the constructor private to prevent multiple instances.
        /// </summary>
        private SwipeUpInputTrigger() { }


        /// <summary>
        /// Calls base input trigger setup and subscribes to the "OnUpSwipeDetected" action of the SwipeEvaluator.
        /// </summary>
        /// <param name="inputReactionComponent">InputReactionField the trigger is attached to.</param>
        public override void Setup(InputReactionField inputReactionComponent)
        {
            base.Setup(inputReactionComponent);
            SwipeEvaluator.OnUpSwipeDetected += Trigger;
        }


        /// <summary>
        /// Unsubscribes from the "OnUpSwipeDetected" action of the SwipeEvaluator.
        /// </summary>
        public override void Shutdown() => SwipeEvaluator.OnUpSwipeDetected -= Trigger;


        /// <summary>
        /// Calls OnSwipeUp action of the owning InputReactionField and the MobileInput class.
        /// </summary>
        public override void Trigger()
        {
            owningInputReactionFields.ForEach(reactionField => reactionField.OnActionTriggered?.Invoke());
            MobileInput.OnSwipeUp?.Invoke();
        }

        #endregion Functions And Methods
    }
}