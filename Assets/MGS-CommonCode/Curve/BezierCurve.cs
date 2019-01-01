/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  BezierCurve.cs
 *  Description  :  Define bezier curve.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  1/7/2017
 *  Description  :  Initial development version.
 *  
 *  Author       :  Mogoson
 *  Version      :  0.1.1
 *  Date         :  2/28/2018
 *  Description  :  Add static method GetPoint.
 *************************************************************************/

using System;
using UnityEngine;

namespace Mogoson.Curve
{
    /// <summary>
    /// Anchor points of linear bezier curve.
    /// </summary>
    [Serializable]
    public struct LinearBezierAnchor
    {
        #region Field and Property
        /// <summary>
        /// Start point of curve.
        /// </summary>
        public Vector3 start;

        /// <summary>
        /// End point of curve.
        /// </summary>
        public Vector3 end;
        #endregion

        #region Public Method
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="start">Start point of curve.</param>
        /// <param name="end">End point of curve.</param>
        public LinearBezierAnchor(Vector3 start, Vector3 end)
        {
            this.start = start;
            this.end = end;
        }
        #endregion
    }

    /// <summary>
    /// Anchor points of quadratic bezier curve.
    /// </summary>
    [Serializable]
    public struct QuadraticBezierAnchor
    {
        #region Field and Property
        /// <summary>
        /// Start point of curve.
        /// </summary>
        public Vector3 start;

        /// <summary>
        /// End point of curve.
        /// </summary>
        public Vector3 end;

        /// <summary>
        /// Tangent point of curve.
        /// </summary>
        public Vector3 tangent;
        #endregion

        #region Public Method
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="start">Start point of curve.</param>
        /// <param name="end">End point of curve.</param>
        /// <param name="tangent">Tangent point of curve.</param>
        public QuadraticBezierAnchor(Vector3 start, Vector3 end, Vector3 tangent)
        {
            this.start = start;
            this.end = end;
            this.tangent = tangent;
        }
        #endregion
    }

    /// <summary>
    /// Anchor points of cubic bezier curve.
    /// </summary>
    [Serializable]
    public struct CubicBezierAnchor
    {
        #region Field and Property
        /// <summary>
        /// Start point of curve.
        /// </summary>
        public Vector3 start;

        /// <summary>
        /// End point of curve.
        /// </summary>
        public Vector3 end;

        /// <summary>
        /// Start tangent point of curve.
        /// </summary>
        public Vector3 startTangent;

        /// <summary>
        /// End tangent point of curve.
        /// </summary>
        public Vector3 endTangent;
        #endregion

        #region Public Method
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="start">Start point of curve.</param>
        /// <param name="end">End point of curve.</param>
        /// <param name="startTangent">Start tangent point of curve.</param>
        /// <param name="endTangent">End tangent point of curve.</param>
        public CubicBezierAnchor(Vector3 start, Vector3 end, Vector3 startTangent, Vector3 endTangent)
        {
            this.start = start;
            this.end = end;
            this.startTangent = startTangent;
            this.endTangent = endTangent;
        }
        #endregion
    }

    /// <summary>
    /// Bezier curve.
    /// </summary>
    public abstract class BezierCurve : ICurve
    {
        #region Field and Property
        /// <summary>
        /// Delta to lerp key.
        /// </summary>
        protected const float Delta = 0.05f;

        /// <summary>
        /// Length of curve.
        /// </summary>
        public float Length
        {
            get
            {
                var length = 0.0f;
                for (float key = 0; key < MaxKey; key += Delta)
                {
                    length += Vector3.Distance(GetPointAt(key), GetPointAt(key + Delta));
                }
                return length;
            }
        }

        /// <summary>
        /// Max key of curve.
        /// </summary>
        public float MaxKey { get { return 1.0f; } }
        #endregion

        #region Public Method
        /// <summary>
        /// Get point on curve at key.
        /// </summary>
        /// <param name="key">Key is in the range(0~1).</param>
        /// <returns>The point on curve at key.</returns>
        public abstract Vector3 GetPointAt(float key);
        #endregion
    }

    /// <summary>
    /// Linear bezier curve.
    /// </summary>
    public class LinearBezierCurve : BezierCurve
    {
        #region Field and Property
        /// <summary>
        /// Anchor points of curve.
        /// </summary>
        public LinearBezierAnchor anchor;
        #endregion

        #region Public Method
        /// <summary>
        /// Constructor.
        /// </summary>
        public LinearBezierCurve()
        {
            anchor = new LinearBezierAnchor();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="anchor">Anchor points of curve.</param>
        public LinearBezierCurve(LinearBezierAnchor anchor)
        {
            this.anchor = anchor;
        }

        /// <summary>
        /// Get point on curve at key.
        /// </summary>
        /// <param name="key">Key is in the range(0~1).</param>
        /// <returns>The point on curve at key.</returns>
        public override Vector3 GetPointAt(float key)
        {
            return GetPointAt(anchor, key);
        }
        #endregion

        #region Static Method
        /// <summary>
        /// Get curve point base on anchor points and key.
        /// </summary>
        /// <param name="anchor">Anchor points of curve.</param>
        /// <param name="key">Key is in the range(0~1).</param>
        /// <returns>Point on curve.</returns>
        public static Vector3 GetPointAt(LinearBezierAnchor anchor, float key)
        {
            return (1 - key) * anchor.start + key * anchor.end;
        }
        #endregion
    }

    /// <summary>
    /// Quadratic bezier curve.
    /// </summary>
    public class QuadraticBezierCurve : BezierCurve
    {
        #region Field and Property
        /// <summary>
        /// Anchor points of curve.
        /// </summary>
        public QuadraticBezierAnchor anchor;
        #endregion

        #region Public Method
        /// <summary>
        /// Constructor.
        /// </summary>
        public QuadraticBezierCurve()
        {
            anchor = new QuadraticBezierAnchor();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="anchor">Anchor points of curve.</param>
        public QuadraticBezierCurve(QuadraticBezierAnchor anchor)
        {
            this.anchor = anchor;
        }

        /// <summary>
        /// Get point on curve at key.
        /// </summary>
        /// <param name="key">Key is in the range(0~1).</param>
        /// <returns>The point on curve at key.</returns>
        public override Vector3 GetPointAt(float key)
        {
            return GetPointAt(anchor, key);
        }
        #endregion

        #region Static Method
        /// <summary>
        /// Get curve point base on anchor points and key.
        /// </summary>
        /// <param name="anchor">Anchor points of curve.</param>
        /// <param name="key">Key is in the range(0~1).</param>
        /// <returns>Point on curve.</returns>
        public static Vector3 GetPointAt(QuadraticBezierAnchor anchor, float t)
        {
            return Mathf.Pow(1 - t, 2) * anchor.start + 2 * t * (1 - t) * anchor.tangent + Mathf.Pow(t, 2) * anchor.end;
        }
        #endregion
    }

    /// <summary>
    /// Cubic bezier curve.
    /// </summary>
    public class CubicBezierCurve : BezierCurve
    {
        #region Field and Property
        /// <summary>
        /// Anchor points of curve.
        /// </summary>
        public CubicBezierAnchor anchor;
        #endregion

        #region Public Method
        /// <summary>
        /// Constructor.
        /// </summary>
        public CubicBezierCurve()
        {
            anchor = new CubicBezierAnchor();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="anchor">Anchor points of curve.</param>
        public CubicBezierCurve(CubicBezierAnchor anchor)
        {
            this.anchor = anchor;
        }

        /// <summary>
        /// Get point on curve at key.
        /// </summary>
        /// <param name="key">Key is in the range(0~1).</param>
        /// <returns>The point on curve at key.</returns>
        public override Vector3 GetPointAt(float key)
        {
            return GetPointAt(anchor, key);
        }
        #endregion

        #region Static Method
        /// <summary>
        /// Get curve point base on anchor points and key.
        /// </summary>
        /// <param name="anchor">Anchor points of curve.</param>
        /// <param name="key">Key is in the range(0~1).</param>
        /// <returns>Point on curve.</returns>
        public static Vector3 GetPointAt(CubicBezierAnchor anchor, float key)
        {
            return Mathf.Pow(1 - key, 3) * anchor.start + 3 * key * Mathf.Pow(1 - key, 2) * anchor.startTangent +
                3 * (1 - key) * Mathf.Pow(key, 2) * anchor.endTangent + Mathf.Pow(key, 3) * anchor.end;
        }
        #endregion
    }
}