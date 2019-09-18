// <copyright>(c) Frederik Trampenau 2019.</copyright>
// <author>Frederik Trampenau</author>
#pragma warning disable 0649 //Disable "default value is null" warning

using UnityEngine;

namespace MIM.InputTriggers
{
    /// <summary>
    /// Input trigger of the ended left tilt.
    /// </summary>
    [CreateAssetMenu(fileName = "EndedTiltLeftInputTrigger", menuName = "InputTrigger/ActionTriggers/EndedTiltLeftInputTrigger")]
    sealed class EndedTiltLeftInputTrigger : InputActionTrigger
    {
        #region Functions And Methods

        /// <summary>
        /// Make the constructor private to prevent multiple instances.
        /// </summary>
        private EndedTiltLeftInputTrigger() { }


        /// <summary>
        /// Calls base input trigger setup and subscribes to the "OnEndedLeftTilt" action.
        /// </summary>
        /// <param name="inputReactionComponent">InputReactionField the trigger is attached to.</param>
        public override void Setup(InputReactionField inputReactionComponent)
        {
            base.Setup(inputReactionComponent);
            TiltEvaluator.OnEndedTiltLeft += Trigger;
        }


        /// <summary>
        /// Unsubscribes from the "OnEndedLeftTilt" action of the TiltEvalutator.
        /// </summary>
        public override void Shutdown() => TiltEvaluator.OnEndedTiltLeft -= Trigger;


        /// <summary>
        /// Calls OnArgumentInputTriggered action of the owning InputReactionField and the OnEndedTiltLeft action of the MobileInput class.
        /// </summary>
        public override void Trigger()
        {
            owningInputReactionFields.ForEach(reactionField => reactionField.OnActionTriggered?.Invoke());
            MobileInput.OnEndedTiltLeft?.Invoke();
        }

        #endregion Functions And Methods
    }
}