// <copyright>(c) Frederik Trampenau 2019.</copyright>
// <author>Frederik Trampenau</author>
#pragma warning disable 0649 //Disable "default value is null" warning

using MIM.Interfaces;
using UnityEngine;

namespace MIM.InputTriggerArgs
{
    /// <summary>
    /// Base class for input trigger arguments.
    /// </summary>
    public class InputTriggerBaseArgs : IPositionalInputArgs
{
        #region Properties

        /// <summary>
        /// Start position of the input.
        /// </summary>
        public Vector2 StartPos { get; private set; }


        /// <summary>
        /// End position of the input.
        /// </summary>
        public Vector2 EndPos { get; }

        #endregion Properties



        #region Functions And Methods

        /// <summary>
        /// Create custom input trigger event arguments with the given parameters.
        /// </summary>
        /// <param name="p_startPos">Start position of the input.</param>
        /// <param name="p_endtPos">End position of the input.</param>
        public InputTriggerBaseArgs(Vector2 p_startPos, Vector2 p_endPos)
        {
            StartPos = p_startPos;
            EndPos = p_endPos;
        }

        #endregion Functions And Methods
    }
}