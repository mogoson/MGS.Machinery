/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  SinCurve.cs
 *  Description  :  Define sin curve for unity.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  7/21/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using UnityEngine;

namespace MGS.Curve
{
    /// <summary>
    ///  Sin curve.
    /// </summary>
    public class SinCurve : ITimeCurve
    {
        #region
        /// <summary>
        /// Args of curve.
        /// </summary>
        public SinArgs args;

        /// <summary>
        /// Constructor.
        /// </summary>
        public SinCurve() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="args">Args of curve.</param>
        public SinCurve(SinArgs args)
        {
            this.args = args;
        }

        /// <summary>
        /// Evaluate the curve at x.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public virtual Vector3 Evaluate(float x)
        {
            return Evaluate(args, x);
        }
        #endregion

        #region
        /// <summary>
        /// Evaluate the curve at x.
        /// </summary>
        /// <param name="args"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Vector2 Evaluate(SinArgs args, float x)
        {
            var y = Evaluate(args.amplitude, args.angular, args.phase, x);
            return new Vector2(x, y);
        }

        /// <summary>
        /// Evaluate the curve at x.
        /// </summary>
        /// <param name="am">Amplitude.</param>
        /// <param name="an">Angular.</param>
        /// <param name="ph">Initial phase.</param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static float Evaluate(float am, float an, float ph, float x)
        {
            return am * (float)Math.Sin(an * x + ph);
        }
        #endregion
    }

    /// <summary>
    /// Args of sin.
    /// </summary>
    [Serializable]
    public struct SinArgs
    {
        #region
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
        #endregion

        #region
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="amplitude">Amplitude of sin.</param>
        /// <param name="angular">Angular of sin.</param>
        /// <param name="phase">Initial phase of sin.</param>
        public SinArgs(float amplitude, float angular, float phase)
        {
            this.amplitude = amplitude;
            this.angular = angular;
            this.phase = phase;
        }
        #endregion
    }
}