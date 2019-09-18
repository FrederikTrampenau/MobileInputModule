// <copyright>(c) Frederik Trampenau 2019.</copyright>
// <author>Frederik Trampenau</author>
#pragma warning disable 0649 //Disable "default value is null" warning

using MIM.Interfaces;
using UnityEngine;

namespace MIM.InputTriggerArgs
{
    /// <summary>
    /// Arguments for the tilt input trigger.
    /// </summary>
    public class TiltTriggerArgs : IVectorMovementInputArgs
    {
        #region Properties

        /// <summary>
        /// Device x and y tilt movement vector of the device.
        /// </summary>
        public Vector2 MovementVector { get; private set; }


        /// <summary>
        /// Start position of the swipe.
        /// </summary>
        public Vector2 StartPos { get; private set; }


        /// <summary>
        /// End position of the swipe.
        /// </summary>
        public Vector2 EndPos { get; private set; }

        #endregion Properties



        #region Functions And Methods

        /// <summary>
        /// Create custom tilt trigger event arguments with the given input parameters.
        /// </summary>
        /// <param name="p_movementVector">Device tilt movement vector.</param>
        /// <param name="p_startPos">Start position of the swipe.</param>
        /// <param name="p_endPos">End position of the swipe.</param>
        public TiltTriggerArgs(Vector2 p_movementVector, Vector2 p_startPos, Vector2 p_endPos)
        {
            MovementVector = p_movementVector;
            StartPos = p_startPos;
            EndPos = p_endPos;
        }

        #endregion Functions And Methods
    }
}