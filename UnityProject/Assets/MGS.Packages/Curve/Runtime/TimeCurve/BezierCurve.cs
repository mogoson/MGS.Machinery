/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  BezierCurve.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  8/18/2021
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using UnityEngine;

namespace MGS.Curve
{
    /// <summary>
    /// Cubic bezier curve.
    /// </summary>
    public class BezierCurve : ITimeCurve
    {
        #region
        /// <summary>
        /// Anchor point of curve from.
        /// </summary>
        public BezierAnchor from;

        /// <summary>
        /// Anchor point of curve to.
        /// </summary>
        public BezierAnchor to;
        #endregion

        #region
        /// <summary>
        /// Constructor.
        /// </summary>
        public BezierCurve() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="from">Anchor point of curve from.</param>
        /// <param name="to">Anchor point of curve to.</param>
        public BezierCurve(BezierAnchor from, BezierAnchor to)
        {
            this.from = from;
            this.to = to;
        }

        /// <summary>
        /// Evaluate the curve at t.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public Vector3 Evaluate(float t)
        {
            return Evaluate(from, to, t);
        }
        #endregion

        #region
        /// <summary>
        /// Evaluate the curve at anchor and time.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Vector3 Evaluate(BezierAnchor from, BezierAnchor to, float t)
        {
            return Mathf.Pow(1 - t, 3) * from.point + 3 * t * Mathf.Pow(1 - t, 2) * from.tangent +
                3 * (1 - t) * Mathf.Pow(t, 2) * to.tangent + Mathf.Pow(t, 3) * to.point;
        }
        #endregion
    }

    /// <summary>
    /// Anchor point of bezier curve.
    /// </summary>
    [Serializable]
    public struct BezierAnchor
    {
        #region
        /// <summary>
        /// Point of anchor position.
        /// </summary>
        public Vector3 point;

        /// <summary>
        /// Point of anchor tangent.
        /// </summary>
        public Vector3 tangent;
        #endregion

        #region
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="point">Point of anchor position.</param>
        /// <param name="tangent">Point of anchor tangent.</param>
        public BezierAnchor(Vector3 point, Vector3 tangent)
        {
            this.point = point;
            this.tangent = tangent;
        }
        #endregion
    }
}