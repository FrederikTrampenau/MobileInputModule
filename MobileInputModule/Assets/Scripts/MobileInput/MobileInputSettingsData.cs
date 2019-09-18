// <copyright>(c) Frederik Trampenau 2019.</copyright>
// <author>Frederik Trampenau</author>
#pragma warning disable 0649 //Disable "default value is null" warning

using UnityEditor;
using UnityEngine;

namespace MIM
{
    /// <summary>
    /// Settings for the mobile input module.
    /// </summary>
    public class MobileInputSettingsData : ScriptableObject
    {
        #region Public Fields

            // Swipe Data
            #region Swipe Data

            /// <summary>
            /// Minimal needed swipe length (in percentage of screen) to call a horizontal swipe event.
            /// </summary>
            [Header("Swipe input settings")]
            [Tooltip("Minimal needed swipe length (in percentage of screen) to call a horizontal swipe event.")]
            [Range(0.0f, 100.0f)]
            public float minHorizontalSwipeLength = 0.0f;

            /// <summary>
            /// Minimal needed swipe length (in percentage of screen) to call a vertical swipe event.
            /// </summary>
            [Tooltip("Minimal needed swipe length (in percentage of screen) to call a vertical swipe event.")]
            [Range(0.0f, 100.0f)]
            public float minVerticalSwipeLength = 0.0f;

            #endregion Swipe Data

            // Tap Data
            #region Tap Data

            /// <summary>
            /// Minimal needed tap duration (in seconds) to call a tap event.
            /// </summary>
            [Header("Tap input settings")]
            [Tooltip("Minimal needed tap duration (in seconds) to call a tap event.")]
            [Min(0.0f)]
            public float minTapDuration = 0.0f;

            /// <summary>
            /// Maximal tap duration (in seconds) to call a tap event.
            /// </summary>
            [Tooltip("Maximal tap duration (in seconds) to call a tap event.")]
            [Min(0.0f)]
            public float maxTapDuration = (float)double.MaxValue;
            #endregion Tap Data

            // Tilt Data
            #region Tilt Data

            /// <summary>
            /// Minimal needed positive tilt on the x-axis to call a positive x-axis (right) tilt event.
            /// </summary>
            [Header("Tilt input settings")]
            [Tooltip("Minimal needed positive tilt on the x-axis to call a positive x-axis (right) tilt event.")]
            [Range(-1.0f, 1.0f)]
            public float xTiltPosThreshold = 0.4f;

            /// <summary>
            /// Minimal needed negative tilt on the y-axis to call a negative x-axis (left) tilt event.
            /// </summary>
            [Tooltip("Minimal needed negative tilt on the y-axis to call a negative x-axis (left) tilt event.")]
            [Range(-1.0f, 1.0f)]
            public float xTiltNegThreshold = -0.4f;

            /// <summary>
            /// Minimal needed positive tilt on the y-axis to call a positive y-axis (forward) tilt event.
            /// </summary>
            [Tooltip("Minimal needed positive tilt on the y-axis to call a positive y-axis (forward) tilt event.")]
            [Range(-1.0f, 1.0f)]
            public float yTiltPosThreshold = 0.4f;

            /// <summary>
            /// Minimal needed negative tilt on the y-axis to call a neagative y-axis (backward) tilt event.
            /// </summary>
            [Tooltip("Minimal needed negative tilt on the y-axis to call a neagative y-axis (backward) tilt event.")]
            [Range(-1.0f, 1.0f)]
            public float yTiltNegThreshold = -0.4f;

        #endregion Tilt Data

        #endregion Public Fields



        #region Properties

        /// <summary>
        /// Name of the settings asset.
        /// </summary>
        public static string SettingsFileName { get; private set; } = "MobileInputSettingsData";

        #endregion Properties



    #if UNITY_EDITOR

        #region Unity Functions

        /// <summary>
        /// Calls setup of the mobile input settings data.
        /// </summary>
        private void OnEnable() => Setup();

        #endregion Unity Functions



        #region Functions And Methods

        /// <summary>
        /// Overrides the existing data settings with given ones.
        /// </summary>
        /// <param name="newSettingsData">Settings data to override with.</param>
        public void OverrideData(MobileInputSettingsData newSettingsData)
        {
            minHorizontalSwipeLength = newSettingsData.minHorizontalSwipeLength;
            minVerticalSwipeLength = newSettingsData.minVerticalSwipeLength;
            minTapDuration = newSettingsData.minTapDuration;
            maxTapDuration = newSettingsData.maxTapDuration;
        }



        /// <summary>
        /// Set up the asset by setting lables and setting up the "MobileInputSettingsAssetRenamer".
        /// </summary>
        private void Setup()
        {
            AssetDatabase.SetLabels(this, new[] { SettingsFileName });
            MobileInputSettingsAssetProtector.Instance.Setup(SettingsFileName);
        }

        #endregion Functions And Methods

    #endif
    }
}