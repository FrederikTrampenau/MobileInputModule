// <copyright>(c) Frederik Trampenau 2019.</copyright>
// <author>Frederik Trampenau</author>
#pragma warning disable 0649 //Disable "default value is null" warning

using MIM.Interfaces;
using UnityEngine;

namespace MIM.InputTriggerArgs
{
    /// <summary>
    /// Arguments for the swipe input trigger.
    /// </summary>
    public class SwipeTriggerArgs : IVectorMovementInputArgs, IIntensityTriggerArgs
    {
        #region Properties

        /// <summary>
        /// Start position of the swipe.
        /// </summary>
        public Vector2 StartPos { get; private set; }


        /// <summary>
        /// End position of the swipe.
        /// </summary>
        public Vector2 EndPos { get; private set; }


        /// <summary>
        /// Length of the swipe.
        /// </summary>
        public float Intensity { get; private set; }


        /// <summary>
        /// Complete vector from the beginning to the end of the swipe.
        /// </summary>
        public Vector2 MovementVector { get; private set; }

        #endregion Properties



        #region Functions And Methods

        /// <summary>
        /// Create custom swipe trigger event arguments with the given input parameters.
        /// </summary>
        /// <param name="p_startPos">Start position of the swipe.</param>
        /// <param name="p_endPos">End position of the swipe.</param>
        /// <param name="p_intensity">Length of the swipe.</param>
        /// <param name="p_completeSwipeVector">Vector from start to end of the swipe.</param>
        public SwipeTriggerArgs(Vector2 p_startPos, Vector2 p_endPos, float p_intensity, Vector2 p_completeSwipeVector)
        {
            StartPos = p_startPos;
            EndPos = p_endPos;
            Intensity = p_intensity;
            MovementVector = p_completeSwipeVector;
        }

        #endregion Functions And Methods
    }
}