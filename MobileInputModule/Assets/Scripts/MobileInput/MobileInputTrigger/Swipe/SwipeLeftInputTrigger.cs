// <copyright>(c) Frederik Trampenau 2019.</copyright>
// <author>Frederik Trampenau</author>
#pragma warning disable 0649 //Disable "default value is null" warning

using UnityEngine;

namespace MIM.InputTriggers
{
    /// <summary>
    /// Input trigger of the left swipe.
    /// </summary>
    [CreateAssetMenu(fileName = "SwipeLeftInputTrigger", menuName = "InputTrigger/ActionTriggers/SwipeLeftInputTrigger")]
    sealed class SwipeLeftInputTrigger : InputActionTrigger
    {
        #region Functions And Methods

        /// <summary>
        /// Make the constructor private to prevent multiple instances.
        /// </summary>
        private SwipeLeftInputTrigger() { }


        /// <summary>
        /// Calls base input trigger setup and subscribes to the "OnLeftSwipeDetected" action.
        /// </summary>
        /// <param name="inputReactionComponent">InputReactionField the trigger is attached to.</param>
        public override void Setup(InputReactionField inputReactionComponent)
        {
            base.Setup(inputReactionComponent);
            SwipeEvaluator.OnLeftSwipeDetected += Trigger;
        }


        /// <summary>
        /// Unsubscribes from the "OnLeftSwipeDetected" action of the SwipeEvaluator.
        /// </summary>
        public override void Shutdown() => SwipeEvaluator.OnLeftSwipeDetected -= Trigger;


        /// <summary>
        /// Calls OnActionTriggered action of the owning InputReactionField and the OnSwipeLeft action MobileInput class.
        /// </summary>
        public override void Trigger()
        {
            owningInputReactionFields.ForEach(reactionField => reactionField.OnActionTriggered?.Invoke());
            MobileInput.OnSwipeLeft?.Invoke();
        }

        #endregion Functions And Methods
    }
}