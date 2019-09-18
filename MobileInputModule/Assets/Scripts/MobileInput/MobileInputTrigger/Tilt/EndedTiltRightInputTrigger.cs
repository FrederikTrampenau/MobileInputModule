// <copyright>(c) Frederik Trampenau 2019.</copyright>
// <author>Frederik Trampenau</author>
#pragma warning disable 0649 //Disable "default value is null" warning

using UnityEngine;

namespace MIM.InputTriggers
{
    /// <summary>
    /// Input trigger of the ended right tilt.
    /// </summary>
    [CreateAssetMenu(fileName = "EndedTiltRightInputTrigger", menuName = "InputTrigger/ActionTriggers/EndedTiltRightInputTrigger")]
    sealed class EndedTiltRightInputTrigger : InputActionTrigger
    {
        #region Functions And Methods

        /// <summary>
        /// Make the constructor private to prevent multiple instances.
        /// </summary>
        private EndedTiltRightInputTrigger() { }


        /// <summary>
        /// Calls base input trigger setup and subscribes to the "OnEndedTiltRight" action.
        /// </summary>
        /// <param name="inputReactionComponent">InputReactionField the trigger is attached to.</param>
        public override void Setup(InputReactionField inputReactionComponent)
        {
            base.Setup(inputReactionComponent);
            TiltEvaluator.OnEndedTiltRight += Trigger;
        }


        /// <summary>
        /// Unsubscribes from the "OnEndedTiltRight" action of the TiltEvalutator.
        /// </summary>
        public override void Shutdown() => TiltEvaluator.OnEndedTiltRight -= Trigger;


        /// <summary>
        /// Calls OnActionTriggered action of the owning InputReactionField and the OnEndedTiltRight action of the MobileInput class.
        /// </summary>
        public override void Trigger()
        {
            owningInputReactionFields.ForEach(reactionField => reactionField.OnActionTriggered?.Invoke());
            MobileInput.OnEndedTiltRight?.Invoke();
        }

        #endregion Functions And Methods
    }
}