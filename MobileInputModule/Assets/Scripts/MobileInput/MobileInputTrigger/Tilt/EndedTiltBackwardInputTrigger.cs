// <copyright>(c) Frederik Trampenau 2019.</copyright>
// <author>Frederik Trampenau</author>
#pragma warning disable 0649 //Disable "default value is null" warning

using UnityEngine;

namespace MIM.InputTriggers
{
    /// <summary>
    /// Input trugger of the ended backward tilt.
    /// </summary>
    [CreateAssetMenu(fileName = "EndedTiltBackwardInputTrigger", menuName = "InputTrigger/ActionTriggers/EndedTiltBackwardInputTrigger")]
    sealed class EndedTiltBackwardInputTrigger : InputActionTrigger
    {
        #region Functions And Methods

        /// <summary>
        /// Make the constructor private to prevent multiple instances.
        /// </summary>
        private EndedTiltBackwardInputTrigger() { }


        /// <summary>
        /// Calls base input trigger setup and subscribes to the "OnEndedTiltBackward" action.
        /// </summary>
        /// <param name="inputReactionComponent">InputReactionField the trigger is attached to.</param>
        public override void Setup(InputReactionField inputReactionComponent)
        {
            base.Setup(inputReactionComponent);
            TiltEvaluator.OnEndedTiltBackward += Trigger;
        }


        /// <summary>
        /// Unsubscribes from the "OnEndedTiltForward" action of the TiltEvalutator.
        /// </summary>
        public override void Shutdown() => TiltEvaluator.OnEndedTiltBackward -= Trigger;


        /// <summary>
        /// Calls OnActionTriggered action of the owning InputReactionField and the OnEndedTiltBackward action of the MobileInput class.
        /// </summary>
        public override void Trigger()
        {
            owningInputReactionFields.ForEach(reactionField => reactionField.OnActionTriggered?.Invoke());

            MobileInput.OnEndedTiltBackward?.Invoke();
        }

        #endregion Functions And Methods
    }
}