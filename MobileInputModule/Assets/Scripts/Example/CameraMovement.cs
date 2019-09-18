// <copyright>(c) Frederik Trampenau 2019.</copyright>
// <author>Frederik Trampenau</author>
#pragma warning disable 0649 //Disable "default value is null" warning

using MIM.Interfaces;
using MIM.Utility;
using UnityEngine;

namespace MIM.Example
{
    /// <summary>
    /// Example to demonstrate camera movement with the Mobile Input Module.
    /// Functions are called trough subsciption to the input events from the Mobile Input class.
    /// </summary>
    [RequireComponent(typeof(Camera))]
    public class CameraMovement : MonoBehaviour
    {
        #region Serialized Fields

        /// <summary>
        /// Speed of the zoom.
        /// </summary>
        [SerializeField]
        float zoomSpeed = 0.25f;

        /// <summary>
        /// Minimal zoom of the camera.
        /// </summary>
        [SerializeField]
        float minOrtographicCameraZoom = 1.0f;

        /// <summary>
        /// Maximal zoom of the camera.
        /// </summary>
        [SerializeField]
        float maxOrtographicCameraZoom = 10.0f;

        /// <summary>
        /// Minimal distance of the perspective camera on the z-Axis.
        /// </summary>
        [SerializeField]
        private float minPerspectiveCameraDistance = -1f;

        /// <summary>
        /// Maximal distance of the perspective camera on the z-Axis.
        /// </summary>
        [SerializeField]
        private float maxPerspectiveCameraDistance = -20f;

        #endregion Serialized Fields



        #region Private Fields

        /// <summary>
        /// Camera which should be moved by the script.
        /// </summary>
        private Camera cameraToMove;

        #endregion Private Fields



        #region Unity Functions

        /// <summary>
        /// Calls the setup of the camera movement.
        /// </summary>
        private void Start() => Setup();

        #endregion Unity Functions



        #region Functions And Methods

        /// <summary>
        /// Gets the camera component and subscribes to the pinch input.
        /// </summary>
        private void Setup()
        {
            cameraToMove = GetComponent<Camera>();
            MobileInput.OnPinchMoving += Zoom;
        }


        /// <summary>
        /// Zooms the camera. Behaviour between ortographic and perspective cameras change.
        /// </summary>
        /// <param name="args">Arguments of the input event.</param>
        public void Zoom(IPositionalInputArgs args)
        {
            var deltaMovementArgs = MobileInputHelper.CastToDeltaMovementArgs(args);

            if (cameraToMove.orthographic)
                ZoomOrtographic(deltaMovementArgs);
            else
                ZoomPerspective(deltaMovementArgs);
        }


        /// <summary>
        /// Handle zoom of an ortographic camera.
        /// </summary>
        /// <param name="deltaMovementArgs">Delta movement arguments of the input.</param>
        private void ZoomOrtographic(IDeltaMovementInputArgs deltaMovementArgs)
        {
            if (deltaMovementArgs != null)
                cameraToMove.orthographicSize -= deltaMovementArgs.DeltaMovement * Time.deltaTime * zoomSpeed;

            cameraToMove.orthographicSize = cameraToMove.orthographicSize >= minOrtographicCameraZoom ? cameraToMove.orthographicSize : minOrtographicCameraZoom;
            cameraToMove.orthographicSize = cameraToMove.orthographicSize >= maxOrtographicCameraZoom ? maxOrtographicCameraZoom : cameraToMove.orthographicSize;
        }


        /// <summary>
        /// Handle zoom of a perspective camera.
        /// </summary>
        /// <param name="deltaMovementArgs">Delta movement arguments of the input.</param>
        private void ZoomPerspective(IDeltaMovementInputArgs deltaMovementArgs)
        {
            if (deltaMovementArgs == null) return;

            var zoomedPosition = cameraToMove.transform.position;

            // Get expected position
            zoomedPosition -= Vector3.forward * -(deltaMovementArgs.DeltaMovement * Time.deltaTime * zoomSpeed);

            // Keep camera position in boundary
            zoomedPosition = zoomedPosition.z <= minPerspectiveCameraDistance ? zoomedPosition : Vector3.forward * minPerspectiveCameraDistance;
            zoomedPosition = zoomedPosition.z >= maxPerspectiveCameraDistance ? zoomedPosition : Vector3.forward * maxPerspectiveCameraDistance;

            cameraToMove.transform.position = zoomedPosition;
        }

        #endregion Functions And Methods
    }
}