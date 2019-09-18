// <copyright>(c) Frederik Trampenau 2019.</copyright>
// <author>Frederik Trampenau</author>
#pragma warning disable 0649 //Disable "default value is null" warning

using MIM.Interfaces;
using System;
using UnityEngine;

namespace MIM.InputTriggers
{
    /// <summary>
    /// Calls that an ongoing swipe is happening and invokes its evaluation if it ended.
    /// </summary>
    sealed class SwipeEventDetector
    {
        #region Properties

        /// <summary>
        /// Gets called when an ongoing swipe gets detected.
        /// </summary>
        public static Action<IPositionalInputArgs> OnSwipeDetected;

        #endregion Properties



        #region Functions And Methods

        /// <summary>
        /// Make the constructor private to prevent multiple instances.
        /// </summary>
        private SwipeEventDetector() { }


        /// <summary>
        /// Invoke action that an ongoing swipe got detected.
        /// </summary>
        /// <param name="touch">Information of the swiping touch.</param>
        public static void CallOngoingSwipe(Touch touch)
        {
            // Simulate touch history to create ongoing swipe arguments.
            var history = new TouchHistory(0f, touch.position);

            if (history.trace.ContainsKey(0f))
                history.trace.Remove(0f);

            history.trace.Add(0f, touch.position - touch.deltaPosition);
            history.trace.Add(touch.deltaTime, touch.position);

            var swipeArgs = SwipeEvaluator.CreateSwipeTriggerArgs(history);

            MobileInput.OnSwipeMoving?.Invoke(swipeArgs);
            OnSwipeDetected?.Invoke(swipeArgs);
        }


        /// <summary>
        /// Invokes evaluation of the given touch history if there is more than one element.
        /// </summary>
        /// <param name="history">History of touch positions.</param>
        public static void CallTouchHistoryEvaluation(TouchHistory history)
        {
            if (history.trace.Count <= 1) return;

            SwipeEvaluator.EvaluateEndedTouchHistory(history);
        }

        #endregion Functions And Methods
    }
}