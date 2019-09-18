// <copyright>(c) Frederik Trampenau 2019.</copyright>
// <author>Frederik Trampenau</author>
#pragma warning disable 0649 //Disable "default value is null" warning

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Linq;

namespace MIM
{
    /// <summary>
    /// Used only in the editor to prevent deletion of the mobile input settings asset.
    /// </summary>
    [InitializeOnLoad]
    public class MobileInputSettingsAssetProtector
    {
        #region Private Fields

        /// <summary>
        /// Default name of the mobile input settings asset.
        /// </summary>
        private static string defaultSettingsAssetName = "MobileInputSettingsData";

        /// <summary>
        /// Instance of the mobile settings asset protector.
        /// </summary>
        private static MobileInputSettingsAssetProtector instance = null;

        #endregion Private Fields

        #region Properties

        /// <summary>
        /// Gets the instance of the mobile settings asset protector and creates a new one if its null.
        /// </summary>
        public static MobileInputSettingsAssetProtector Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MobileInputSettingsAssetProtector();
                }

                return instance;
            }
        }

        #endregion Properties



        #region Functions And Methods

        /// <summary>
        /// Subscribes to the editor update when the editor gets opened or when the solution compiles because of the [InitializeOnLoad] attribute.
        /// </summary>
        static MobileInputSettingsAssetProtector() => EditorApplication.update += PreventAssetDeletion;


        /// <summary>
        /// Sets the default mobile input setting asset name and subscribing to the update of the editor.
        /// </summary>
        /// <param name="defaultAssetName"></param>
        public void Setup(string defaultAssetName) => defaultSettingsAssetName = defaultAssetName;


        /// <summary>
        /// Continously checks if a mobile input settings asset exist and creates one if not.
        /// </summary>
        public static void PreventAssetDeletion()
        {
            Resources.LoadAll(string.Empty, typeof(MobileInputSettingsData));
            var foundSettings = Resources.FindObjectsOfTypeAll<MobileInputSettingsData>();
            if (foundSettings.Length <= 0)
            {
                CreateDefaultSettingsAsset();
            }
            else if (foundSettings.Length > 1)
            {
                foreach (var item in foundSettings.Skip(1))
                {
                    var path = AssetDatabase.GetAssetPath(item);
                    AssetDatabase.DeleteAsset(path);
                }
            }
        }


        /// <summary>
        /// Creates a mobile input settings asset with default values.
        /// </summary>
        /// <returns>Instance of the settings scripatble object.</returns>
        public static MobileInputSettingsData CreateDefaultSettingsAsset()
        {
            MobileInputSettingsData defaultSettings = ScriptableObject.CreateInstance<MobileInputSettingsData>();
            AssetDatabase.CreateAsset(defaultSettings, "Assets/Resources/" + defaultSettingsAssetName + ".asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();

            return defaultSettings;
        }

        #endregion Functions And Methods
    }
}
#endif