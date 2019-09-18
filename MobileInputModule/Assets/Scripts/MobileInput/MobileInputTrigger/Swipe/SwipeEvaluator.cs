// <copyright>(c) Frederik Trampenau 2019.</copyright>
// <author>Frederik Trampenau</author>
#pragma warning disable 0649 //Disable "default value is null" warning

using MIM.InputTriggerArgs;
using MIM.Interfaces;
using System;
using System.Linq;
using UnityEngine;

namespace MIM.InputTriggers
{
    /// <summary>
    /// Enum for the direction of a swipe.
    /// </summary>
    public enum SwipeDirection { LEFT, RIGHT, UP, DOWN, NONE }

    /// <summary>
    /// Evaluates touch data regarding swipes.
    /// </summary>
    sealed class SwipeEvaluator
    {
        #region Structs

        /// <summary>
        /// Percentage of the screen in width and heigth. Used for swipe validation.
        /// </summary>
        private struct ScreenPercentage
        {
            public float widthPercentage;
            public float heightPercentage;
        }

        #endregion Structs



        #region Events

        /// <summary>
        /// Gets called when a swipe ends.
        /// </summary>
        public static Action<IPositionalInputArgs> OnSwipeEnded;

        /// <summary>
        /// Gets called when a left swipe ends.
        /// </summary>
        public static Action OnLeftSwipeDetected;

        /// <summary>
        /// Gets called when a right swipe ends.
        /// </summary>
        public static Action OnRightSwipeDetected;

        /// <summary>
        /// Gets called when an up swipe ends.
        /// </summary>
        public static Action OnUpSwipeDetected;

        /// <summary>
        /// Gets called when a down swipe ends.
        /// </summary>
        public static Action OnDownSwipeDetected;

        #endregion Events



        #region Functions And Methods

        /// <summary>
        /// Make the constructor private to prevent multiple instances.
        /// </summary>
        private SwipeEvaluator() { }


        /// <summary>
        /// Evaluates an ended touch history and invokes actions according to the result of the evaluation.
        /// </summary>
        /// <param name="history"></param>
        public static void EvaluateEndedTouchHistory(TouchHistory history)
        {
            var startPos = history.trace.First().Value;
            var endPos = history.trace.Last().Value;

            var completeSwipeVector = endPos - startPos;
            var swipeDirection = CalculateSwipeDirection(completeSwipeVector);

            if (IsSwipeValid(completeSwipeVector, swipeDirection))
            {
                var swipeTriggerArgs = CreateSwipeTriggerArgs(history);

                InvokeSwipeActions(swipeTriggerArgs, swipeDirection);
            }
        }


        /// <summary>
        /// Creates arguments for the swipe by evaluating the touch history.
        /// </summary>
        /// <param name="history">History of touch positions.</param>
        /// <returns>Event arguments created with given touch history.</returns>
        public static SwipeTriggerArgs CreateSwipeTriggerArgs(TouchHistory history)
        {
            var startPos = history.trace.First().Value;
            var endPos = history.trace.Last().Value;
            var completeSwipeVector = endPos - startPos;
            var intensity = completeSwipeVector.magnitude;

            return new SwipeTriggerArgs(startPos, endPos, intensity, completeSwipeVector);
        }


        /// <summary>
        /// Checks if a swipe was valid and should get registered.
        /// </summary>
        /// <param name="completeSwipeVector">Vector of the swipe fro mstart to end.</param>
        /// <param name="swipeDirection">Direction form the swipe.</param>
        /// <returns>Should the swipe get registered as a swipe?</returns>
        private static bool IsSwipeValid(Vector2 completeSwipeVector, SwipeDirection swipeDirection)
        {
            var swipemagnitude = completeSwipeVector.magnitude;

            var swipeOnScreenPercentage = CalculateSwipeOnScreenPercentage(swipemagnitude);

            var minHorizontalSwipeLengthScreenPercentage = MobileInputModule.LoadSettings().minHorizontalSwipeLength / 100;
            var minVerticalSwipeLengthScreenPercentage = MobileInputModule.LoadSettings().minVerticalSwipeLength / 100;

            switch (swipeDirection)
            {
                case SwipeDirection.LEFT:
                case SwipeDirection.RIGHT:
                    if (minHorizontalSwipeLengthScreenPercentage > swipeOnScreenPercentage.widthPercentage) return false;
                    break;
                case SwipeDirection.UP:
                case SwipeDirection.DOWN:
                    if (minVerticalSwipeLengthScreenPercentage > swipeOnScreenPercentage.heightPercentage) return false;
                    break;
            }

            return true;
        }


        /// <summary>
        /// Calculates percentage of the screen length in relation to the swipe span.
        /// </summary>
        /// <param name="swipeLength">Length of the span of the swipe.</param>
        /// <returns>Width and heigth screen percentage in relation to the swipe span.</returns>
        private static ScreenPercentage CalculateSwipeOnScreenPercentage(float swipeLength)
        {
            var screenWidth = Screen.width;
            var screenHeight = Screen.height;

            return new ScreenPercentage { widthPercentage = swipeLength / screenWidth, heightPercentage = swipeLength / screenHeight };
        }


        /// <summary>
        /// Returns the direction of the swipe, given by the vector of the swipe.
        /// </summary>
        /// <param name="swipeVector">Movement vector of the swipe.</param>
        /// <returns>Direction the swipe is going.</returns>
        private static SwipeDirection CalculateSwipeDirection(Vector2 swipeVector)
        {
            var direction = SwipeDirection.NONE;

            if (Math.Sqrt(swipeVector.x * swipeVector.x) > Math.Sqrt(swipeVector.y * swipeVector.y))
            {
                direction = swipeVector.x > 0.0f ? SwipeDirection.RIGHT : SwipeDirection.LEFT;
            }
            else if (Math.Sqrt(swipeVector.x * swipeVector.x) < Math.Sqrt(swipeVector.y * swipeVector.y))
            {
                direction = swipeVector.y > 0.0f ? SwipeDirection.UP : SwipeDirection.DOWN;
            }
            return direction;
        }


        /// <summary>
        /// Invokes actions of swiping with the given arguments and dependent on the direction of the swipe.
        /// </summary>
        /// <param name="args">Input arguments of the evaluated swipe.</param>
        /// <param name="direction">Direction of the evaluated swipe.</param>
        private static void InvokeSwipeActions(IVectorMovementInputArgs args, SwipeDirection direction)
        {
            switch (direction)
            {
                case SwipeDirection.LEFT:
                    OnLeftSwipeDetected?.Invoke();
                    MobileInput.OnSwipeLeft?.Invoke();
                    break;
                case SwipeDirection.RIGHT:
                    OnRightSwipeDetected?.Invoke();
                    MobileInput.OnSwipeRight?.Invoke();
                    break;
                case SwipeDirection.UP:
                    OnUpSwipeDetected?.Invoke();
                    MobileInput.OnSwipeUp?.Invoke();
                    break;
                case SwipeDirection.DOWN:
                    OnDownSwipeDetected?.Invoke();
                    MobileInput.OnSwipeDown?.Invoke();
                    break;
                case SwipeDirection.NONE:
                default:
                    return;
            }

            OnSwipeEnded?.Invoke(args);
        }

        #endregion Functions And Methods
    }
}