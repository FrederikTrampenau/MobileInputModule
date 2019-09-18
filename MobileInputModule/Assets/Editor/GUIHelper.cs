// <copyright>(c) Frederik Trampenau 2019.</copyright>
// <author>Frederik Trampenau</author>
#pragma warning disable 0649 //Disable "default value is null" warning

using UnityEngine;
using UnityEditor;

namespace MIM.Editors
{
    /// <summary>
    /// Creates editor GUI.
    /// </summary>
    [System.Serializable]
    public static class GUIFactory
    {
        #region Functions And Methods

        /// <summary>
        /// Creates an int field gui and returns its value.
        /// </summary>
        public static int CreateIntFieldGUI(string fieldText, int property, int minVal, int maxVal) => Mathf.Clamp(EditorGUILayout.IntField(fieldText, property), minVal, maxVal);


        /// <summary>
        /// Creates an int field gui without a label and returns its value.
        /// </summary>
        public static int CreateIntFieldGUI(int property, int minVal, int maxVal) => Mathf.Clamp(EditorGUILayout.IntField(property), minVal, maxVal);

        
        /// <summary>
        /// Creates a float field gui and returns its value.
        /// </summary>
        public static float CreateFloatFieldGUI(string fieldText, float property, float minVal, float maxVal) => Mathf.Clamp(EditorGUILayout.FloatField(fieldText, property), minVal, maxVal);


        /// <summary>
        /// Creates a float field gui without a label and returns its value.
        /// </summary>
        public static float CreateFloatFieldGUI(float property, float minVal, float maxVal) => Mathf.Clamp(EditorGUILayout.FloatField(property), minVal, maxVal);


        /// <summary>
        /// Creates a toggle GUI with the given text and property.
        /// </summary>
        /// <param name="toggleLabel">Text shown next to the toggle button.</param>
        /// <param name="property">Value of the toggle.</param>
        /// <returns>New value of the toggle.</returns>
        public static bool CreateToggleGUI(string toggleLabel, bool property) => EditorGUILayout.Toggle(toggleLabel, property);


        /// <summary>
        /// Creates a GUI box with property fields for the trigger list and the unity event.
        /// </summary>
        /// <param name="fieldIterator">Iterator value for the reaction field array.</param>
        /// <param name="propertyName">Property name which gets displayed in a warning message when no trigger is set.</param>
        /// <param name="triggerListProperty">Property of the trigger list.</param>
        /// <param name="unityEventProperty">Property of the unity event.</param>
        public static void CreateReactionFieldGUI(int fieldIterator, string propertyName, SerializedProperty triggerListProperty, SerializedProperty unityEventProperty)
        {
            GUILayout.BeginVertical("GroupBox");

            EditorGUILayout.PropertyField(triggerListProperty, includeChildren: true);
            if (triggerListProperty.arraySize <= 0)
                EditorGUILayout.HelpBox($"No {propertyName} Set!", MessageType.Error);

            AddGUISpace(1);

            EditorGUILayout.PropertyField(unityEventProperty, includeChildren: true);

            GUILayout.EndVertical();
        }


        /// <summary>
        /// Creates a slider in the window which controls a value with the given parameters.
        /// </summary>
        /// <param name="label">Label above the slider.</param>
        /// <param name="labelStyle">Style of the label header.</param>
        /// <param name="helpBoxText">Text which gets displayed above the slider.</param>
        /// <param name="settingsValue">Reference to the value that gets controller.</param>
        /// <param name="minValue">Min value of the slider.</param>
        /// <param name="maxValue">Max value of the slider.</param>
        public static void CreateSlider(string label, GUIStyle labelStyle, string helpBoxText, ref float settingsValue, float minValue, float maxValue)
        {
            GUILayout.Label(label, labelStyle);
            EditorGUILayout.HelpBox(helpBoxText, MessageType.None, true);
            settingsValue = EditorGUILayout.Slider(settingsValue, minValue, maxValue, GetDefaultSliderLayout());
        }


        /// <summary>
        /// Adds amount of GUI space to the editor by the given amount.
        /// </summary>
        /// <param name="amount">Amount of added space.</param>
        public static void AddGUISpace(int amount)
        {
            for (int i = 0; i < amount; i++)
                EditorGUILayout.Space();
        }


        /// <summary>
        /// Gets a default slider layout.
        /// </summary>
        /// <returns>Default layout for sliders.</returns>
        private static GUILayoutOption[] GetDefaultSliderLayout()
        {
            GUILayoutOption[] sliderLayout = new GUILayoutOption[1];
            sliderLayout[0] = GUILayout.MaxWidth(Screen.width / 3.0f);
            return sliderLayout;
        }

        #endregion Functions And Methods
    }
}