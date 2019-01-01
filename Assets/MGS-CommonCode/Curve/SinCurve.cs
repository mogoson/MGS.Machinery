/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  SinCurve.cs
 *  Description  :  Define sin curve for unity.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  7/21/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using UnityEngine;

namespace Mogoson.Curve
{
    /// <summary>
    /// Args of sin.
    /// </summary>
    [Serializable]
    public struct SinArgs
    {
        #region Field and Property
        /// <summary>
        /// Amplitude of sin.
        /// </summary>
        public float amplitude;

        /// <summary>
        /// Angular of sin.
        /// </summary>
        public float angular;

        /// <summary>
        /// Initial phase of sin.
        /// </summary>
        public float phase;

        /// <summary>
        /// Setover of sin.
        /// </summary>
        public float setover;
        #endregion

        #region Public Method
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="amplitude">Amplitude of sin.</param>
        /// <param name="angular">Angular of sin.</param>
        /// <param name="phase">Initial phase of sin.</param>
        /// <param name="setover">Setover of sin.</param>
        public SinArgs(float amplitude, float angular, float phase, float setover)
        {
            this.amplitude = amplitude;
            this.angular = angular;
            this.phase = phase;
            this.setover = setover;
        }
        #endregion
    }

    /// <summary>
    ///  Sin curve.
    /// </summary>
    public class SinCurve : ICurve
    {
        #region Field and Property
        /// <summary>
        /// Coefficient of delta to lerp key.
        /// </summary>
        protected const float Coefficient = 0.05f;

        /// <summary>
        /// Args of sin curve.
        /// </summary>
        public SinArgs args;

        /// <summary>
        /// Length of sin curve.
        /// </summary>
        public float Length
        {
            get
            {
                var length = 0.0f;
                var delta = MaxKey * Coefficient;
                for (float key = 0; key < MaxKey; key += delta)
                {
                    length += Vector3.Distance(GetPointAt(key), GetPointAt(key + delta));
                }
                return length;
            }
        }

        /// <summary>
        /// Max key of sin curve.
        /// </summary>
        public virtual float MaxKey { set; get; }
        #endregion

        #region Public Method
        /// <summary>
        /// Constructor.
        /// </summary>
        public SinCurve()
        {
            args = new SinArgs();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="args">Args of sin curve.</param>
        public SinCurve(SinArgs args)
        {
            this.args = args;
        }

        /// <summary>
        /// Get point on sin curve at x.
        /// </summary>
        /// <param name="x">Value of x axis.</param>
        /// <returns>The point on sin curve at x.</returns>
        public virtual Vector3 GetPointAt(float x)
        {
            return GetPointAt(args, x);
        }
        #endregion

        #region Static Method
        /// <summary>
        /// Evaluate the value of sin curve at x.
        /// </summary>
        /// <param name="args">Args of sin curve.</param>
        /// <param name="x">Value of x axis.</param>
        /// <returns>The value of sin curve at x.</returns>
        public static float Evaluate(SinArgs args, double x)
        {
            return args.amplitude * (float)Math.Sin(args.angular * x + args.phase) + args.setover;
        }

        /// <summary>
        /// Get point on sin curve at x.
        /// </summary>
        /// <param name="args">Args of sin curve.</param>
        /// <param name="x">Value of x axis.</param>
        /// <returns>The point on sin curve at x.</returns>
        public static Vector3 GetPointAt(SinArgs args, float x)
        {
            return new Vector3(x, Evaluate(args, x));
        }
        #endregion
    }
}