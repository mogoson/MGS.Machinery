/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MonoSinCurve.cs
 *  Description  :  Define mono curve base on sin curve.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  7/21/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Curve
{
    /// <summary>
    /// Mono curve base on sin curve.
    /// </summary>
    public class MonoSinCurve : MonoCurve
    {
        /// <summary>
        /// Args of sin mono curve.
        /// </summary>
        public SinArgs args = new SinArgs(1, 1, 0);

        /// <summary>
        /// radian of sin mono curve.
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
        protected SinCurve curve = new SinCurve();

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
            if (args.angular == 0)
            {
                return radian;
            }

            var halfPI = Mathf.PI * 0.5f;
            var angularAbs = Mathf.Abs(args.angular);
            var piece = Vector2.Distance(Vector2.zero, new Vector2(halfPI / angularAbs, args.amplitude));
            var pieces = piece * angularAbs;
            var segments = Mathf.RoundToInt(radian / halfPI);
            return pieces * segments;
        }
    }
}