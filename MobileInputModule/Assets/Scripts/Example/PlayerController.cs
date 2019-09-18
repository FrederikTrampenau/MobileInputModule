// <copyright>(c) Frederik Trampenau 2019.</copyright>
// <author>Frederik Trampenau</author>
#pragma warning disable 0649 //Disable "default value is null" warning

using MIM.Interfaces;
using UnityEngine;

namespace MIM.Example
{
    /// <summary>
    /// Example to demonstrate player controls with the Mobile Input Module.
    /// Functions are called trough the assigned editor unity events.
    /// Some of them are not but the user can assign them if wanted ("Jump" and "Crouch").
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        #region Serialized Fields

        /// <summary>
        /// Speed of the player.
        /// </summary>
        [SerializeField]
        private float speed = 20.0f;

        /// <summary>
        /// Amount of scale to change per scaling.
        /// </summary>
        [SerializeField]
        private Vector3 scaleChangeAmount = new Vector3(0.5f, 0.5f, 0.5f);

        /// <summary>
        /// Max scale of the player object.
        /// </summary>
        [SerializeField]
        private float maxUniformScale = 2.5f;

        /// <summary>
        /// Min scale of the player object.
        /// </summary>
        [SerializeField]
        private float minUniformScale = 0.5f;

        #endregion Serialized Fields



        #region Functions And Methods

        /// <summary>
        /// Moves the player with the given input arguments.
        /// Behaviour between objects with and without rigidbody change.
        /// </summary>
        /// <param name="args">Arguments of the input event.</param>
        public void Move(IPositionalInputArgs args)
        {
            if (args is IVectorMovementInputArgs movementInputArgs)
            {
                if (GetComponent<Rigidbody>())
                    GetComponent<Rigidbody>().AddForce(movementInputArgs.MovementVector * Time.deltaTime * speed, ForceMode.VelocityChange);
                else
                    transform.Translate(movementInputArgs.MovementVector * Time.deltaTime * speed);
            }
        }


        /// <summary>
        /// Character jumps, visualized for debugging by increasing its scale.
        /// </summary>
        public void Jump() => Grow();


        /// <summary>
        /// Character crouches, visualized for debugging by decreasing its scale.
        /// </summary>
        public void Crouch() => Shrink();


        /// <summary>
        /// Lets the player leap to the left.
        /// </summary>
        public void LeapLeft() => transform.Translate(Vector3.left * speed * 5.0f);


        /// <summary>
        /// Lets the player leap to the right.
        /// </summary>
        public void LeapRight() => transform.Translate(Vector3.right * speed * 5.0f);


        /// <summary>
        /// Increases size of the player.
        /// </summary>
        public void Grow()
        {
            var grownScale = gameObject.transform.localScale + scaleChangeAmount;
            // Doesn't work for every axis scale (only x) but it's enough for debugging since I am using a uniform scale
            gameObject.transform.localScale = grownScale.x <= maxUniformScale ? grownScale : gameObject.transform.localScale;
        }


        /// <summary>
        /// Decreases size of the player.
        /// </summary>
        public void Shrink()
        {
            var shrunkenScale = gameObject.transform.localScale - scaleChangeAmount;
            // Doesn't work for every axis scale (only x) but it's enough for debugging since I am using a uniform scale
            gameObject.transform.localScale = shrunkenScale.x >= minUniformScale ? shrunkenScale : gameObject.transform.localScale;
        }

        #endregion Functions And Methods
    }
}