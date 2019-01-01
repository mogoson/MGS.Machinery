/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  AnchorHose.cs
 *  Description  :  Render dynamic hose mesh base on anchor curve.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  3/20/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using Mogoson.Curve;
using System.Collections.Generic;
using UnityEngine;

namespace Mogoson.CurveHose
{
    /// <summary>
    /// Render dynamic hose mesh base on anchor curve.
    /// </summary>
    [AddComponentMenu("Mogoson/CurveHose/AnchorHose")]
    public class AnchorHose : MonoCurveHose
    {
        #region Field and Property
        /// <summary>
        /// Hose curve is close?
        /// </summary>
        public bool close = false;

        /// <summary>
        /// Anchors of hose curve.
        /// </summary>
        [SerializeField]
        [HideInInspector]
        protected List<Vector3> anchors = new List<Vector3>()
        {
            new Vector3(1, 1, 1),
            new Vector3(1, 1, 2),
            new Vector3(3, 1, 2),
            new Vector3(3, 1, 3)
        };

        /// <summary>
        /// Count of hose curve anchors.
        /// </summary>
        public int AnchorsCount { get { return anchors.Count; } }

        /// <summary>
        /// Curve for hose.
        /// </summary>
        protected override ICurve Curve { get { return curve; } }

        /// <summary>
        /// Anchor curve of hose.
        /// </summary>
        protected HermiteCurve curve = new HermiteCurve();
        #endregion

        #region Public Method
        /// <summary>
        /// Rebuild the mesh of hose.
        /// </summary>
        public override void Rebuild()
        {
            curve = HermiteCurve.FromAnchors(anchors.ToArray(), close);
            base.Rebuild();
        }

        /// <summary>
        /// Add anchor item.
        /// </summary>
        /// <param name="item">Anchor item.</param>
        public void AddAnchor(Vector3 item)
        {
            anchors.Add(transform.InverseTransformPoint(item));
        }

        /// <summary>
        /// Insert Anchor item at index.
        /// </summary>
        /// <param name="index">Index of anchor.</param>
        /// <param name="item">Anchor item.</param>
        public void InsertAnchor(int index, Vector3 item)
        {
            anchors.Insert(index, transform.InverseTransformPoint(item));
        }

        /// <summary>
        /// Set the anchor item at index.
        /// </summary>
        /// <param name="index">Index of anchor.</param>
        /// <param name="item">Anchor item.</param>
        public void SetAnchorAt(int index, Vector3 item)
        {
            anchors[index] = transform.InverseTransformPoint(item);
        }

        /// <summary>
        /// Get the anchor item at index.
        /// </summary>
        /// <param name="index">Anchor index.</param>
        /// <returns>Anchor item.</returns>
        public Vector3 GetAnchorAt(int index)
        {
            return transform.TransformPoint(anchors[index]);
        }

        /// <summary>
        /// Remove the anchor item.
        /// </summary>
        /// <param name="item">Anchor item.</param>
        public void RemoveAnchor(Vector3 item)
        {
            anchors.Remove(item);
        }

        /// <summary>
        /// Remove the anchor item at index.
        /// </summary>
        /// <param name="index">Anchor index.</param>
        public void RemoveAnchorAt(int index)
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
        #endregion
    }
}