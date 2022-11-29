/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MonoCurveSkinnedHose.cs
 *  Description  :  Define MonoCurveSkinnedHose to render dynamic hose mesh
 *                  base on mono curve.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  3/20/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using MGS.Curve;
using System.Collections.Generic;
using UnityEngine;

namespace MGS.SkinnedMesh
{
    /// <summary>
    /// Render dynamic hose mesh base on mono curve.
    /// </summary>
    public class MonoCurveSkinnedHose : MonoCurveHose, IMonoCurveHose
    {
        /// <summary>
        /// Mono curve for curve hose.
        /// </summary>
        protected IMonoCurve curve;

        /// <summary>
        /// Rebuild renderer base curve.
        /// </summary>
        /// <param name="curve"></param>
        public override void Rebuild(IMonoCurve curve)
        {
            this.curve = curve;
            base.Rebuild();
        }

        /// <summary>
        /// Rebuild the mesh of hose.
        /// </summary>
        protected override void Rebuild(Mesh mesh)
        {
            mesh.Clear();
            if (curve == null || curve.Length == 0)
            {
                Segments = 0;
                mesh.RecalculateBounds();
                return;
            }

            var differ = 0f;
            Segments = MonoCurveUtility.GetSegmentCount(curve, segment, out differ) + 1;
            var isSeal = seal && polygon > 2;

            mesh.vertices = CreateVertices(curve, Segments, differ, isSeal);
            mesh.uv = CreateUV(Segments, isSeal);
            mesh.triangles = CreateTriangles(Segments, isSeal);

            if (isSeal)
            {
                if (mesh.subMeshCount < 2)
                {
                    mesh.subMeshCount = 2;
                }
                mesh.SetTriangles(CreateSideTriangles(Segments), 1);
            }

            mesh.RecalculateNormals();
#if UNITY_5_6_OR_NEWER
            mesh.RecalculateTangents();
#endif
            mesh.RecalculateBounds();
        }

        /// <summary>
        /// Create the vertices of hose mesh.
        /// </summary>
        /// <param name="curve"></param>
        /// <param name="segments"></param>
        /// <param name="differ"></param>
        /// <param name="isSeal">Is seal at both ends of hose?</param>
        /// <returns>Vertices array.</returns>
        protected Vector3[] CreateVertices(IMonoCurve curve, int segments, float differ, bool isSeal = false)
        {
            var vertices = new List<Vector3>();
            for (int i = 0; i < segments - 1; i++)
            {
                var len = differ * i;
                var node = curve.LocalEvaluate(len);
                var tangent = (curve.LocalEvaluate(len + differ) - node);
                vertices.AddRange(MeshBuildUtility.BuildPolygonVertices(polygon, radius, node, Quaternion.LookRotation(tangent)));
            }
            var lastNode = curve.LocalEvaluate(curve.Length);
            var lastTangent = (lastNode - curve.LocalEvaluate(curve.Length - differ));
            vertices.AddRange(MeshBuildUtility.BuildPolygonVertices(polygon, radius, lastNode, Quaternion.LookRotation(lastTangent)));

            if (isSeal)
            {
                var polygonVertices = polygon + 1;
                vertices.Add(curve.LocalEvaluate(0));
                vertices.AddRange(vertices.GetRange(0, polygonVertices));

                var lastPolygonStart = polygonVertices * (segments - 1);
                vertices.Add(lastNode);
                vertices.AddRange(vertices.GetRange(lastPolygonStart, polygonVertices));
            }
            return vertices.ToArray();
        }

        /// <summary>
        /// Create triangles of hose mesh.
        /// </summary>
        /// <param name="segments"></param>
        /// <param name="isSeal">Is seal at both ends of hose?</param>
        /// <returns>Triangles array.</returns>
        protected int[] CreateTriangles(int segments, bool isSeal = false)
        {
            var triangles = MeshBuildUtility.BuildPrismTriangles(polygon, segments, 0);
            return triangles.ToArray();
        }

        /// <summary>
        /// Create triangles of hose side mesh.
        /// </summary>
        /// <param name="segments"></param>
        /// <returns></returns>
        protected int[] CreateSideTriangles(int segments)
        {
            var polygonVertices = polygon + 1;
            var firstSide = polygonVertices * segments;
            var triangles = MeshBuildUtility.BuildPolygonTriangles(polygon, firstSide, firstSide + 1, false);

            var lastSide = firstSide + polygonVertices + 1;
            triangles.AddRange(MeshBuildUtility.BuildPolygonTriangles(polygon, lastSide, lastSide + 1));
            return triangles.ToArray();
        }

        /// <summary>
        /// Create uv of hose mesh.
        /// </summary>
        /// <param name="segments"></param>
        /// <param name="isSeal">Is seal at both ends of hose?</param>
        /// <returns>UV array.</returns>
        protected Vector2[] CreateUV(int segments, bool isSeal = false)
        {
            var uv = MeshBuildUtility.BuildPrismUV(polygon, segments);
            if (isSeal)
            {
                for (int i = 0; i < 2; i++)
                {
                    uv.Add(Vector2.one * 0.5f);
                    uv.AddRange(MeshBuildUtility.BuildPolygonUV(polygon, i == 0));
                }
            }
            return uv.ToArray();
        }
    }
}