// <copyright>(c) Frederik Trampenau 2019.</copyright>
// <author>Frederik Trampenau</author>
#pragma warning disable 0649 //Disable "default value is null" warning

using UnityEngine;

namespace MIM.Interfaces
{
    /// <summary>
    /// Interface for input arguments with a position parameter.
    /// </summary>
    public interface IPositionalInputArgs
    {
        #region Properties

        /// <summary>
        /// Start position of the input.
        /// </summary>
        Vector2 StartPos { get; }

        /// <summary>
        /// End positon of the input.
        /// </summary>
        Vector2 EndPos { get; }

        #endregion Properties
    }
}