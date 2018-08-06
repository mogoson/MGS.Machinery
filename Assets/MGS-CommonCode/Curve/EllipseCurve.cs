/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  EllipseCurve.cs
 *  Description  :  Define ellipse curve.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  7/13/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using UnityEngine;

namespace Mogoson.Curve
{
    /// <summary>
    /// Args of ellipse.
    /// </summary>
    [Serializable]
    public struct EllipseArgs
    {
        #region Field and Property
        /// <summary>
        /// Center of ellipse.
        /// </summary>
        public Vector3 center;

        /// <summary>
        /// Semi minor axis of ellipse.
        /// </summary>
        public float semiMinorAxis;

        /// <summary>
        /// Semi major axis of ellipse.
        /// </summary>
        public float semiMajorAxis;
        #endregion

        #region Public Method
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="center">Center of ellipse.</param>
        /// <param name="semiMinorAxis">Semi minor axis of ellipse.</param>
        /// <param name="semiMajorAxis">Semi major axis of ellipse.</param>
        public EllipseArgs(Vector3 center, float semiMinorAxis, float semiMajorAxis)
        {
            this.center = center;
            this.semiMinorAxis = semiMinorAxis;
            this.semiMajorAxis = semiMajorAxis;
        }
        #endregion
    }

    /// <summary>
    /// Ellipse curve.
    /// </summary>
    public class EllipseCurve : ICurve
    {
        #region Field and Property
        /// <summary>
        /// Args of ellipse curve.
        /// </summary>
        public EllipseArgs args;

        /// <summary>
        /// Length of curve.
        /// </summary>
        public float Length
        {
            get
            {
                var minor = Mathf.Min(args.semiMinorAxis, args.semiMajorAxis);
                var major = Mathf.Max(args.semiMinorAxis, args.semiMajorAxis);
                return 2 * Mathf.PI * minor + 4 * (major - minor);
            }
        }

        /// <summary>
        /// Max around radian of ellipse.
        /// </summary>
        public virtual float MaxKey { get { return 2 * Mathf.PI; } }
        #endregion

        #region Public Method
        /// <summary>
        /// Constructor.
        /// </summary>
        public EllipseCurve()
        {
            args = new EllipseArgs();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="args">Args of ellipse curve.</param>
        public EllipseCurve(EllipseArgs args)
        {
            this.args = args;
        }

        /// <summary>
        /// Get point on ellipse at around radian.
        /// </summary>
        /// <param name="radian">Around radian of ellipse.</param>
        /// <returns>The point on ellipse at around radian.</returns>
        public virtual Vector3 GetPointAt(float radian)
        {
            return GetPointAt(args, radian);
        }
        #endregion

        #region Static Method
        /// <summary>
        /// Get point on ellipse at around radian.
        /// </summary>
        /// <param name="args">Args of ellipse curve.</param>
        /// <param name="radian">Around radian of ellipse.</param>
        /// <returns>The point on ellipse at around radian.</returns>
        public static Vector3 GetPointAt(EllipseArgs args, float radian)
        {
            return args.center + new Vector3(args.semiMinorAxis * Mathf.Cos(radian), 0, args.semiMajorAxis * Mathf.Sin(radian));
        }
        #endregion
    }
}