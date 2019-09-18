// <copyright>(c) Frederik Trampenau 2019.</copyright>
// <author>Frederik Trampenau</author>
#pragma warning disable 0649 //Disable "default value is null" warning

using MIM.InputTriggerArgs;
using MIM.Interfaces;
using System;
using System.Linq;

namespace MIM.InputTriggers
{
    /// <summary>
    /// Evaluates touch data regarding taps.
    /// </summary>
    sealed class TapEvaluator
    {
        #region Properties

        /// <summary>
        /// Gets called when a tap ends.
        /// </summary>
        public static Action<IPositionalInputArgs> OnTapDetected;

        #endregion Properties



        #region Functions And Methods

        /// <summary>
        /// Make the constructor private to prevent multiple instances.
        /// </summary>
        private TapEvaluator() { }


        /// <summary>
        /// Evaluates the history of a touch input.
        /// </summary>
        /// <param name="history">History of touch positions and their timestamps.</param>
        public static void EvaluateEndedTouchHistory(TouchHistory history)
        {
            if (IsTapValid(history))
                OnTapDetected?.Invoke(CreateTapTriggerArgs(history));
        }


        /// <summary>
        /// Checks if a tap was valid and should get registered.
        /// </summary>
        /// <param name="history">History of touch positions and their timestamps.</param>
        /// <returns>Should the tap get registered as a tap?</returns>
        private static bool IsTapValid(TouchHistory history)
        {
            var startTime = history.trace.First().Key;
            var endTime = history.trace.Last().Key;
            var duration = endTime - startTime;

            return duration > MobileInputModule.LoadSettings().minTapDuration &&
                    duration < MobileInputModule.LoadSettings().maxTapDuration;
        }


        /// <summary>
        /// Creates arguments for the tap by evaluating the touch history.
        /// </summary>
        /// <param name="history">History of touch positions and their timestamps.</param>
        /// <returns>Event arguments created with given touch history.</returns>
        private static TapTriggerArgs CreateTapTriggerArgs(TouchHistory history)
        {
            var position = history.trace.Last().Value;
            return new TapTriggerArgs(position, position);
        }

        #endregion Functions And Methods
    }
}