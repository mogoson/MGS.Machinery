/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  EllipseCurve.cs
 *  Description  :  Define ellipse curve.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  7/13/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using UnityEngine;

namespace MGS.Curve
{
    /// <summary>
    /// Ellipse curve.
    /// </summary>
    public class EllipseCurve : ITimeCurve
    {
        #region
        /// <summary>
        /// Args of curve.
        /// </summary>
        public EllipseArgs args;

        /// <summary>
        /// Constructor.
        /// </summary>
        public EllipseCurve() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="args">Args of ellipse curve.</param>
        public EllipseCurve(EllipseArgs args)
        {
            this.args = args;
        }

        /// <summary>
        /// Evaluate the curve at radian.
        /// </summary>
        /// <param name="r">Around radian.</param>
        /// <returns></returns>
        public virtual Vector3 Evaluate(float r)
        {
            return Evaluate(args, r);
        }
        #endregion

        #region
        /// <summary>
        /// Evaluate the curve at radian.
        /// </summary>
        /// <param name="args"></param>
        /// <param name="r">Radian.</param>
        /// <returns></returns>
        public static Vector2 Evaluate(EllipseArgs args, float r)
        {
            return Evaluate(args.semiMinorAxis, args.semiMajorAxis, r);
        }

        /// <summary>
        /// Evaluate the curve at radian.
        /// </summary>
        /// <param name="mi"></param>
        /// <param name="ma"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Vector2 Evaluate(float mi, float ma, float r)
        {
            return new Vector2(mi * Mathf.Cos(r), ma * Mathf.Sin(r));
        }
        #endregion
    }

    /// <summary>
    /// Args of ellipse.
    /// </summary>
    [Serializable]
    public struct EllipseArgs
    {
        #region
        /// <summary>
        /// Semi minor axis of ellipse.
        /// </summary>
        public float semiMinorAxis;

        /// <summary>
        /// Semi major axis of ellipse.
        /// </summary>
        public float semiMajorAxis;
        #endregion

        #region
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="semiMinorAxis">Semi minor axis of ellipse.</param>
        /// <param name="semiMajorAxis">Semi major axis of ellipse.</param>
        public EllipseArgs(float semiMinorAxis, float semiMajorAxis)
        {
            this.semiMinorAxis = semiMinorAxis;
            this.semiMajorAxis = semiMajorAxis;
        }
        #endregion
    }
}