/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  HelixHose.cs
 *  Description  :  Render dynamic hose mesh base on helix curve.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  7/18/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using Mogoson.Curve;
using UnityEngine;

namespace Mogoson.CurveHose
{
    /// <summary>
    /// Render dynamic hose mesh base on helix curve.
    /// </summary>
    [AddComponentMenu("Mogoson/CurveHose/HelixHose")]
    public class HelixHose : MonoCurveHose
    {
        #region Field and Property
        /// <summary>
        /// Top ellipse args of curve.
        /// </summary>
        public EllipseArgs topEllipse = new EllipseArgs(Vector3.up, 1.0f, 1.0f);

        /// <summary>
        /// Bottom ellipse args of curve.
        /// </summary>
        public EllipseArgs bottomEllipse = new EllipseArgs(Vector3.zero, 1.0f, 1.0f);

        /// <summary>
        /// Max around radian of helix.
        /// </summary>
        public float maxRadian = 6 * Mathf.PI;

        /// <summary>
        /// Curve for hose.
        /// </summary>
        protected override ICurve Curve { get { return curve; } }

        /// <summary>
        /// Curve of hose.
        /// </summary>
        protected HelixCurve curve = new HelixCurve();
        #endregion

        #region Public Method
        /// <summary>
        /// Rebuild hose.
        /// </summary>
        public override void Rebuild()
        {
            curve.topEllipse = topEllipse;
            curve.bottomEllipse = bottomEllipse;
            curve.MaxKey = maxRadian;
            base.Rebuild();
        }
        #endregion
    }
}