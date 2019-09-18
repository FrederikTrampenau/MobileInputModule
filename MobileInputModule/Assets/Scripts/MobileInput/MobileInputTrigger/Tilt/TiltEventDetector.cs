// <copyright>(c) Frederik Trampenau 2019.</copyright>
// <author>Frederik Trampenau</author>
#pragma warning disable 0649 //Disable "default value is null" warning

using UnityEngine;

namespace MIM.InputTriggers
{
    /// <summary>
    /// Facade which sets up the tilt evaluator and calls the evaluation of ongoinf tilts.
    /// </summary>
    public class TiltEventDetector
    {
        #region Properties

        /// <summary>
        /// Loaded input settings from the mobile input settings module.
        /// </summary>
        private static MobileInputSettingsData loadedSettingsData;

        #endregion Properties



        #region Functions And Methods

        /// <summary>
        /// Make the constructor private to prevent multiple input instances.
        /// </summary>
        private TiltEventDetector() { }


        /// <summary>
        /// Set up by setting the loaded settings data and calling the setup of the TiltEvaluator.
        /// </summary>
        /// <param name="p_loadedSettingsData">Loaded mobile input module settings data.</param>
        public static void Setup(MobileInputSettingsData p_loadedSettingsData)
        {
            loadedSettingsData = p_loadedSettingsData;
            TiltEvaluator.Setup(loadedSettingsData);
        }


        /// <summary>
        /// Invoke action that an ongoing swipe got detected.
        /// </summary>
        /// <param name="acceleration">Device acceleration which should be evaluated.</param>
        public static void CallOngoingTiltEvaluation(Vector2 acceleration) => TiltEvaluator.EvaluateOngoingTilt(acceleration);

        #endregion Functions And Methods
    }
}