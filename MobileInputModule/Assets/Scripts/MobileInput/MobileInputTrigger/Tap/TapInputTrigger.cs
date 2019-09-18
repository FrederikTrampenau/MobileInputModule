// <copyright>(c) Frederik Trampenau 2019.</copyright>
// <author>Frederik Trampenau</author>
#pragma warning disable 0649 //Disable "default value is null" warning

using MIM.Interfaces;
using UnityEngine;

namespace MIM.InputTriggers
{
    /// <summary>
    /// Input trigger for the tap.
    /// </summary>
    [CreateAssetMenu(fileName = "TapInputTrigger", menuName = "InputTrigger/ArgumentTriggers/TapInputTrigger")]
    public class TapInputTrigger : InputArgsTrigger
    {
        #region Functions And Methods

        /// <summary>
        /// Make the constructor private to prevent multiple instances.
        /// </summary>
        private TapInputTrigger() { }


        /// <summary>
        /// Calls base input trigger setup and subscribes to the "OnTapDetected" action.
        /// </summary>
        /// <param name="inputReactionComponent">InputReactionField the trigger is attached to.</param>
        public override void Setup(InputReactionField inputReactionComponent)
        {
            base.Setup(inputReactionComponent);
            TapEvaluator.OnTapDetected += Trigger;
        }


        /// <summary>
        /// Unsubscribes from the "OnTapDetected" action of the SwipeEvaluator.
        /// </summary>
        public override void Shutdown() => TapEvaluator.OnTapDetected -= Trigger;


        /// <summary>
        /// Calls OnArgumentInputTriggered action of the owning InputReactionField and OnTap action of the MobileInput class.
        /// </summary>
        public override void Trigger(IPositionalInputArgs args)
        {
            owningInputReactionFields.ForEach(reactionField => reactionField.OnArgumentInputTriggered?.Invoke(args));
            MobileInput.OnTap?.Invoke(args);
        }

        #endregion Functions And Methods
    }
}