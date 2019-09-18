// <copyright>(c) Frederik Trampenau 2019.</copyright>
// <author>Frederik Trampenau</author>
#pragma warning disable 0649 //Disable "default value is null" warning

using MIM.Interfaces;
using UnityEngine;

namespace MIM.Utility
{
    /// <summary>
    /// Helper class for handling arguments of mobile input.
    /// </summary>
    public class MobileInputHelper
    {
        #region Functions And Methods

        /// <summary>
        /// Casts the input arguments into delta movement arguments.
        /// </summary>
        /// <param name="inputArgs">Input arguments of type IPositionalInputArgs.</param>
        /// <returns>Input args parsed into delta movement args. Null if parsing failed.</returns>
        public static IDeltaMovementInputArgs CastToDeltaMovementArgs(IPositionalInputArgs inputArgs) => inputArgs as IDeltaMovementInputArgs;


        /// <summary>
        /// Casts the input arguments into vector movement arguments.
        /// </summary>
        /// <param name="inputArgs">Input arguments of type IPositionalInputArgs.</param>
        /// <returns>Input args parsed into vector movement args. Null if parsing failed.</returns>
        public static IVectorMovementInputArgs CastToVectorMovementArgs(IPositionalInputArgs inputArgs) => inputArgs as IVectorMovementInputArgs;


        /// <summary>
        /// Casts the input arguments into intensity arguments.
        /// </summary>
        /// <param name="inputArgs">Input arguments of type IPositionalInputArgs.</param>
        /// <returns>Input args parsed into intensity args. Null if parsing failed.</returns>
        public static IIntensityTriggerArgs CastToIntensityArgs(IPositionalInputArgs inputArgs) => inputArgs as IIntensityTriggerArgs;


        /// <summary>
        /// Gets direction of the positional input. Will be a zero vector most of the time, use overload with IVectorMovementInputArgs instead.
        /// </summary>
        /// <param name="args">Arguments of the input event.</param>
        /// <returns>Direction of the input.</returns>
        public static Vector2 GetDirection(IPositionalInputArgs args) => args.EndPos - args.StartPos;


        /// <summary>
        /// Gets direction of the positional input multiplied with the delta movement. Will be a zero vector most of the time, use overload with IVectorMovementInputArgs instead.
        /// </summary>
        /// <param name="args">Arguments of the input event.</param>
        /// <returns>Direction of the input.</returns>
        public static Vector2 GetDirection(IDeltaMovementInputArgs args) => (args.EndPos - args.StartPos) * args.DeltaMovement;


        /// <summary>
        /// Gets direction of the positional input multiplied with the intensity. Will be a zero vector most of the time, use overload with IVectorMovementInputArgs instead.
        /// </summary>
        /// <param name="args">Arguments of the input event.</param>
        /// <returns>Direction of the input.</returns>
        public static Vector2 GetDirection(IIntensityTriggerArgs args) => (args.EndPos - args.StartPos) * args.Intensity;


        /// <summary>
        /// Gets direction of the vector input.
        /// </summary>
        /// <param name="args">Arguments of the input event.</param>
        /// <returns>Direction of the input.</returns>
        public static Vector2 GetDirection(IVectorMovementInputArgs args) => args.MovementVector;


        /// <summary>
        /// Gets magnitude of the positional input. Will be zero most of the time, use overload with IDeltaMovementInputArgs or IVectorMovementInputArgs instead.
        /// </summary>
        /// <param name="args">Arguments of the input event.</param>
        /// <returns>Magnitude of the input.</returns>
        public static float GetMagnitude(IPositionalInputArgs args) => GetDirection(args).magnitude;


        /// <summary>
        /// Gets magnitude of the delta movement input.
        /// </summary>
        /// <param name="args">Arguments of the input event.</param>
        /// <returns>Magnitude of the input.</returns>
        public static float GetMagnitude(IDeltaMovementInputArgs args) => args.DeltaMovement;


        /// <summary>
        /// Gets magnitude of the positional input multiplied with the intensity. Will be zero most of the time, use overload with IDeltaMovementInputArgs or IVectorMovementInputArgs instead.
        /// </summary>
        /// <param name="args">Arguments of the input event.</param>
        /// <returns>Magnitude of the input.</returns>
        public static float GetMagnitude(IIntensityTriggerArgs args) => GetDirection(args).magnitude * args.Intensity;


        /// <summary>
        /// Gets magnitude of the vector input.
        /// </summary>
        /// <param name="args">Arguments of the input event.</param>
        /// <returns>Magnitude of the input.</returns>
        public static float GetMagnitude(IVectorMovementInputArgs args) => args.MovementVector.magnitude;


        /// <summary>
        /// Gets position between start and end position.
        /// </summary>
        /// <param name="args">Arguments of the input event.</param>
        /// <returns>Focus point of the input.</returns>
        public static Vector2 GetFocusPoint(IPositionalInputArgs args) => args.StartPos + GetDirection(args) * GetMagnitude(args);


        /// <summary>
        /// /// Gets start position added with the direction multiplied by the delta movement of the input.
        /// </summary>
        /// <param name="args">Arguments of the input event.</param>
        /// <returns>Focus point of the input.</returns>
        public static Vector2 GetFocusPoint(IDeltaMovementInputArgs args) => args.StartPos + GetDirection(args) * args.DeltaMovement;


        /// <summary>
        /// Gets start position added with the direction multiplied by the intensity of the input.
        /// </summary>
        /// <param name="args">Arguments of the input event.</param>
        /// <returns>Focus point of the input.</returns>
        public static Vector2 GetFocusPoint(IIntensityTriggerArgs args) => args.StartPos + GetDirection(args) * args.Intensity;


        /// <summary>
        /// Gets position at the middle of the vector input.
        /// </summary>
        /// <param name="args">Arguments of the input event.</param>
        /// <returns>Focus point of the input.</returns>
        public static Vector2 GetFocusPoint(IVectorMovementInputArgs args) => args.StartPos + args.MovementVector * (GetMagnitude(args) / 2.0f);


        #endregion Functions And Methods
    }
}