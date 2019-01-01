/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  HelixCurve.cs
 *  Description  :  Define helix curve.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  7/14/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Mogoson.Curve
{
    /// <summary>
    /// Helix curve.
    /// </summary>
    public class HelixCurve : ICurve
    {
        #region Field and Property
        /// <summary>
        /// Coefficient of delta to lerp key.
        /// </summary>
        protected const float Coefficient = 0.05f;

        /// <summary>
        /// Top ellipse args of curve.
        /// </summary>
        public EllipseArgs topEllipse;

        /// <summary>
        /// Bottom ellipse args of curve.
        /// </summary>
        public EllipseArgs bottomEllipse;

        /// <summary>
        /// Length of curve.
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
        /// Max around radian of helix.
        /// </summary>
        public float MaxKey { set; get; }
        #endregion

        #region Public Method
        /// <summary>
        /// Constructor.
        /// </summary>
        public HelixCurve()
        {
            topEllipse = new EllipseArgs();
            bottomEllipse = new EllipseArgs();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="topEllipse">Top ellipse args of curve.</param>
        /// <param name="bottomEllipse">Bottom ellipse args of curve.</param>
        public HelixCurve(EllipseArgs topEllipse, EllipseArgs bottomEllipse)
        {
            this.topEllipse = topEllipse;
            this.bottomEllipse = bottomEllipse;
        }

        /// <summary>
        /// Get point on helix at around radian.
        /// </summary>
        /// <param name="radian">Around radian of helix.</param>
        /// <returns>The point on helix at around radian.</returns>
        public Vector3 GetPointAt(float radian)
        {
            return GetPointAt(topEllipse, bottomEllipse, MaxKey, radian);
        }
        #endregion

        #region Static Method
        /// <summary>
        /// Get point on helix at around radian.
        /// </summary>
        /// <param name="topEllipse">Top ellipse args of curve.</param>
        /// <param name="bottomEllipse">Bottom ellipse args of curve.</param>
        /// <param name="maxRadian">Max around radian of helix.</param>
        /// <param name="radian">Around radian of helix.</param>
        /// <returns>The point on helix at around radian.</returns>
        public static Vector3 GetPointAt(EllipseArgs topEllipse, EllipseArgs bottomEllipse, float maxRadian, float radian)
        {
            if (maxRadian == 0)
            {
                maxRadian = Mathf.Epsilon;
            }
            return Vector3.Lerp(EllipseCurve.GetPointAt(bottomEllipse, radian), EllipseCurve.GetPointAt(topEllipse, radian), radian / maxRadian);
        }
        #endregion
    }
}