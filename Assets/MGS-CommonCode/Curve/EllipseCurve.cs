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
    /// Info of ellipse.
    /// </summary>
    [Serializable]
    public struct EllipseInfo
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
        public EllipseInfo(Vector3 center, float semiMinorAxis, float semiMajorAxis)
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
        /// Ellipse info of curve.
        /// </summary>
        public EllipseInfo ellipse;

        /// <summary>
        /// Length of curve.
        /// </summary>
        public float Length
        {
            get
            {
                var minor = Mathf.Min(ellipse.semiMinorAxis, ellipse.semiMajorAxis);
                var major = Mathf.Max(ellipse.semiMinorAxis, ellipse.semiMajorAxis);
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
            ellipse = new EllipseInfo();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="ellipse">Ellipse info of curve.</param>
        public EllipseCurve(EllipseInfo ellipse)
        {
            this.ellipse = ellipse;
        }

        /// <summary>
        /// Get point on ellipse at around radian.
        /// </summary>
        /// <param name="radian">Around radian of ellipse.</param>
        /// <returns>The point on ellipse at around radian.</returns>
        public virtual Vector3 GetPointAt(float radian)
        {
            return GetPointAt(ellipse, radian);
        }
        #endregion

        #region Static Method
        /// <summary>
        /// Get point on ellipse at around radian.
        /// </summary>
        /// <param name="ellipse">Ellipse info of curve.</param>
        /// <param name="radian">Around radian of ellipse.</param>
        /// <returns>The point on ellipse at around radian.</returns>
        public static Vector3 GetPointAt(EllipseInfo ellipse, float radian)
        {
            return ellipse.center + new Vector3(ellipse.semiMinorAxis * Mathf.Cos(radian), 0, ellipse.semiMajorAxis * Mathf.Sin(radian));
        }
        #endregion
    }
}