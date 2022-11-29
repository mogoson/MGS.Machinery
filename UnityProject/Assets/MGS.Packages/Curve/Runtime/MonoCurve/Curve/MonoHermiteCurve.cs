/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MonoHermiteCurve.cs
 *  Description  :  Define mono curve base on anchors.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  2/28/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace MGS.Curve
{
    /// <summary>
    /// Mono curve base on anchors.
    /// </summary>
    public class MonoHermiteCurve : MonoCurve
    {
        /// <summary>
        /// Mono curve is auto smooth?
        /// </summary>
        public bool autoSmooth;

        /// <summary>
        /// Anchors of mono curve.
        /// </summary>
        [SerializeField]
        [HideInInspector]
        protected internal List<HermiteAnchor> anchors = new List<HermiteAnchor>();

        /// <summary>
        /// Count of mono curve anchors.
        /// </summary>
        public int AnchorsCount { get { return anchors.Count; } }

        /// <summary>
        /// Mono curve is close?
        /// </summary>
        public bool IsClose
        {
            get { return anchors.Count > 2 && anchors[0].point == anchors[anchors.Count - 1].point; }
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
        protected HermiteCurve curve = new HermiteCurve();

        /// <summary>
        /// Rebuild mono curve.
        /// </summary>
        public override void Rebuild()
        {
            curve.ClearFrames();
            if (anchors.Count > 0)
            {
                var time = 0f;
                var prev = anchors[0];
                foreach (var anchor in anchors)
                {
                    time += Vector3.Distance(prev.point, anchor.point);
                    curve.AddFrame(new KeyFrame(time, anchor.point, anchor.inTangent, anchor.outTangent));
                    prev = anchor;
                }

                if (autoSmooth)
                {
                    curve.SmoothTangents();
                }
            }
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
            if (curve.FramesCount > 0)
            {
                return base.LocalEvaluateNormalized(curve[curve.FramesCount - 1].time * t);
            }
            return Vector3.zero;
        }

        /// <summary>
        /// Add anchor item.
        /// </summary>
        /// <param name="anchor">Anchor item.</param>
        public void AddAnchor(HermiteAnchor anchor)
        {
            anchors.Add(InverseTransformAnchor(anchor));
        }

        /// <summary>
        /// Insert Anchor item at index.
        /// </summary>
        /// <param name="index">Index of anchor.</param>
        /// <param name="anchor">Anchor item.</param>
        public void InsertAnchor(int index, HermiteAnchor anchor)
        {
            anchors.Insert(index, InverseTransformAnchor(anchor));
        }

        /// <summary>
        /// Set the anchor item at index.
        /// </summary>
        /// <param name="index">Index of anchor.</param>
        /// <param name="anchor">Anchor item.</param>
        public void SetAnchor(int index, HermiteAnchor anchor)
        {
            anchors[index] = InverseTransformAnchor(anchor);
        }

        /// <summary>
        /// Get the anchor item at index.
        /// </summary>
        /// <param name="index">Anchor index.</param>
        /// <returns>Anchor item.</returns>
        public HermiteAnchor GetAnchor(int index)
        {
            return TransformAnchor(anchors[index]);
        }

        /// <summary>
        /// Remove the anchor item.
        /// </summary>
        /// <param name="anchor">Anchor item.</param>
        public void RemoveAnchor(HermiteAnchor anchor)
        {
            anchors.Remove(anchor);
        }

        /// <summary>
        /// Remove the anchor item at index.
        /// </summary>
        /// <param name="index">Anchor index.</param>
        public void RemoveAnchor(int index)
        {
            anchors.RemoveAt(index);
        }

        /// <summary>
        /// Clear all anchor items.
        /// </summary>
        public void ClearAnchors()
        {
            anchors.Clear();
        }

        /// <summary>
        /// Inverse anchor tangents from HermiteCurve.
        /// (Require the anchors is already build to the curve)
        /// </summary>
        public void InverseAnchorTangents()
        {
            for (int i = 0; i < anchors.Count; i++)
            {
                var frame = curve[i];
                var anchor = anchors[i];
                anchor.inTangent = frame.inTangent;
                anchor.outTangent = frame.outTangent;
                anchors[i] = anchor;
            }
        }

        /// <summary>
        /// Inverse transform anchor.
        /// </summary>
        /// <param name="anchor"></param>
        /// <returns></returns>
        protected HermiteAnchor InverseTransformAnchor(HermiteAnchor anchor)
        {
            anchor.point = transform.InverseTransformPoint(anchor.point);
            anchor.inTangent = transform.InverseTransformVector(anchor.inTangent);
            anchor.outTangent = transform.InverseTransformVector(anchor.outTangent);
            return anchor;
        }

        /// <summary>
        /// Transform anchor.
        /// </summary>
        /// <param name="anchor"></param>
        /// <returns></returns>
        protected HermiteAnchor TransformAnchor(HermiteAnchor anchor)
        {
            anchor.point = transform.TransformPoint(anchor.point);
            anchor.inTangent = transform.TransformVector(anchor.inTangent);
            anchor.outTangent = transform.TransformVector(anchor.outTangent);
            return anchor;
        }
    }

    /// <summary>
    /// Anchor settings of hermite curve.
    /// </summary>
    [Serializable]
    public struct HermiteAnchor
    {
        /// <summary>
        /// Point of anchor.
        /// </summary>
        public Vector3 point;

        /// <summary>
        /// In tangent vector based on point.
        /// </summary>
        public Vector3 inTangent;

        /// <summary>
        /// Out tangent vector based on point.
        /// </summary>
        public Vector3 outTangent;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="point">Point of anchor.</param>
        public HermiteAnchor(Vector3 point)
        {
            this.point = point;
            inTangent = Vector3.zero;
            outTangent = Vector3.zero;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="point">Point of anchor.</param>
        /// <param name="inTangent">In tangent vector based on anchor point.</param>
        /// <param name="outTangent">Out tangent vector based on anchor point.</param>
        public HermiteAnchor(Vector3 point, Vector3 inTangent, Vector3 outTangent)
        {
            this.point = point;
            this.inTangent = inTangent;
            this.outTangent = outTangent;
        }
    }
}