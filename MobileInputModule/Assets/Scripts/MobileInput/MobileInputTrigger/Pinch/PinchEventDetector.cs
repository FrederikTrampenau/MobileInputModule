// <copyright>(c) Frederik Trampenau 2019.</copyright>
// <author>Frederik Trampenau</author>
#pragma warning disable 0649 //Disable "default value is null" warning

using UnityEngine;

namespace MIM.InputTriggers
{
    /// <summary>
    /// Facade which calls the evaluation of a pinch.
    /// </summary>
    sealed public class PinchEventDetector
    {
        #region Functions And Methods

        /// <summary>
        /// Make the constructor private to prevent multiple input instances.
        /// </summary>
        private PinchEventDetector() { }


        /// <summary>
        /// Invoke action that an ongoing swipe got detected.
        /// </summary>
        /// <param name="touches">Tuple of the first and second finger touch .</param>
        public static void CallOngoingPinchEvaluation((Touch, Touch) touches) => PinchEvaluator.EvaluateOngoingPinch(touches.Item1, touches.Item2);

        #endregion Functions And Methods
    }
}