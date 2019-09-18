// <copyright>(c) Frederik Trampenau 2019.</copyright>
// <author>Frederik Trampenau</author>
#pragma warning disable 0649 //Disable "default value is null" warning

using MIM.InputTriggerArgs;
using MIM.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MIM.InputTriggers
{
    /// <summary>
    /// Enum for the direction of a tilt.
    /// </summary>
    public enum TiltDirection { RIGHT, LEFT, FORWARD, BACKWARD, NONE }

    /// <summary>
    /// Evaluates device acceleration data regarding tilts.
    /// </summary>
    public class TiltEvaluator
    {
        #region Structs

        /// <summary>
        /// Struct which holds information about a tilt.
        /// </summary>
        private struct Tilt
        {
            TiltDirection direction;
        }

        #endregion Structs



        #region Properties

        /// <summary>
        /// Loaded input settings from the mobile input settings module.
        /// </summary>
        private static MobileInputSettingsData loadedSettingsData;

        /// <summary>
        /// Holds previous tilts. Used to check for ended tilts.
        /// </summary>
        private static HashSet<TiltDirection> previousTilts = new HashSet<TiltDirection>();

        #endregion Properties



        #region Events

        /// <summary>
        /// Gets called while a tilt is happening.
        /// </summary>
        public static Action<IVectorMovementInputArgs> OnOngoingTilt;

        /// <summary>
        /// Gets called while a left tilt is happening.
        /// </summary>
        public static Action<IVectorMovementInputArgs> OnOngoingTiltLeft;

        /// <summary>
        /// Gets called while a right tilt is happening.
        /// </summary>
        public static Action<IVectorMovementInputArgs> OnOngoingTiltRight;

        /// <summary>
        /// Gets called while a forward tilt is happening.
        /// </summary>
        public static Action<IVectorMovementInputArgs> OnOngoingTiltForward;

        /// <summary>
        /// Gets called while a backward tilt is happening.
        /// </summary>
        public static Action<IVectorMovementInputArgs> OnOngoingTiltBackward;

        /// <summary>
        /// Gets called when a left tilt ends.
        /// </summary>
        public static Action OnEndedTiltLeft;

        /// <summary>
        /// Gets called when a right tilt ends.
        /// </summary>
        public static Action OnEndedTiltRight;

        /// <summary>
        /// Gets called when a forward tilt ends.
        /// </summary>
        public static Action OnEndedTiltForward;

        /// <summary>
        /// Gets called when a backward tilt ends.
        /// </summary>
        public static Action OnEndedTiltBackward;

        #endregion Events



        #region Functions And Methods

        /// <summary>
        /// Set up by setting the loaded settings data.
        /// </summary>
        /// <param name="p_loadedSettingsData">Loaded mobile input module settings data.</param>
        public static void Setup(MobileInputSettingsData p_loadedSettingsData) => loadedSettingsData = p_loadedSettingsData;


        /// <summary>
        /// Evaluates if the device acceleration is a valid tilt and handles its events.
        /// </summary>
        /// <param name="acceleration">Acceleration of the device.</param>
        public static void EvaluateOngoingTilt(Vector2 acceleration)
        {
            HashSet<TiltDirection> directions = CalculateTiltDirections(acceleration);

            HandleEndedTilts(directions);

            HandleOngoingTilts(acceleration, directions);
        }


        /// <summary>
        /// Checks if a tilt has ended, invokes its action and removes triggered tilts from the previous tilt tracking container.
        /// </summary>
        /// <param name="ongoingTiltDirections">Directions of the currently ongoing tilt.</param>
        private static void HandleEndedTilts(HashSet<TiltDirection> ongoingTiltDirections)
        {
            var remainingActivePreviousTilts = previousTilts;
            foreach (var previousTilt in previousTilts)
            {
                // Checks if a tilt is not active anymore but was in the previous frame
                if (!ongoingTiltDirections.Contains(previousTilt))
                {
                    InvokeEndedTiltAction(previousTilt);
                    remainingActivePreviousTilts.Remove(previousTilt);
                }
            }
            previousTilts = remainingActivePreviousTilts;
        }


        /// <summary>
        /// Checks if ongoing tilts are valid, invokes actions according to the valid tilts and updates the previous tilt tracking container.
        /// </summary>
        /// <param name="acceleration">Device acceleration of the happening tilt.</param>
        /// <param name="ongoingTiltDirections">Directions of the currently ongoing tilt.</param>
        private static void HandleOngoingTilts(Vector2 acceleration, HashSet<TiltDirection> ongoingTiltDirections)
        {
            var tiltMovement = CreateTiltTriggerArs(acceleration);
            var isAnyTiltValid = false;

            foreach (var direction in ongoingTiltDirections)
            {
                if (CheckIsTiltValid(direction, acceleration))
                {
                    isAnyTiltValid = true;
                    InvokeOngoingTiltDirectionAction(direction, tiltMovement);

                    UpdatePreviousOngoingTilts(direction);
                }
            }

            if (isAnyTiltValid)
                InvokeOngoingTiltAction(tiltMovement);
        }


        /// <summary>
        /// Updates container which tracks ongoing tilts from the previous frame.
        /// </summary>
        /// <param name="ongoingTiltDirection">Currently ongoing tilt direction.</param>
        private static void UpdatePreviousOngoingTilts(TiltDirection ongoingTiltDirection)
        {
            if (!previousTilts.Contains(ongoingTiltDirection))
                previousTilts.Add(ongoingTiltDirection);
        }


        /// <summary>
        /// Checks if the tilt was strong enough to pass the treshold values.
        /// </summary>
        /// <param name="direction">Direction of the happening tilt.</param>
        /// <param name="acceleration">Device acceleration of the happening tilt.</param>
        /// <returns>Was the tilt strong enough to pass the needed treshold values.</returns>
        private static bool CheckIsTiltValid(TiltDirection direction, Vector2 acceleration)
        {
            switch (direction)
            {
                case TiltDirection.RIGHT:
                    return acceleration.x >= loadedSettingsData.xTiltPosThreshold;
                case TiltDirection.LEFT:
                    return acceleration.x <= loadedSettingsData.xTiltNegThreshold;

                case TiltDirection.FORWARD:
                    return acceleration.y >= loadedSettingsData.yTiltPosThreshold;
                case TiltDirection.BACKWARD:
                    return acceleration.y <= loadedSettingsData.yTiltNegThreshold;

                case TiltDirection.NONE:
                default:
                    return false;
            }
        }


        /// <summary>
        /// Calculate current tilt directions with the given acceleration.
        /// </summary>
        /// <param name="acceleration">Current acceleration of the device.</param>
        /// <returns>Container with all currently happening tilt directions.</returns>
        private static HashSet<TiltDirection> CalculateTiltDirections(Vector2 acceleration)
        {
            HashSet<TiltDirection> directions = new HashSet<TiltDirection>();

            if (acceleration.x > 0.0f)
                directions.Add(TiltDirection.RIGHT);
            else if (acceleration.x < 0.0f)
                directions.Add(TiltDirection.LEFT);

            if (acceleration.y > 0.0f)
                directions.Add(TiltDirection.FORWARD);
            else if (acceleration.y < 0.0f)
                directions.Add(TiltDirection.BACKWARD);

            return directions;
        }


        /// <summary>
        /// Invoke ongoing tilt actions dependent on the given direction.
        /// </summary>
        /// <param name="direction">Direction of the tilt</param>
        /// <param name="tiltMovement">Current movement of the device.</param>
        private static void InvokeOngoingTiltDirectionAction(TiltDirection direction, IVectorMovementInputArgs tiltMovement)
        {
            switch (direction)
            {
                case TiltDirection.RIGHT:
                    OnOngoingTiltRight?.Invoke(tiltMovement);
                    break;
                case TiltDirection.LEFT:
                    OnOngoingTiltLeft?.Invoke(tiltMovement);
                    break;
                case TiltDirection.FORWARD:
                    OnOngoingTiltForward?.Invoke(tiltMovement);
                    break;
                case TiltDirection.BACKWARD:
                    OnOngoingTiltBackward?.Invoke(tiltMovement);
                    break;
                case TiltDirection.NONE:
                default:
                    return;
            }
        }


        /// <summary>
        /// Invokes action of an ongoing tilt with the given tilt movement parameters.
        /// </summary>
        /// <param name="tiltMovement">Information about the ongoing tilt movement.</param>
        private static void InvokeOngoingTiltAction(IVectorMovementInputArgs tiltMovement) => OnOngoingTilt?.Invoke(tiltMovement);


        /// <summary>
        /// Invokes action of an ended tilt with the given tilt direction.
        /// </summary>
        /// <param name="direction">Direction of the ended tilt.</param>
        private static void InvokeEndedTiltAction(TiltDirection direction)
        {
            switch (direction)
            {
                case TiltDirection.RIGHT:
                    OnEndedTiltRight?.Invoke();
                    break;
                case TiltDirection.LEFT:
                    OnEndedTiltLeft?.Invoke();
                    break;
                case TiltDirection.FORWARD:
                    OnEndedTiltForward?.Invoke();
                    break;
                case TiltDirection.BACKWARD:
                    OnEndedTiltBackward?.Invoke();
                    break;
                case TiltDirection.NONE:
                default:
                    break;
            }
        }


        /// <summary>
        /// Creates tilt trigger arguments from the device acceleration.
        /// </summary>
        /// <param name="acceleration">Current acceleration of the device.</param>
        /// <returns>Tilt trigger arguments created from the device acceleration.</returns>
        private static TiltTriggerArgs CreateTiltTriggerArs(Vector2 acceleration) => new TiltTriggerArgs(acceleration, Vector2.zero, Vector2.zero);

        #endregion Functions And Methods
    }
}