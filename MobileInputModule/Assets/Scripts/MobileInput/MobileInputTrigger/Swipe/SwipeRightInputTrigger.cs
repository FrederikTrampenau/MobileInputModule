// <copyright>(c) Frederik Trampenau 2019.</copyright>
// <author>Frederik Trampenau</author>
#pragma warning disable 0649 //Disable "default value is null" warning

using UnityEngine;

namespace MIM.InputTriggers
{
    /// <summary>
    /// Input trigger of the right swipe.
    /// </summary>
    [CreateAssetMenu(fileName = "SwipeRightInputTrigger", menuName = "InputTrigger/ActionTriggers/SwipeRightInputTrigger")]
    sealed class SwipeRightInputTrigger : InputActionTrigger
    {
        #region Functions And Methods

        /// <summary>
        /// Make the constructor private to prevent multiple instances.
        /// </summary>
        private SwipeRightInputTrigger() { }


        /// <summary>
        /// Calls base input trigger setup and subscribes to the "OnRightSwipeDetected" action.
        /// </summary>
        /// <param name="inputReactionComponent">InputReactionField the trigger is attached to.</param>
        public override void Setup(InputReactionField inputReactionComponent)
        {
            base.Setup(inputReactionComponent);
            SwipeEvaluator.OnRightSwipeDetected += Trigger;
        }


        /// <summary>
        /// Unsubscribes from the "OnRightSwipeDetected" action of the SwipeEvaluator.
        /// </summary>
        public override void Shutdown() => SwipeEvaluator.OnRightSwipeDetected -= Trigger;


        /// <summary>
        /// Calls OnSwipeRight action of the owning InputReactionField and the MobileInput class.
        /// </summary>
        public override void Trigger()
        {
            owningInputReactionFields.ForEach(reactionField => reactionField.OnActionTriggered?.Invoke());
            MobileInput.OnSwipeRight?.Invoke();
        }

        #endregion Functions And Methods
    }
}