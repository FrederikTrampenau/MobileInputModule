// <copyright>(c) Frederik Trampenau 2019.</copyright>
// <author>Frederik Trampenau</author>
#pragma warning disable 0649 //Disable "default value is null" warning

namespace MIM.InputTriggers
{
    /// <summary>
    /// Facade which calls the evaluation of a tap.
    /// </summary>
    sealed class TapEventDetector
    {
        #region Functions And Methods

        /// <summary>
        /// Make the constructor private to prevent multiple instances.
        /// </summary>
        private TapEventDetector() { }

        /// <summary>
        /// Invokes evaluation of the given touch history.
        /// </summary>
        /// <param name="history">History of touch positions.</param>
        public static void CallTouchHistoryEvaluation(TouchHistory history) => TapEvaluator.EvaluateEndedTouchHistory(history);

        #endregion Functions And Methods
    }
}