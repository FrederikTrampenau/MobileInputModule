// <copyright>(c) Frederik Trampenau 2019.</copyright>
// <author>Frederik Trampenau</author>
#pragma warning disable 0649 //Disable "default value is null" warning

using System;
using UnityEngine;

namespace MIM.Utility
{
    /// <summary>
    /// Helper class for math functionalities.
    /// </summary>
    public class MathUtility
    {
        #region Functions And Methods

        /// <summary>
        /// Rounds a Vector2 to a given amount of decimals.
        /// </summary>
        /// <param name="vectorToRound">Vector2 thath gets rounded.</param>
        /// <param name="decimals">Amount of digits after the decimal point.</param>
        /// <returns>Rounded Vector3 with given decimals.</returns>
        public static Vector2 RoundVector(Vector2 vectorToRound, int decimals)
        {
            var roundedVector = new Vector2(
                (float)Math.Round(vectorToRound.x, decimals),
                (float)Math.Round(vectorToRound.y, decimals));

            return roundedVector;
        }


        /// <summary>
        /// Rounds a Vector3 to a given amount of decimals.
        /// </summary>
        /// <param name="vectorToRound">Vector3 thath gets rounded.</param>
        /// <param name="decimals">Amount of digits after the decimal point.</param>
        /// <returns>Rounded Vector3 with given decimals.</returns>
        public static Vector3 RoundVector(Vector3 vectorToRound, int decimals)
        {
            var roundedVector = new Vector3(
                (float)Math.Round(vectorToRound.x, decimals),
                (float)Math.Round(vectorToRound.y, decimals),
                (float)Math.Round(vectorToRound.z, decimals));

            return roundedVector;
        }


        /// <summary>
        /// Calculates difference of the two given vectors.
        /// </summary>
        /// <param name="firstVector">First vector to substract from.</param>
        /// <param name="secondVector">Second vector that gets substracted.</param>
        /// <returns>Difference of the two vectors (also the vector from first to second vector).</returns>
        public static Vector2 CalculateVectorDifference(Vector2 firstVector, Vector2 secondVector) => firstVector - secondVector;

        #endregion Functions And Methods
    }
}