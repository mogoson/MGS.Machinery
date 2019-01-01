/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MonoCurveHose.cs
 *  Description  :  Define MonoCurveHose to render dynamic hose mesh base
 *                  on center curve.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  3/20/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using Mogoson.Curve;
using Mogoson.Skin;
using Mogoson.UMesh;
using System.Collections.Generic;
using UnityEngine;

namespace Mogoson.CurveHose
{
    /// <summary>
    /// Render dynamic hose mesh base on center curve.
    /// </summary>
    public abstract class MonoCurveHose : MonoSkin, ICurveHose
    {
        #region Field and Property
        /// <summary>
        /// Polygon of hose cross section.
        /// </summary>
        [SerializeField]
        protected int polygon = 8;

        /// <summary>
        /// Segment length of subdivide hose.
        /// </summary>
        [SerializeField]
        protected float segment = 0.25f;

        /// <summary>
        /// Radius of hose mesh.
        /// </summary>
        [SerializeField]
        protected float radius = 0.1f;

        /// <summary>
        /// Is seal at both ends of hose?
        /// </summary>
        [SerializeField]
        protected bool seal = false;

        /// <summary>
        /// Polygon of hose cross section.
        /// </summary>
        public int Polygon
        {
            set { polygon = value; }
            get { return polygon; }
        }

        /// <summary>
        ///  Segment length of subdivide hose.
        /// </summary>
        public float Segment
        {
            set { segment = value; }
            get { return segment; }
        }

        /// <summary>
        /// Radius of hose mesh.
        /// </summary>
        public float Radius
        {
            set { radius = value; }
            get { return radius; }
        }

        /// <summary>
        /// Is seal at both ends of hose?
        /// </summary>
        public bool Seal
        {
            set { seal = value; }
            get { return seal; }
        }

        /// <summary>
        /// Max key of hose center curve.
        /// </summary>
        public virtual float MaxKey { get { return Curve.MaxKey; } }

        /// <summary>
        /// Length of hose center curve.
        /// </summary>
        public virtual float Length { get { return length; } }

        /// <summary>
        /// Curve for hose.
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
        /// Length of hose center curve.
        /// </summary>
        protected float length = 0.0f;

        /// <summary>
        /// Segment count of subdivide hose.
        /// </summary>
        protected int segmentCount = 0;
        #endregion

        #region Protected Method
        /// <summary>
        /// Rebuild the mesh of hose.
        /// </summary>
        protected override void RebuildMesh()
        {
            var isSeal = seal && polygon > 2;
            mesh.Clear();
            mesh.vertices = CreateVertices(isSeal);
            mesh.triangles = CreateTriangles(isSeal);
            mesh.uv = CreateUV(isSeal);
            if (isSeal)
            {
                if (mesh.subMeshCount < 2)
                {
                    mesh.subMeshCount = 2;
                }
                mesh.SetTriangles(CreateSideTriangles(), 1);
            }
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
        }

        /// <summary>
        /// Create the vertices of hose mesh.
        /// </summary>
        /// <param name="isSeal">Is seal at both ends of hose?</param>
        /// <returns>Vertices array.</returns>
        protected Vector3[] CreateVertices(bool isSeal = false)
        {
            var vertices = new List<Vector3>();
            var keySegment = MaxKey / (segmentCount - 1);
            for (int i = 0; i < segmentCount - 1; i++)
            {
                var key = keySegment * i;
                var center = Curve.GetPointAt(key);
                var tangent = (Curve.GetPointAt(key + Delta) - center).normalized;
                vertices.AddRange(MeshUtility.CreateVerticesBasePolygon(polygon, radius, center, Quaternion.LookRotation(tangent)));
            }
            var lastCenter = Curve.GetPointAt(MaxKey);
            var lastTangent = (lastCenter - Curve.GetPointAt(MaxKey - Delta)).normalized;
            vertices.AddRange(MeshUtility.CreateVerticesBasePolygon(polygon, radius, lastCenter, Quaternion.LookRotation(lastTangent)));

            if (isSeal)
            {
                var polygonVertices = polygon + 1;
                vertices.Add(Curve.GetPointAt(0));
                vertices.AddRange(vertices.GetRange(0, polygonVertices));

                var lastPolygonStart = polygonVertices * (segmentCount - 1);
                vertices.Add(lastCenter);
                vertices.AddRange(vertices.GetRange(lastPolygonStart, polygonVertices));
            }
            return vertices.ToArray();
        }

        /// <summary>
        /// Create triangles of hose mesh.
        /// </summary>
        /// <param name="isSeal">Is seal at both ends of hose?</param>
        /// <returns>Triangles array.</returns>
        protected int[] CreateTriangles(bool isSeal = false)
        {
            var triangles = MeshUtility.CreateTrianglesBasePrism(polygon, segmentCount, 0);
            return triangles.ToArray();
        }

        /// <summary>
        /// Create triangles of hose side mesh.
        /// </summary>
        /// <returns></returns>
        protected int[] CreateSideTriangles()
        {
            var polygonVertices = polygon + 1;
            var leftCenter = polygonVertices * segmentCount;
            var triangles = MeshUtility.CreateTrianglesBasePolygon(polygon, leftCenter, leftCenter + 1, false);

            var rightCenter = leftCenter + polygonVertices + 1;
            triangles.AddRange(MeshUtility.CreateTrianglesBasePolygon(polygon, rightCenter, rightCenter + 1));
            return triangles.ToArray();
        }

        /// <summary>
        /// Create uv of hose mesh.
        /// </summary>
        /// <param name="isSeal">Is seal at both ends of hose?</param>
        /// <returns>UV array.</returns>
        protected Vector2[] CreateUV(bool isSeal = false)
        {
            var uv = MeshUtility.CreateUVBasePrism(polygon, segmentCount);
            if (isSeal)
            {
                for (int i = 0; i < 2; i++)
                {
                    uv.Add(Vector2.one * 0.5f);
                    uv.AddRange(MeshUtility.CreateUVBasePolygon(polygon));
                }
            }
            return uv.ToArray();
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Rebuild the mesh of hose.
        /// </summary>
        public override void Rebuild()
        {
            length = Curve.Length;
            segmentCount = (int)(length / segment) + 1;
            base.Rebuild();
        }

        /// <summary>
        /// Get point from center curve of hose at key.
        /// </summary>
        /// <param name="key">Key of hose center curve.</param>
        /// <returns>Point on hose curve at key.</returns>
        public Vector3 GetPointAt(float key)
        {
            return transform.TransformPoint(Curve.GetPointAt(key));
        }
        #endregion
    }
}