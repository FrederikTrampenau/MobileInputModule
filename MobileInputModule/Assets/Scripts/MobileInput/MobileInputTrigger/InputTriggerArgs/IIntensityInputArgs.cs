// <copyright>(c) Frederik Trampenau 2019.</copyright>
// <author>Frederik Trampenau</author>
#pragma warning disable 0649 //Disable "default value is null" warning

namespace MIM.Interfaces
{
    /// <summary>
    /// Interface for input arguments with an intensity parameter.
    /// </summary>
    public interface IIntensityTriggerArgs : IPositionalInputArgs
    {
        #region Properties

        /// <summary>
        /// Intensity, length, magnitude or something similar of the trigger.
        /// </summary>
        float Intensity { get; }

        #endregion Properties
    }
}