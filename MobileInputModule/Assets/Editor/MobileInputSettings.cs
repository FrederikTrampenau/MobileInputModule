// <copyright>(c) Frederik Trampenau 2019.</copyright>
// <author>Frederik Trampenau</author>
#pragma warning disable 0649 //Disable "default value is null" warning

using UnityEditor;
using UnityEngine;

namespace MIM.Editors
{
    /// <summary>
    /// Editor window of the mobile input settings.
    /// </summary>
    public class MobileInputSettings : EditorWindow
    {
        #region Serialized Fields

        /// <summary>
        /// Loaded mobile input settings data.
        /// </summary>
        [SerializeField]
        private static MobileInputSettingsData inputSettings;

        #endregion Serialized Fields



        #region Private Fields

        /// <summary>
        /// Min size of the mobile settings window.
        /// </summary>
        private static Vector2 windowMinSize = new Vector2(400.0f, 600.0f);

        /// <summary>
        /// Scroll position in the mobile settings window.
        /// </summary>
        private static Vector2 scrollPosition;

        #endregion Private Fields



        #region Properties

        /// <summary>
        /// Gets the stored mobile input settings.
        /// </summary>
        /// <returns></returns>
        public static MobileInputSettingsData GetInputSettings() => inputSettings;


        /// <summary>
        /// Loads the mobile input settings asset in the resources.
        /// </summary>
        /// <returns>Loaded mobile input settings.</returns>
        private static MobileInputSettingsData LoadSettingsAsset()
        {
            // Load all resources before searching to be able to find the settings
            Resources.LoadAll(string.Empty, typeof(MobileInputSettingsData));
            var foundSettings = Resources.FindObjectsOfTypeAll<MobileInputSettingsData>();

            return foundSettings.Length > 0 ? foundSettings[0] : null;
        }


        /// <summary>
        /// Checks if the user added settings data.
        /// </summary>
        /// <returns>Does input settings data exist?</returns>
        private bool HasSettingsData() => inputSettings;

        #endregion Properties



        #region Unity Functions

        /// <summary>
        /// Get existing open window or if none, make a new one.
        /// </summary>
        [MenuItem("Window/Mobile Input Settings")]
        static void Init()
        {
            inputSettings = null;

            MobileInputSettings window = GetWindow<MobileInputSettings>("Mobile Settings", true, typeof(EditorWindow));
            window.minSize = windowMinSize;
            window.Show();
        }


        /// <summary>
        /// Process values from the editor and update the window.
        /// </summary>
        void OnGUI()
        {
            inputSettings = LoadSettingsAsset();

            if (!HasSettingsData())
            {
                GUILayout.Label("No input settings instance found in the resources folder, add one!", EditorStyles.boldLabel);
                return;
            }

            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

            CreateSwipeSettingsGUI();

            GUIFactory.AddGUISpace(3);

            CreateTapSettingsGUI();

            GUIFactory.AddGUISpace(3);

            CreateTiltSettingsGUI();

            EditorGUILayout.EndScrollView();

            SaveSettings();
        }

        #endregion Unity Functions



        #region Functions And Methods

        /// <summary>
        /// Creates the GUI of the swipe settings.
        /// </summary>
        private static void CreateSwipeSettingsGUI()
        {
            GUILayout.BeginVertical("HelpBox");
            GUILayout.Label("Swipe Settings", EditorStyles.boldLabel);


            // Min horizontal swipe length slider
            GUILayout.BeginVertical("GroupBox");

            GUIFactory.CreateSlider("Min Horizontal Swipe Length (Completed Swipes)",
                                    EditorStyles.largeLabel,
                                    "Needed horizontal swipe length to register a swipe. Measured in screen percent",
                                    ref inputSettings.minHorizontalSwipeLength,
                                    0.0f,
                                    100.0f);

            GUILayout.EndVertical();


            // Min vertical swipe length slider
            GUILayout.BeginVertical("GroupBox");

            GUIFactory.CreateSlider("Min Vertical Swipe Length (Completed Swipes)",
                                    EditorStyles.largeLabel,
                                    "Needed vertical swipe length to register a swipe. Measured in screen percent",
                                    ref inputSettings.minVerticalSwipeLength,
                                    0.0f,
                                    100.0f);

            GUILayout.EndVertical();

            GUILayout.EndVertical();
        }


        /// <summary>
        /// Creates the GUI of the tap settings.
        /// </summary>
        private static void CreateTapSettingsGUI()
        {
            GUILayout.BeginVertical("HelpBox");
            GUILayout.Label("Tap Settings", EditorStyles.boldLabel);


            // Min tap duration
            GUILayout.BeginVertical("GroupBox");

            EditorGUILayout.LabelField("Min Tap Duration In Seconds", EditorStyles.largeLabel);
            inputSettings.minTapDuration = GUIFactory.CreateFloatFieldGUI(inputSettings.minTapDuration,
                                                                            0.0f,
                                                                            (float)double.PositiveInfinity);

            GUILayout.EndVertical();


            // Max tap duration
            GUILayout.BeginVertical("GroupBox");

            EditorGUILayout.LabelField("Max Tap Duration In Seconds", EditorStyles.largeLabel);
            inputSettings.maxTapDuration = GUIFactory.CreateFloatFieldGUI(inputSettings.maxTapDuration,
                                                                            0.0f,
                                                                            (float)double.PositiveInfinity);

            GUILayout.EndVertical();


            GUILayout.EndVertical();
        }


        /// <summary>
        /// Creates the GUI of the tilt settings.
        /// </summary>
        private static void CreateTiltSettingsGUI()
        {
            GUILayout.BeginVertical("HelpBox");
            GUILayout.Label("Tilt Settings", EditorStyles.boldLabel);


            // Pos x-tilt treshold
            GUILayout.BeginVertical("GroupBox");

            EditorGUILayout.LabelField("Min Positive Needed X-Tilt (right) Treshold", EditorStyles.largeLabel);
            inputSettings.xTiltPosThreshold = GUIFactory.CreateFloatFieldGUI(inputSettings.xTiltPosThreshold,
                                                                                -1.0f,
                                                                                1.0f);

            GUILayout.EndVertical();


            // Neg x-tilt treshold
            GUILayout.BeginVertical("GroupBox");

            EditorGUILayout.LabelField("Min Negative Needed X-Tilt (left) Treshold", EditorStyles.largeLabel);
            inputSettings.xTiltNegThreshold = GUIFactory.CreateFloatFieldGUI(inputSettings.xTiltNegThreshold,
                                                                                -1.0f,
                                                                                1.0f);

            GUILayout.EndVertical();


            // Pos y-tilt treshold
            GUILayout.BeginVertical("GroupBox");

            EditorGUILayout.LabelField("Min Positive Needed Y-Tilt (forward) Treshold", EditorStyles.largeLabel);
            inputSettings.yTiltPosThreshold = GUIFactory.CreateFloatFieldGUI(inputSettings.yTiltPosThreshold,
                                                                                -1.0f,
                                                                                1.0f);

            GUILayout.EndVertical();


            // Neg y-tilt treshold
            GUILayout.BeginVertical("GroupBox");

            EditorGUILayout.LabelField("Min Negative Needed Y-Tilt (backward) Treshold", EditorStyles.largeLabel);
            inputSettings.yTiltNegThreshold = GUIFactory.CreateFloatFieldGUI(inputSettings.yTiltNegThreshold,
                                                                                -1.0f,
                                                                                1.0f);

            GUILayout.EndVertical();

            GUILayout.EndVertical();
        }


        /// <summary>
        /// Saves settings by overriding the scriptable object data.
        /// </summary>
        private static void SaveSettings()
        {
            var foundSettings = Resources.FindObjectsOfTypeAll<MobileInputSettingsData>();

            if (foundSettings.Length > 0 && inputSettings)
                foundSettings[0].OverrideData(inputSettings);
        }

        #endregion Functions And Methods
    }
}