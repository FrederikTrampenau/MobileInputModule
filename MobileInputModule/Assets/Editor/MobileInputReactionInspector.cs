// <copyright>(c) Frederik Trampenau 2019.</copyright>
// <author>Frederik Trampenau</author>
#pragma warning disable 0649 //Disable "default value is null" warning

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

namespace MIM.Editors
{
    /// <summary>
    /// Custom editor class for the MobileInputReaction.
    /// </summary>
    [CustomEditor(typeof(MobileInputReaction)), CanEditMultipleObjects]
    [System.Serializable]
    public class MobileInputReactionInspector : Editor
    {
        #region Serialized Fields

        /// <summary>
        /// Serialized property of the container for the input reaction fields.
        /// </summary>
        [SerializeField]
        private SerializedProperty inputReactionFields;

        /// <summary>
        /// Serialized property of the index container for all reaction fields that use action triggers.
        /// </summary>
        [SerializeField]
        private SerializedProperty reactionFieldsUsingActions;

        /// <summary>
        /// Serialized property of the index container for all reaction fields that use argument triggers.
        /// </summary>
        [SerializeField]
        private SerializedProperty reactionFieldsUsingArguments;

        /// <summary>
        /// Container for all reaction field indices and if they use action triggers (assigned in the inspector).
        /// </summary>
        [HideInInspector, SerializeField]
        private Dictionary<int, bool> useActionTriggerContainer = new Dictionary<int, bool>();

        /// <summary>
        /// Container for all reaction field indices and if they use argument triggers (assigned in the inspector).
        /// </summary>
        [HideInInspector, SerializeField]
        private Dictionary<int, bool> useArgumentTriggerContainer = new Dictionary<int, bool>();

        /// <summary>
        /// Amount of reaction fields for the mobile input reaction component.
        /// </summary>
        [HideInInspector, SerializeField]
        private int reactionFieldAmount = 0;

        #endregion Serialized Fields



        #region Private Fields

        /// <summary>
        /// Max amount of reaction fields. Limited because of in-editor performance reasons.
        /// </summary>
        private int maxReactionFields = 10;

        /// <summary>
        /// Target of this editor script, casted to an MobileInputReaction.
        /// </summary>
        private MobileInputReaction castedTargetComponent;

        #endregion Private Fields



        #region Unity Functions

        /// <summary>
        /// Set up serialized properties.
        /// </summary>
        private void OnEnable() => SetupSerializedProperties();


        /// <summary>
        /// Update inspector content.
        /// </summary>
        public override void OnInspectorGUI()
        {
            castedTargetComponent = (MobileInputReaction)target;

            serializedObject.Update();

            reactionFieldAmount = GUIFactory.CreateIntFieldGUI("Reaction Amount (0 - 10):", castedTargetComponent.inputReactionFields.Count, 0, maxReactionFields);
            inputReactionFields.arraySize = reactionFieldAmount;

            ResizeUsageDictionary(ref useActionTriggerContainer);
            ResizeUsageDictionary(ref useArgumentTriggerContainer);

            HandleReactionFields();

            UpdateActionTriggerUsage(castedTargetComponent);
            UpdateArgumentTriggerUsage(castedTargetComponent);

            serializedObject.ApplyModifiedProperties();

            if (GUI.changed)
            {
                EditorUtility.SetDirty(castedTargetComponent);
                EditorSceneManager.MarkSceneDirty(castedTargetComponent.gameObject.scene);
            }
        }

        #endregion Unity Functions



        #region Functions And Methods

        /// <summary>
        /// Make the constructor private to prevent multiple instances.
        /// </summary>
        private MobileInputReactionInspector() { }


        /// <summary>
        /// Set serialized properties by getting them from the serialized object.
        /// </summary>
        private void SetupSerializedProperties()
        {
            castedTargetComponent = (MobileInputReaction)target;

            inputReactionFields = serializedObject.FindProperty("inputReactionFields");

            var useActionTriggerDictionary = new Dictionary<int, bool>();
            for (int i = 0; i < inputReactionFields.arraySize; i++)
                useActionTriggerDictionary.Add(i, castedTargetComponent.reactionFieldsUsingActions.Contains(i));

            var useArgumentTriggerDictionary = new Dictionary<int, bool>();
            for (int i = 0; i < inputReactionFields.arraySize; i++)
                useArgumentTriggerDictionary.Add(i, castedTargetComponent.reactionFieldsUsingArguments.Contains(i));

            useActionTriggerContainer = useActionTriggerDictionary;
            useArgumentTriggerContainer = useArgumentTriggerDictionary;
        }


        /// <summary>
        /// Updates dictionary which contains information about the usage of argument triggers.
        /// </summary>
        /// <param name="castedTarget">MobileInputReaction target object of the editor script.</param>
        private void UpdateArgumentTriggerUsage(MobileInputReaction castedTarget)
        {
            List<int> updatedTriggersUsingArguments = new List<int>();
            foreach (var useArgumentTriggerEntry in useArgumentTriggerContainer)
            {
                if (useArgumentTriggerEntry.Value)
                    updatedTriggersUsingArguments.Add(useArgumentTriggerEntry.Key);
            }

            var updatedUseArgumentTriggerContainer = new Dictionary<int, bool>();
            foreach (var trigger in useArgumentTriggerContainer)
                updatedUseArgumentTriggerContainer.Add(trigger.Key, updatedTriggersUsingArguments.Contains(trigger.Key));

            castedTarget.reactionFieldsUsingArguments = updatedTriggersUsingArguments;
            castedTarget.useArgumentTriggerContainer = updatedUseArgumentTriggerContainer;
        }


        /// <summary>
        /// Updates dictionary which contains information about the usage of action triggers.
        /// </summary>
        /// <param name="castedTarget">MobileInputReaction target object of the editor script.</param>
        private void UpdateActionTriggerUsage(MobileInputReaction castedTarget)
        {
            List<int> updatedTriggersUsingActions = new List<int>();
            foreach (var useActionTriggerEntry in useActionTriggerContainer)
            {
                if (useActionTriggerEntry.Value)
                    updatedTriggersUsingActions.Add(useActionTriggerEntry.Key);
            }

            var updatedUseActionTrigger = new Dictionary<int, bool>();
            foreach (var trigger in useActionTriggerContainer)
                updatedUseActionTrigger.Add(trigger.Key, updatedTriggersUsingActions.Contains(trigger.Key));

            castedTarget.reactionFieldsUsingActions = updatedTriggersUsingActions;
            castedTarget.useActionTriggerContainer = updatedUseActionTrigger;
        }


        /// <summary>
        /// Creates or resets the reaction fields and creates GUI for them.
        /// </summary>
        private void HandleReactionFields()
        {
            for (int fieldIterator = 0; fieldIterator < inputReactionFields.arraySize; fieldIterator++)
            {
                GUI.backgroundColor = Color.black;
                GUILayout.BeginVertical("HelpBox");
                GUI.backgroundColor = Color.white;
                GUILayout.BeginVertical("Box");

                // Handle action triggers
                useActionTriggerContainer[fieldIterator] = GUIFactory.CreateToggleGUI("Use Action Input Triggers", useActionTriggerContainer[fieldIterator]);

                //if (castedTarget.useActionTriggerContainer.Count > 0 && castedTarget.useActionTriggerContainer.First().Value)
                if (useActionTriggerContainer[fieldIterator])
                {
                    GUIFactory.CreateReactionFieldGUI(fieldIterator,
                                                        "Action Input Triggers",
                                                        inputReactionFields.GetArrayElementAtIndex(fieldIterator).FindPropertyRelative("inputActionTriggers"),
                                                        inputReactionFields.GetArrayElementAtIndex(fieldIterator).FindPropertyRelative("m_OnActionTriggered"));
                }
                else
                {
                    ResetReactionField(inputReactionFields.GetArrayElementAtIndex(fieldIterator).FindPropertyRelative("inputActionTriggers"),
                                        inputReactionFields.GetArrayElementAtIndex(fieldIterator).FindPropertyRelative("m_OnActionTriggered"));
                }

                GUIFactory.AddGUISpace(3);

                // Handle argument triggers
                useArgumentTriggerContainer[fieldIterator] = GUIFactory.CreateToggleGUI("Use Argument Input Triggers", useArgumentTriggerContainer[fieldIterator]);

                if (useArgumentTriggerContainer[fieldIterator])
                {
                    GUIFactory.CreateReactionFieldGUI(fieldIterator,
                                                        "Argument Input Triggers",
                                                        inputReactionFields.GetArrayElementAtIndex(fieldIterator).FindPropertyRelative("inputArgumentTriggers"),
                                                        inputReactionFields.GetArrayElementAtIndex(fieldIterator).FindPropertyRelative("m_OnArgumentInputTriggered"));
                }
                else
                {
                    ResetReactionField(inputReactionFields.GetArrayElementAtIndex(fieldIterator).FindPropertyRelative("inputArgumentTriggers"),
                                        inputReactionFields.GetArrayElementAtIndex(fieldIterator).FindPropertyRelative("m_OnArgumentInputTriggered"));
                }

                GUILayout.EndVertical();
                GUILayout.EndVertical();

                CreateSpaceBetweenFields(fieldIterator);
            }
        }


        /// <summary>
        /// Creates GUI spaces by the given amount.
        /// </summary>
        /// <param name="fieldIterator">Iterator value for the reaction field array.</param>
        private void CreateSpaceBetweenFields(int fieldIterator)
        {
            if (fieldIterator < inputReactionFields.arraySize - 1)
                GUIFactory.AddGUISpace(3);
        }


        /// <summary>
        /// Resets the trigger list and unity events of the reaction field.
        /// </summary>
        /// <param name="triggerListProperty">Property for the list of triggers.</param>
        /// <param name="unityEventProperty">Property for the unity event which contains the reactions of the triggers.</param>
        private void ResetReactionField(SerializedProperty triggerListProperty, SerializedProperty unityEventProperty)
        {
            // Reset trigger list
            var triggerAmount = triggerListProperty.arraySize;
            if (triggerAmount > 0)
            {
                triggerListProperty.ClearArray();
                triggerListProperty.arraySize = 0;
            }
        }


        /// <summary>
        /// Resizes the given dictionary so it fits to th input reaction field amount.
        /// </summary>
        /// <param name="dictionaryToResize">Dictionary which gets resized to the amount of reaction fields.</param>
        private void ResizeUsageDictionary(ref Dictionary<int, bool> dictionaryToResize)
        {
            if (inputReactionFields.arraySize < dictionaryToResize.Count)
                dictionaryToResize.Remove(dictionaryToResize.Count - 1);
            if (inputReactionFields.arraySize > dictionaryToResize.Count)
                dictionaryToResize.Add(dictionaryToResize.Count, false);

            // Resize dictionary recursive
            if (inputReactionFields.arraySize != dictionaryToResize.Count)
                ResizeUsageDictionary(ref dictionaryToResize);
        }

        #endregion Functions And Methods
    }
}