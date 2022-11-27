/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MonoHelixCurve.cs
 *  Description  :  Define mono curve base on helix curve.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  7/14/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Curve
{
    /// <summary>
    /// Mono curve base on helix curve.
    /// </summary>
    public class MonoHelixCurve : MonoCurve
    {
        /// <summary>
        /// From ellipse args of curve.
        /// </summary>
        public EllipseArgs from = new EllipseArgs(1.0f, 1.0f);

        /// <summary>
        /// To ellipse args of curve.
        /// </summary>
        public EllipseArgs to = new EllipseArgs(2.0f, 2.0f);

        /// <summary>
        /// Altitude from bottom to top.
        /// </summary>
        public float altitude = 2;

        /// <summary>
        /// Around radian.
        /// </summary>
        public float radian = Mathf.PI * 4;

        /// <summary>
        /// Length of mono curve.
        /// </summary>
        public override float Length { get { return length; } }

        /// <summary>
        /// Length of mono curve.
        /// </summary>
        protected float length;

        /// <summary>
        /// Curve for mono curve.
        /// </summary>
        protected override ITimeCurve Curve { get { return curve; } }

        /// <summary>
        /// Curve of mono curve.
        /// </summary>
        protected HelixCurve curve = new HelixCurve();

        /// <summary>
        /// Rebuild mono curve.
        /// </summary>
        public override void Rebuild()
        {
            curve.from = from;
            curve.to = to;
            curve.altitude = altitude;
            curve.radian = radian;
            length = EvaluateLength();
            base.Rebuild();
        }
    }
}