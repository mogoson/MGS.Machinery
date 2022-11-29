/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MonoBezierCurve.cs
 *  Description  :  Define mono curve base on cubic bezier curve.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  2/28/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Curve
{
    /// <summary>
    /// Mono curve base on cubic bezier curve.
    /// </summary>
    public class MonoBezierCurve : MonoCurve
    {
        /// <summary>
        /// Anchor point of mono curve from.
        /// </summary>
        [SerializeField]
        [HideInInspector]
        protected internal BezierAnchor from = new BezierAnchor(new Vector3(1, 1), new Vector3(0, 1));

        /// <summary>
        /// Anchor point of mono curve to.
        /// </summary>
        [SerializeField]
        [HideInInspector]
        protected internal BezierAnchor to = new BezierAnchor(new Vector3(3, 3), new Vector3(0, -1));

        /// <summary>
        /// Anchor point of mono curve from.
        /// </summary>
        public BezierAnchor From
        {
            set { from = InverseTransformAnchor(value); }
            get { return TransformAnchor(from); }
        }

        /// <summary>
        /// Anchor point of mono curve to.
        /// </summary>
        public BezierAnchor To
        {
            set { to = InverseTransformAnchor(value); }
            get { return TransformAnchor(to); }
        }

        /// <summary>
        /// Mono curve is close?
        /// </summary>
        public bool IsClose
        {
            get { return from.point == to.point; }
        }

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
        protected BezierCurve curve = new BezierCurve();

        /// <summary>
        /// Rebuild mono curve.
        /// </summary>
        public override void Rebuild()
        {
            var anchorFr = from;
            anchorFr.tangent += anchorFr.point;
            curve.from = anchorFr;

            var anchorTo = to;
            anchorTo.tangent += anchorTo.point;
            curve.to = anchorTo;

            length = EvaluateLength();
            base.Rebuild();
        }

        /// <summary>
        /// Inverse transform anchor.
        /// </summary>
        /// <param name="anchor"></param>
        /// <returns></returns>
        protected BezierAnchor InverseTransformAnchor(BezierAnchor anchor)
        {
            anchor.point = transform.InverseTransformPoint(anchor.point);
            anchor.tangent = transform.TransformVector(anchor.tangent);
            return anchor;
        }

        /// <summary>
        /// Transform anchor.
        /// </summary>
        /// <param name="anchor"></param>
        /// <returns></returns>
        protected BezierAnchor TransformAnchor(BezierAnchor anchor)
        {
            anchor.point = transform.TransformPoint(anchor.point);
            anchor.tangent = transform.TransformVector(anchor.tangent);
            return anchor;
        }
    }
}