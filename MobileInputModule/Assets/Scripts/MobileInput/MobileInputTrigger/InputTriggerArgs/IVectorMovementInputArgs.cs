// <copyright>(c) Frederik Trampenau 2019.</copyright>
// <author>Frederik Trampenau</author>
#pragma warning disable 0649 //Disable "default value is null" warning

using UnityEngine;

namespace MIM.Interfaces
{
    /// <summary>
    /// Interface for input arguments with a vector movement parameter.
    /// </summary>
    public interface IVectorMovementInputArgs : IPositionalInputArgs
    {
        #region Properties

        /// <summary>
        /// Complete vector from the beginning to the end of the swipe.
        /// </summary>
        Vector2 MovementVector { get; }

        #endregion Properties
    }
}