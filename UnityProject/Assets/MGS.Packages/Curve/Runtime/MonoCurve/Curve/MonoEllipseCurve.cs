/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MonoEllipseCurve.cs
 *  Description  :  Define mono curve base on ellipse curve.
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
    /// Mono curve base on ellipse curve.
    /// </summary>
    public class MonoEllipseCurve : MonoCurve
    {
        /// <summary>
        /// Args of ellipse mono curve.
        /// </summary>
        public EllipseArgs args = new EllipseArgs(1, 2);

        /// <summary>
        /// radian of ellipse mono curve.
        /// </summary>
        public float radian = Mathf.PI * 2;

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
        protected EllipseCurve curve = new EllipseCurve();

        /// <summary>
        /// Rebuild mono curve.
        /// </summary>
        public override void Rebuild()
        {
            curve.args = args;
            length = EvaluateLength();
            base.Rebuild();
        }

        /// <summary>
        /// Evaluate local point on mono curve at normalized time int the range[0,1].
        /// </summary>
        /// <param name="t">The normalized time.</param>
        /// <returns>The value of the curve, at the point in time specified.</returns>
        public override Vector3 LocalEvaluateNormalized(float t)
        {
            return base.LocalEvaluateNormalized(radian * t);
        }

        /// <summary>
        /// Evaluate length of mono curve.
        /// </summary>
        /// <returns></returns>
        protected virtual float EvaluateLength()
        {
            var circles = Mathf.Abs(radian) / (Mathf.PI * 2);
            var minor = Mathf.Abs(args.semiMinorAxis);
            var major = Mathf.Abs(args.semiMajorAxis);
            if (minor == 0 || major == 0)
            {
                return 2 * (minor + major) * circles;
            }
            var a = Mathf.Max(minor, major);
            var b = Mathf.Min(minor, major);
            return (2 * Mathf.PI * b + 4 * (a - b)) * circles;
        }
    }
}