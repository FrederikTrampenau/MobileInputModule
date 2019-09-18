// <copyright>(c) Frederik Trampenau 2019.</copyright>
// <author>Frederik Trampenau</author>
#pragma warning disable 0649 //Disable "default value is null" warning

using MIM.InputTriggers;
using System;
using System.Linq;
using UnityEngine;

namespace MIM
{
    /// <summary>
    /// Loads settings, creates default mobile input reaction and processes the mobile input functionality.
    /// </summary>
    [AddComponentMenu("Event/Mobile Input Module")]
    public class MobileInputModule : MonoBehaviour
    {
        #region Private Fields

        /// <summary>
        /// Loaded / created mobile input settings data.
        /// </summary>
        private static MobileInputSettingsData settingsData;

        #endregion Private Fields



        #region Properties

        /// <summary>
        /// Tries to load settings data or creates default values if none got found.
        /// </summary>
        private static MobileInputSettingsData SettingsData
        {
            get
            {
                if (settingsData) return settingsData;

                Resources.LoadAll(string.Empty, typeof(MobileInputSettingsData));
                var foundSettings = Resources.Load<MobileInputSettingsData>(MobileInputSettingsData.SettingsFileName);

                settingsData = foundSettings != null ? foundSettings : new MobileInputSettingsData();

                return settingsData;
            }
        }

        /// <summary>
        /// Loads mobile input settings from the resources.
        /// </summary>
        /// <returns>Settings data loaded from the resources.</returns>
        public static MobileInputSettingsData LoadSettings() => SettingsData;

        #endregion Properties



        #region Events

        /// <summary>
        /// Gets called every frame by an Update method.
        /// </summary>
        private Action OnUpdate;

        #endregion Events



        #region Unity Functions

        /// <summary>
        /// Calls the setup of the input module.
        /// </summary>
        private void Awake() => Setup();


        /// <summary>
        /// Calls the OnUpdate action to process the mobile input.
        /// </summary>
        private void Update() => OnUpdate?.Invoke();


        /// <summary>
        /// Removes the process function of the mobile input from the invokation list of the OnUpdate action.
        /// </summary>
        private void OnDestroy() => OnUpdate -= MobileInput.Process;

        #endregion Unity Functions



        #region Functions And Methods

        /// <summary>
        /// Adds the process function of the mobile input to the invocation list of the OnUpdate action and sets up the TiltEventDetector.
        /// </summary>
        private void Setup()
        {
            if (IsFirstModuleInHierarchy())
            {
                CreateDefaultReactionComponent();
                RemoveDuplications();

                OnUpdate += MobileInput.Process;

                TiltEventDetector.Setup(LoadSettings());
            }
        }


        /// <summary>
        /// Creates, attaches and sets up a default reaction component which covers all action and argument triggers.
        /// </summary>
        private void CreateDefaultReactionComponent()
        {
            var defaultMobileInputReaction = gameObject.AddComponent<MobileInputReaction>();
            defaultMobileInputReaction.inputReactionFields.Add(new InputReactionField());

            EnableAllTriggers(defaultMobileInputReaction, 0);
            AddAllTriggersToReactionComponent(defaultMobileInputReaction, 0);
        }


        /// <summary>
        /// Enables action and arugment triggers for the given reaction field of the input reaction component.
        /// </summary>
        /// <param name="inputReactionComponent">Reaction component that should have all triggers enabled.</param>
        /// <param name="fieldIdxToEnable">Index of the field whose triggers should become enabled.</param>
        private static void EnableAllTriggers(MobileInputReaction inputReactionComponent, int fieldIdxToEnable)
        {
            inputReactionComponent.reactionFieldsUsingActions.Add(fieldIdxToEnable);
            inputReactionComponent.reactionFieldsUsingArguments.Add(fieldIdxToEnable);
            inputReactionComponent.useActionTriggerContainer.Add(fieldIdxToEnable, true);
            inputReactionComponent.useArgumentTriggerContainer.Add(fieldIdxToEnable, true);
        }


        /// <summary>
        /// Adds all loaded triggers from the resources to the first field of the reaction component.
        /// </summary>
        /// <param name="inputReactionComponent">Reaction component that should have all triggers added to it.</param>
        /// <param name="fieldIdxToAddTo">Index of the field the triggers should be added to.</param>
        private static void AddAllTriggersToReactionComponent(MobileInputReaction inputReactionComponent, int fieldIdxToAddTo)
        {
            var loadedArgumentTriggers = Resources.LoadAll<InputArgsTrigger>("MobileInputTrigger/ArgumentTriggers");
            foreach (var inputTrigger in loadedArgumentTriggers)
                inputReactionComponent.inputReactionFields[fieldIdxToAddTo].inputArgumentTriggers.Add(inputTrigger);

            var loadedActionTriggers = Resources.LoadAll<InputActionTrigger>("MobileInputTrigger/ActionTriggers");
            foreach (var inputTrigger in loadedActionTriggers)
                inputReactionComponent.inputReactionFields[fieldIdxToAddTo].inputActionTriggers.Add(inputTrigger);
        }


        /// <summary>
        /// Checks if it is the first of its kind in the hierarchy.
        /// </summary>
        /// <returns>Is this component the first of its kind in the hierarchy.</returns>
        private bool IsFirstModuleInHierarchy() => Equals(FindObjectsOfType<MobileInputModule>().OrderBy(module => module.transform.GetSiblingIndex()).First());


        /// <summary>
        /// Destroys any duplications of the input module except itself.
        /// </summary>
        private void RemoveDuplications() => FindObjectsOfType<MobileInputModule>().Where(module => module != this).ToList().ForEach(moduleDuplication => Destroy(moduleDuplication));

        #endregion Functions And Methods
    }
}