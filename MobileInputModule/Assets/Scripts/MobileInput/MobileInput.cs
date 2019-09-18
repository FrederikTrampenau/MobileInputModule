// <copyright>(c) Frederik Trampenau 2019.</copyright>
// <author>Frederik Trampenau</author>
#pragma warning disable 0649 //Disable "default value is null" warning

using MIM.InputTriggers;
using MIM.Interfaces;
using MIM.Utility;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MIM
{
    /// <summary>
    /// Contains positions of a touch history.
    /// </summary>
    public class TouchHistory
    {
        #region Public Fields

        /// <summary>
        /// Container for the touch position history sorted by time stamps.
        /// </summary>
        public SortedDictionary<float, Vector2> trace = new SortedDictionary<float, Vector2>();

        #endregion Public Fields



        #region Functions And Methods

        /// <summary>
        /// Constructor of the touch history.
        /// </summary>
        /// <param name="timeStamp">First time stamp of the touch history.</param>
        /// <param name="startPosition">First position of the touch history.</param>
        public TouchHistory(float timeStamp, Vector2 startPosition)
        {
            trace.Add(timeStamp, startPosition);
        }

        #endregion Functions And Methods
    }


    /// <summary>
    /// Checks for mobile input and calls their evaluation. Contains events for mobile input.
    /// </summary>
    [AddComponentMenu("Event/Standalone Input Module")]
    public class MobileInput : Attribute
    {
        #region Private Fields

        /// <summary>
        /// Dictionary which contains the touch history per finger (finger id).
        /// </summary>
        private static Dictionary<int, TouchHistory> touchHistory = new Dictionary<int, TouchHistory>();


        /// <summary>
        /// List which contains the acceleration history.
        /// </summary>
        private static List<Vector2> accelerationHistory = new List<Vector2>();

        #endregion Private Fields



        #region Properties

        /// <summary>
        /// Returns the current rotation rate of the device as a vector if it supports a gyroscope.
        /// Returns a zero vector if the device doesn't support a gyroscope.
        /// </summary>
        /// <returns>Current rotation rate of the device as a vector.</returns>
        public static Vector3 GyroscopeRotation => SystemInfo.supportsGyroscope ? Input.gyro.rotationRate : Vector3.zero;


        /// <summary>
        /// Returns the current acceleration of the device as a vector if it supports an accelerometer.
        /// Returns a zero vector if the device doesn't support an accelerometer.
        /// </summary>
        /// <returns>Current acceleration of the device as a vector.</returns>
        public static Vector3 DeviceAcceleration
        {
            get
            {
                if (SystemInfo.supportsAccelerometer)
                {
                    var acceleration = Input.acceleration;

                    acceleration = MathUtility.RoundVector(acceleration, 2);

                    return acceleration;
                }

                return Vector3.zero;
            }
        }


        /// <summary>
        /// Returns the amount of touches on the device if it supports touch.
        /// Returns 0 if the device doesn't support touch.
        /// </summary>
        /// <returns>Current amount of touches the device registers.</returns>
        public static int TouchAmount => Input.touchSupported ? Input.touchCount : 0;

        #endregion Properties



        #region Events

        // Argument Triggers \\

        /// <summary>
        /// Gets called when a tap ends.
        /// </summary>
        public static Action<IPositionalInputArgs> OnTap;

        /// <summary>
        /// Gets called while a swipe is happening.
        /// </summary>
        public static Action<IPositionalInputArgs> OnSwipeMoving;

        /// <summary>
        /// Gets called when a swipe ends.
        /// </summary>
        public static Action<IPositionalInputArgs> OnSwipeEnded;

        /// <summary>
        /// Gets called while a pinch is happening.
        /// </summary>
        public static Action<IPositionalInputArgs> OnPinchMoving;

        /// <summary>
        /// Gets called while a tilt is happening.
        /// </summary>
        public static Action<IPositionalInputArgs> OnOngoingTilt;

        /// <summary>
        /// Gets called while a left tilt is happening.
        /// </summary>
        public static Action<IPositionalInputArgs> OnOngoingTiltLeft;

        /// <summary>
        /// Gets called while a right tilt is happening.
        /// </summary>
        public static Action<IPositionalInputArgs> OnOngoingTiltRight;

        /// <summary>
        /// Gets called while a forward tilt is happening.
        /// </summary>
        public static Action<IPositionalInputArgs> OnOngoingTiltForward;

        /// <summary>
        /// Gets called while a backward tilt is happening.
        /// </summary>
        public static Action<IPositionalInputArgs> OnOngoingTiltBackward;


        // Action Triggers \\

        /// <summary>
        /// Gets called when a left swipe happens.
        /// </summary>
        public static Action OnSwipeLeft;

        /// <summary>
        /// Gets called when a right swipe happens.
        /// </summary>
        public static Action OnSwipeRight;

        /// <summary>
        /// Gets called when an up swipe happens.
        /// </summary>
        public static Action OnSwipeUp;

        /// <summary>
        /// Gets called when a down swipe happens.
        /// </summary>
        public static Action OnSwipeDown;

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
        /// Make the constructor private to prevent multiple instances.
        /// </summary>
        private MobileInput() { }


        /// <summary>
        /// Process the input information.
        /// </summary>
        public static void Process()
        {
            TrackDeviceAcceleration();
            CheckTiltInputEvents();

            TrackTouches();
            CheckTouchInputEvents();
        }


        /// <summary>
        /// Tracks any device accelerations so they can be evaluated.
        /// </summary>
        private static void TrackDeviceAcceleration()
        {
            if (accelerationHistory.Count <= 0 && (DeviceAcceleration.x != 0 || DeviceAcceleration.y != 0))
            {
                accelerationHistory.Add(DeviceAcceleration);
            }
        }


        /// <summary>
        /// Checks input for tilt events and calls its evaluation.
        /// </summary>
        private static void CheckTiltInputEvents() => TiltEventDetector.CallOngoingTiltEvaluation(DeviceAcceleration);


        /// <summary>
        /// Tracks and adds touch data to the entires of the touch history.
        /// </summary>
        private static void TrackTouches()
        {
            foreach (var touch in Input.touches)
            {
                if (touchHistory.ContainsKey(touch.fingerId))
                {
                    touchHistory[touch.fingerId].trace.Add(Time.realtimeSinceStartup, touch.position);
                }
            }
        }


        /// <summary>
        /// Checks if a swipe was done and invokes the action with the direction and distance.
        /// </summary>
        private static void CheckTouchInputEvents()
        {
            foreach (var touch in Input.touches)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        touchHistory.Add(touch.fingerId, new TouchHistory(Time.realtimeSinceStartup, touch.position));
                        break;
                    case TouchPhase.Ended:
                        CallEndedTouchEvaluations(touch);
                        touchHistory.Remove(touch.fingerId);
                        break;
                    case TouchPhase.Canceled:
                        touchHistory.Remove(touch.fingerId);
                        break;
                    case TouchPhase.Moved:
                        CallOngoingTouchEvaluations(touch);
                        break;
                    default:
                        break;
                }
            }

            // Evaluate pinch
            if (TouchAmount == 2)
                PinchEventDetector.CallOngoingPinchEvaluation((Input.GetTouch(0), Input.GetTouch(1)));
        }


        /// <summary>
        /// Calls ongoing touch actions.
        /// </summary>
        /// <param name="touch">Touch information of the ongoing touch.</param>
        private static void CallOngoingTouchEvaluations(Touch touch) => SwipeEventDetector.CallOngoingSwipe(touch);


        /// <summary>
        /// Tells all touch input detectors to evaluate the ended touch.
        /// </summary>
        /// <param name="touch">Touch information of the ended touch.</param>
        private static void CallEndedTouchEvaluations(Touch touch)
        {
            TapEventDetector.CallTouchHistoryEvaluation(touchHistory[touch.fingerId]);
            SwipeEventDetector.CallTouchHistoryEvaluation(touchHistory[touch.fingerId]);
        }

        #endregion Functions And Methods
    }
}