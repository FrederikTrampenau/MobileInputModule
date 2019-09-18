// <copyright>(c) Frederik Trampenau 2019.</copyright>
// <author>Frederik Trampenau</author>
#pragma warning disable 0649 //Disable "default value is null" warning

using UnityEngine;

namespace MIM.InputTriggers
{
    /// <summary>
    /// Input trigger of the ended forward tilt.
    /// </summary>
    [CreateAssetMenu(fileName = "EndedTiltForwardInputTrigger", menuName = "InputTrigger/ActionTriggers/EndedTiltForwardInputTrigger")]
    sealed class EndedTiltForwardInputTrigger : InputActionTrigger
    {
        #region Functions And Methods

        /// <summary>
        /// Make the constructor private to prevent multiple instances.
        /// </summary>
        private EndedTiltForwardInputTrigger() { }


        /// <summary>
        /// Calls base input trigger setup and subscribes to the "OnEndedTiltForward" action.
        /// </summary>
        /// <param name="inputReactionComponent">InputReactionField the trigger is attached to.</param>
        public override void Setup(InputReactionField inputReactionComponent)
        {
            base.Setup(inputReactionComponent);
            TiltEvaluator.OnEndedTiltForward += Trigger;
        }


        /// <summary>
        /// Unsubscribes from the "OnEndedTiltForward" action of the TiltEvalutator.
        /// </summary>
        public override void Shutdown() => TiltEvaluator.OnEndedTiltForward -= Trigger;


        /// <summary>
        /// Calls OnActionTriggered action of the owning InputReactionField and the OnEndedTiltForward action of the MobileInput class.
        /// </summary>
        public override void Trigger()
        {
            owningInputReactionFields.ForEach(reactionField => reactionField.OnActionTriggered?.Invoke());

            MobileInput.OnEndedTiltForward?.Invoke();
        }

        #endregion Functions And Methods
    }
}