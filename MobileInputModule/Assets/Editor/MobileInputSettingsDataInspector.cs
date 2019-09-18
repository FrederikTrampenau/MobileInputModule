// <copyright>(c) Frederik Trampenau 2019.</copyright>
// <author>Frederik Trampenau</author>
#pragma warning disable 0649 //Disable "default value is null" warning

using UnityEditor;

namespace MIM.Editors
{
    /// <summary>
    /// Custom editor class for the MobileInputSettingsData.
    /// </summary>
    [CustomEditor(typeof(MobileInputSettingsData)), CanEditMultipleObjects]
    public class MobileInputSettingsDataInspector : Editor
    {
        #region Serialized Fields

        /// <summary>
        /// Serialized property of the minimal needed swipe length (in percentage of screen) to call a horizontal swipe event.
        /// </summary>
        private SerializedProperty minHorizontalSwipeLength;

        /// <summary>
        /// Serialized property of the minimal needed swipe length (in percentage of screen) to call a vertical swipe event.
        /// </summary>
        private SerializedProperty minVerticalSwipeLength;

        /// <summary>
        /// Serialized property of the minimal needed tap duration (in seconds) to call a tap event.
        /// </summary>
        private SerializedProperty minTapDuration;

        /// <summary>
        /// Serialized property of the maximal tap duration (in seconds) to call a tap event.
        /// </summary>
        private SerializedProperty maxTapDuration;

        /// <summary>
        /// Serialized property of the minimal needed positive tilt on the x-axis to call a positive x-axis (right) tilt event.
        /// </summary>
        private SerializedProperty xTiltPosThreshold;

        /// <summary>
        /// Serialized property of the minimal needed negative tilt on the y-axis to call a negative x-axis (left) tilt event.
        /// </summary>
        private SerializedProperty xTiltNegThreshold;

        /// <summary>
        /// Serialized property of the minimal needed positive tilt on the y-axis to call a positive y-axis (forward) tilt event.
        /// </summary>
        private SerializedProperty yTiltPosThreshold;

        /// <summary>
        /// Serialized property of the minimal needed negative tilt on the y-axis to call a neagative y-axis (backward) tilt event.
        /// </summary>
        private SerializedProperty yTiltNegThreshold;

        #endregion Serialized Fields



        #region Unity Functions

        /// <summary>
        /// Set up serialized properties.
        /// </summary>
        private void OnEnable() => SetupSerilizedProperties();

        #endregion Unity Functions



        #region Functions And Methods

        /// <summary>
        /// Make the constructor private to prevent multiple instances.
        /// </summary>
        private MobileInputSettingsDataInspector() { }


        /// <summary>
        /// Set serialized properties by getting them from the serialized object.
        /// </summary>
        private void SetupSerilizedProperties()
        {
            minHorizontalSwipeLength = serializedObject.FindProperty("minHorizontalSwipeLength");
            minVerticalSwipeLength = serializedObject.FindProperty("minVerticalSwipeLength");

            minTapDuration = serializedObject.FindProperty("minTapDuration");
            maxTapDuration = serializedObject.FindProperty("maxTapDuration");

            xTiltPosThreshold = serializedObject.FindProperty("xTiltPosThreshold");
            xTiltNegThreshold = serializedObject.FindProperty("xTiltNegThreshold");
            yTiltPosThreshold = serializedObject.FindProperty("yTiltPosThreshold");
            yTiltNegThreshold = serializedObject.FindProperty("yTiltNegThreshold");
        }


        /// <summary>
        /// Update inspector content.
        /// </summary>
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            CreateSwipeDataGUI();

            GUIFactory.AddGUISpace(3);

            CreateTapDataGUI();

            GUIFactory.AddGUISpace(3);

            CreateTiltDataGUI();

            serializedObject.ApplyModifiedProperties();
        }


        /// <summary>
        /// Creates GUI elements for swipe settings data.
        /// </summary>
        private void CreateSwipeDataGUI()
        {
            EditorGUILayout.BeginVertical("HelpBox");

            EditorGUILayout.LabelField("Swipe Settings", EditorStyles.boldLabel);

            EditorGUILayout.LabelField("Min Horizontal Swipe Length (in % of screen)", EditorStyles.largeLabel);
            minHorizontalSwipeLength.floatValue = GUIFactory.CreateFloatFieldGUI(minHorizontalSwipeLength.floatValue,
                                                                                    0.0f,
                                                                                    100.0f);

            GUIFactory.AddGUISpace(1);

            EditorGUILayout.LabelField("Min Vertical Swipe Length (in % of screen)", EditorStyles.largeLabel);
            minVerticalSwipeLength.floatValue = GUIFactory.CreateFloatFieldGUI(minVerticalSwipeLength.floatValue,
                                                                                0.0f,
                                                                                100.0f);

            EditorGUILayout.EndVertical();
        }


        /// <summary>
        /// Creates GUI elements for tap settings data.
        /// </summary>
        private void CreateTapDataGUI()
        {
            EditorGUILayout.BeginVertical("HelpBox");

            EditorGUILayout.LabelField("Tap Settings", EditorStyles.boldLabel);

            EditorGUILayout.LabelField("Min Tap Duration In Seconds", EditorStyles.largeLabel);
            minTapDuration.floatValue = GUIFactory.CreateFloatFieldGUI(minTapDuration.floatValue,
                                                                        0.0f,
                                                                        (float)double.PositiveInfinity);

            GUIFactory.AddGUISpace(1);

            EditorGUILayout.LabelField("Max Tap Duration In Seconds", EditorStyles.largeLabel);
            maxTapDuration.floatValue = GUIFactory.CreateFloatFieldGUI(maxTapDuration.floatValue,
                                                                        0.0f,
                                                                        (float)double.PositiveInfinity);

            EditorGUILayout.EndVertical();
        }


        /// <summary>
        /// Creates GUI elements for tilt settings data.
        /// </summary>
        private void CreateTiltDataGUI()
        {
            EditorGUILayout.BeginVertical("HelpBox");

            EditorGUILayout.LabelField("Tilt Settings", EditorStyles.boldLabel);

            EditorGUILayout.LabelField("Min Positive Needed X-Tilt (right)", EditorStyles.largeLabel);
            xTiltPosThreshold.floatValue = GUIFactory.CreateFloatFieldGUI(xTiltPosThreshold.floatValue,
                                                                            0.0f,
                                                                            1.0f);

            GUIFactory.AddGUISpace(1);

            EditorGUILayout.LabelField("Min Negative Needed X-Tilt (left)", EditorStyles.largeLabel);
            xTiltNegThreshold.floatValue = GUIFactory.CreateFloatFieldGUI(xTiltNegThreshold.floatValue,
                                                                            -1.0f,
                                                                            0.0f);


            GUIFactory.AddGUISpace(1);


            EditorGUILayout.LabelField("Min Positive Needed Y-Tilt (forward)", EditorStyles.largeLabel);
            yTiltPosThreshold.floatValue = GUIFactory.CreateFloatFieldGUI(yTiltPosThreshold.floatValue,
                                                                            0.0f,
                                                                            1.0f);

            GUIFactory.AddGUISpace(1);

            EditorGUILayout.LabelField("Min Negative Needed Y-Tilt (backward)", EditorStyles.largeLabel);
            yTiltNegThreshold.floatValue = GUIFactory.CreateFloatFieldGUI(yTiltNegThreshold.floatValue,
                                                                            -1.0f,
                                                                            0.0f);

            EditorGUILayout.EndVertical();
        }

        #endregion Functions And Methods
    }
}