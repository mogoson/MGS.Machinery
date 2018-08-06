/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MonoCurvePipe.cs
 *  Description  :  Define MonoCurvePipe to render dynamic pipe mesh base
 *                  on center curve.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  3/20/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using Mogoson.Curve;
using Mogoson.Skin;
using System.Collections.Generic;
using UnityEngine;

namespace Mogoson.CurvePipe
{
    /// <summary>
    /// Render dynamic pipe mesh base on center curve.
    /// </summary>
    public abstract class MonoCurvePipe : MonoSkin, ICurvePipe
    {
        #region Field and Property
        /// <summary>
        /// Polygon of pipe cross section.
        /// </summary>
        [SerializeField]
        protected int polygon = 8;

        /// <summary>
        /// Segment length of subdivide pipe.
        /// </summary>
        [SerializeField]
        protected float segment = 0.25f;

        /// <summary>
        /// Radius of pipe mesh.
        /// </summary>
        [SerializeField]
        protected float radius = 0.1f;

        /// <summary>
        /// Is seal at both ends of pipe?
        /// </summary>
        [SerializeField]
        protected bool seal = false;

        /// <summary>
        /// Polygon of pipe cross section.
        /// </summary>
        public int Polygon
        {
            set { polygon = value; }
            get { return polygon; }
        }

        /// <summary>
        ///  Segment length of subdivide pipe.
        /// </summary>
        public float Segment
        {
            set { segment = value; }
            get { return segment; }
        }

        /// <summary>
        /// Radius of pipe mesh.
        /// </summary>
        public float Radius
        {
            set { radius = value; }
            get { return radius; }
        }

        /// <summary>
        /// Is seal at both ends of pipe?
        /// </summary>
        public bool Seal
        {
            set { seal = value; }
            get { return seal; }
        }

        /// <summary>
        /// Max key of pipe center curve.
        /// </summary>
        public virtual float MaxKey { get { return Curve.MaxKey; } }

        /// <summary>
        /// Length of pipe center curve.
        /// </summary>
        public virtual float Length { get { return length; } }

        /// <summary>
        /// Curve for pipe.
        /// </summary>
        protected abstract ICurve Curve { get; }

        /// <summary>
        /// Radian of circle.
        /// </summary>
        protected const float CircleRadian = 2 * Mathf.PI;

        /// <summary>
        /// Delta to calculate tangent.
        /// </summary>
        protected const float Delta = 0.001f;

        /// <summary>
        /// Length of pipe center curve.
        /// </summary>
        protected float length = 0.0f;

        /// <summary>
        /// Segment count of subdivide pipe.
        /// </summary>
        protected int segmentCount = 0;
        #endregion

        #region Protected Method
        /// <summary>
        /// Create the vertices of pipe mesh.
        /// </summary>
        /// <returns>Vertices array.</returns>
        protected override Vector3[] CreateVertices()
        {
            var vertices = new List<Vector3>();
            var keySegment = MaxKey / segmentCount;
            for (int i = 0; i < segmentCount; i++)
            {
                var key = keySegment * i;
                var center = Curve.GetPointAt(key);
                var tangent = (Curve.GetPointAt(key + Delta) - center).normalized;
                vertices.AddRange(CreateSegmentVertices(center, Quaternion.LookRotation(tangent)));
            }

            var lastCenter = Curve.GetPointAt(MaxKey);
            var lastTangent = (lastCenter - Curve.GetPointAt(MaxKey - Delta)).normalized;
            vertices.AddRange(CreateSegmentVertices(lastCenter, Quaternion.LookRotation(lastTangent)));

            if (seal && polygon > 2)
            {
                vertices.Add(Curve.GetPointAt(0));
                vertices.Add(Curve.GetPointAt(MaxKey));
            }
            return vertices.ToArray();
        }

        /// <summary>
        /// Create triangles of pipe mesh.
        /// </summary>
        /// <returns>Triangles array.</returns>
        protected override int[] CreateTriangles()
        {
            var triangles = new List<int>();
            for (int i = 0; i < segmentCount; i++)
            {
                for (int j = 0; j < polygon - 1; j++)
                {
                    triangles.Add(polygon * i + j);
                    triangles.Add(polygon * i + j + 1);
                    triangles.Add(polygon * (i + 1) + j + 1);

                    triangles.Add(polygon * i + j);
                    triangles.Add(polygon * (i + 1) + j + 1);
                    triangles.Add(polygon * (i + 1) + j);
                }

                triangles.Add(polygon * i);
                triangles.Add(polygon * (i + 1));
                triangles.Add(polygon * (i + 2) - 1);

                triangles.Add(polygon * i);
                triangles.Add(polygon * (i + 2) - 1);
                triangles.Add(polygon * (i + 1) - 1);
            }

            if (seal && polygon > 2)
            {
                for (int i = 0; i < polygon - 1; i++)
                {
                    triangles.Add(polygon * (segmentCount + 1));
                    triangles.Add(i + 1);
                    triangles.Add(i);

                    triangles.Add(polygon * (segmentCount + 1) + 1);
                    triangles.Add(polygon * segmentCount + i);
                    triangles.Add(polygon * segmentCount + i + 1);
                }

                triangles.Add(polygon * (segmentCount + 1));
                triangles.Add(0);
                triangles.Add(polygon - 1);

                triangles.Add(polygon * (segmentCount + 1) + 1);
                triangles.Add(polygon * (segmentCount + 1) - 1);
                triangles.Add(polygon * segmentCount);
            }
            return triangles.ToArray();
        }

        /// <summary>
        /// Create vertices of current segment base pipe.
        /// </summary>
        /// <param name="center">Center point of segment.</param>
        /// <param name="rotation">Rotation of segment vertices.</param>
        /// <returns>Segment vertices.</returns>
        protected virtual Vector3[] CreateSegmentVertices(Vector3 center, Quaternion rotation)
        {
            var vertices = new Vector3[polygon];
            for (int i = 0; i < polygon; i++)
            {
                var angle = CircleRadian / polygon * i;
                var vertice = center + rotation * new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
                vertices[i] = vertice;
            }
            return vertices;
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Rebuild the mesh of pipe.
        /// </summary>
        public override void Rebuild()
        {
            length = Curve.Length;
            segmentCount = (int)(length / segment);
            base.Rebuild();
        }

        /// <summary>
        /// Get point from center curve of pipe at key.
        /// </summary>
        /// <param name="key">Key of pipe center curve.</param>
        /// <returns>Point on pipe curve at key.</returns>
        public Vector3 GetPointAt(float key)
        {
            return transform.TransformPoint(Curve.GetPointAt(key));
        }
        #endregion
    }
}