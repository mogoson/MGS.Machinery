/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  EllipseHose.cs
 *  Description  :  Render dynamic hose mesh base on ellipse curve.
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
    /// Render dynamic hose mesh base on ellipse curve.
    /// </summary>
    [AddComponentMenu("Mogoson/CurveHose/EllipseHose")]
    public class EllipseHose : MonoCurveHose
    {
        #region Field and Property
        /// <summary>
        /// Semi minor axis of ellipse.
        /// </summary>
        public float semiMinorAxis = 1.0f;

        /// <summary>
        /// Semi major axis of ellipse.
        /// </summary>
        public float semiMajorAxis = 1.5f;

        /// <summary>
        /// Curve for hose.
        /// </summary>
        protected override ICurve Curve { get { return curve; } }

        /// <summary>
        /// Curve of hose.
        /// </summary>
        protected EllipseCurve curve = new EllipseCurve();
        #endregion

        #region Public Method
        /// <summary>
        /// Rebuild hose.
        /// </summary>
        public override void Rebuild()
        {
            curve.args.semiMinorAxis = semiMinorAxis;
            curve.args.semiMajorAxis = semiMajorAxis;
            base.Rebuild();
        }
        #endregion
    }
}