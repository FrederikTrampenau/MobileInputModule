// <copyright>(c) Frederik Trampenau 2019.</copyright>
// <author>Frederik Trampenau</author>
#pragma warning disable 0649 //Disable "default value is null" warning

using MIM.Interfaces;
using UnityEngine;

namespace MIM.InputTriggerArgs
{
    /// <summary>
    /// Arguments for the pinch input trigger.
    /// </summary>
    public class PinchTriggerArgs : IDeltaMovementInputArgs, IIntensityTriggerArgs
    {
        #region Properties

        /// <summary>
        /// Difference in length of the pinch span since the last frame.
        /// </summary>
        public float DeltaMovement { get; }


        /// <summary>
        /// First finger position of the pinch.
        /// </summary>
        public Vector2 StartPos { get; }


        /// <summary>
        /// Second finger position of the pinch.
        /// </summary>
        public Vector2 EndPos { get; }


        /// <summary>
        /// Length of the pinch span.
        /// </summary>
        public float Intensity { get; }

        #endregion Properties



        #region Functions And Methods

        /// <summary>
        /// Create custom pinch trigger event arguments with the given input parameters.
        /// </summary>
        /// <param name="p_deltaMovement">Difference in length of the pinch span since the last frame.</param>
        /// <param name="p_startPos">First finger position of the pinch.</param>
        /// <param name="p_endPos">Second finger position of the pinch.</param>
        /// <param name="p_intensity">Length of the pinch span.</param>
        public PinchTriggerArgs(float p_deltaMovement, Vector2 p_startPos, Vector2 p_endPos, float p_intensity)
        {
            DeltaMovement = p_deltaMovement;
            StartPos = p_startPos;
            EndPos = p_endPos;
            Intensity = p_intensity;
        }

    #endregion Functions And Methods
    }
}