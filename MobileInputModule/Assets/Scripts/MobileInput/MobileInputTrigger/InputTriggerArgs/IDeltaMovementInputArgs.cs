// <copyright>(c) Frederik Trampenau 2019.</copyright>
// <author>Frederik Trampenau</author>
#pragma warning disable 0649 //Disable "default value is null" warning

namespace MIM.Interfaces
{
    /// <summary>
    /// Interface for input arguments with a delta movement parameter.
    /// </summary>
    public interface IDeltaMovementInputArgs : IPositionalInputArgs
    {
        #region Properties

        /// <summary>
        /// Difference in length of the pinch span since the last frame.
        /// </summary>
        float DeltaMovement { get; }

        #endregion Properties
    }
}