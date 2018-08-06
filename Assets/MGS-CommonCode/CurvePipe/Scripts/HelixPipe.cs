/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  HelixPipe.cs
 *  Description  :  Render dynamic pipe mesh base on helix curve.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  7/18/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using Mogoson.Curve;
using UnityEngine;

namespace Mogoson.CurvePipe
{
    /// <summary>
    /// Render dynamic pipe mesh base on helix curve.
    /// </summary>
    [AddComponentMenu("Mogoson/CurvePipe/HelixPipe")]
    public class HelixPipe : MonoCurvePipe
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
        /// Curve for pipe.
        /// </summary>
        protected override ICurve Curve { get { return curve; } }

        /// <summary>
        /// Curve of pipe.
        /// </summary>
        protected HelixCurve curve = new HelixCurve();
        #endregion

        #region Public Method
        /// <summary>
        /// Rebuild pipe.
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