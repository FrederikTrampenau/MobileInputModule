// <copyright>(c) Frederik Trampenau 2019.</copyright>
// <author>Frederik Trampenau</author>
#pragma warning disable 0649 //Disable "default value is null" warning

using UnityEngine;

namespace MIM.InputTriggers
{
    /// <summary>
    /// Input trigger of the down swipe.
    /// </summary>
    [CreateAssetMenu(fileName = "SwipeDownInputTrigger", menuName = "InputTrigger/ActionTriggers/SwipeDownInputTrigger")]
    sealed class SwipeDownInputTrigger : InputActionTrigger
    {
        #region Functions And Methods

        /// <summary>
        /// Make the constructor private to prevent multiple instances.
        /// </summary>
        private SwipeDownInputTrigger() { }


        /// <summary>
        /// Calls base input trigger setup and subscribes to the "OnDownSwipeDetected" action of the SwipeEvaluator.
        /// </summary>
        /// <param name="inputReactionComponent">InputReactionField the trigger is attached to.</param>
        public override void Setup(InputReactionField inputReactionComponent)
        {
            base.Setup(inputReactionComponent);
            SwipeEvaluator.OnDownSwipeDetected += Trigger;
        }


        /// <summary>
        /// Unsubscribes from the "OnDownSwipeDetected" action of the SwipeEvaluator.
        /// </summary>
        public override void Shutdown() => SwipeEvaluator.OnDownSwipeDetected -= Trigger;


        /// <summary>
        /// Calls OnSwipeDown action of the owning InputReactionField and the MobileInput class.
        /// </summary>
        public override void Trigger()
        {
            owningInputReactionFields.ForEach(reactionField => reactionField.OnActionTriggered?.Invoke());
            MobileInput.OnSwipeDown?.Invoke();
        }

        #endregion Functions And Methods
    }
}