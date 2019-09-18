// <copyright>(c) Frederik Trampenau 2019.</copyright>
// <author>Frederik Trampenau</author>
#pragma warning disable 0649 //Disable "default value is null" warning

using MIM.InputTriggerArgs;
using MIM.Interfaces;
using MIM.Utility;
using System;
using UnityEngine;

namespace MIM.InputTriggers
{
    /// <summary>
    /// Evaluates touch data regarding pinches.
    /// </summary>
    sealed class PinchEvaluator
    {
        #region Properties

        /// <summary>
        /// Gets called when an ongoing pinch gets detected.
        /// </summary>
        public static Action<IPositionalInputArgs> OnPinchDetected;

        #endregion Properties



        #region Functions And Methods

        /// <summary>
        /// Calculates pinch with given touches and triggers input actions.
        /// </summary>
        /// <param name="touchZero">Touch of the first finger.</param>
        /// <param name="touchOne">Touch of the second finger.</param>
        public static void EvaluateOngoingPinch(Touch touchZero, Touch touchOne)
        {
            // Previous touch data
            var prevTouchZeroPos = touchZero.position - touchZero.deltaPosition;
            var prevTouchOnePos = touchOne.position - touchOne.deltaPosition;
            var prevTouchDeltaMovement = MathUtility.CalculateVectorDifference(prevTouchZeroPos, prevTouchOnePos);

            // Current touch data
            var curTouchZeroPos = touchZero.position;
            var curTouchOnePos = touchOne.position;
            var curTouchDeltaMovement = MathUtility.CalculateVectorDifference(curTouchOnePos, curTouchZeroPos);

            // Pinch data
            var pinchDeltaVector = MathUtility.CalculateVectorDifference(prevTouchDeltaMovement, curTouchDeltaMovement);
            var pinchDeltaMovement = CalculatePinchDeltaMovement(prevTouchDeltaMovement, curTouchDeltaMovement);

            var pinchTriggerArgs = new PinchTriggerArgs(pinchDeltaMovement, curTouchZeroPos, curTouchOnePos, pinchDeltaVector.magnitude);

            OnPinchDetected?.Invoke(pinchTriggerArgs);
        }


        /// <summary>
        /// Calculates difference in length of the pinch span since the last frame.
        /// </summary>
        /// <param name="prevTouchDeltaMovement">Pinch span of the previous pinch.</param>
        /// <param name="curTouchDeltaMovement">Pinch span of the current pinch.</param>
        /// <returns>Difference in length of the pinch span since the last frame.</returns>
        private static float CalculatePinchDeltaMovement(Vector2 prevTouchDeltaMovement, Vector2 curTouchDeltaMovement)
        {
            var prevTouchDeltaMagnitude = prevTouchDeltaMovement.magnitude;
            var curTouchDeltaMagnitude = curTouchDeltaMovement.magnitude;

            var deltaMovement = curTouchDeltaMagnitude - prevTouchDeltaMagnitude;
            return deltaMovement;
        }

        #endregion Functions And Methods
    }
}