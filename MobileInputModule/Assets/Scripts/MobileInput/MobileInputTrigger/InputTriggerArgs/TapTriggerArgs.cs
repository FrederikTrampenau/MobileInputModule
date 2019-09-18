// <copyright>(c) Frederik Trampenau 2019.</copyright>
// <author>Frederik Trampenau</author>
#pragma warning disable 0649 //Disable "default value is null" warning

using MIM.Interfaces;
using UnityEngine;

namespace MIM.InputTriggerArgs
{
    /// <summary>
    /// Arguments for the tap input trigger.
    /// </summary>
    public class TapTriggerArgs : IPositionalInputArgs
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

        #endregion Properties



        #region Functions And Methods

        /// <summary>
        /// Create custom tap trigger event arguments with the given input parameters.
        /// </summary>
        /// <param name="p_startPos">Start position of the swipe.</param>
        /// <param name="p_endPos">End position of the swipe.</param>
        public TapTriggerArgs(Vector2 p_startPos, Vector2 p_endPos)
        {
            StartPos = p_startPos;
            EndPos = p_endPos;
        }

        #endregion Functions And Methods
    }
}