/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MeshBuildUtility.cs
 *  Description  :  Utility for build skinned mesh.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  10/26/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using UnityEngine;

namespace MGS.SkinnedMesh
{
    /// <summary>
    /// Utility for build skinned mesh.
    /// </summary>
    public sealed class MeshBuildUtility
    {
        #region Public Method
        /// <summary>
        /// Build polygon vertices.
        /// </summary>
        /// <param name="edge">Edge count of polygon.</param>
        /// <param name="radius">Radius of polygon.</param>
        /// <param name="center">Center of polygon.</param>
        /// <param name="rotation">Rotation of polygon.</param>
        /// <returns>Vertices base on polygon.</returns>
        public static List<Vector3> BuildPolygonVertices(int edge, float radius, Vector3 center, Quaternion rotation)
        {
            var vertices = new List<Vector3>();
            var sector = 2 * Mathf.PI / edge;
            var radian = 0f;
            for (int i = 0; i <= edge; i++)
            {
                radian = sector * i;
                vertices.Add(center + rotation * new Vector3(Mathf.Cos(radian), Mathf.Sin(radian)) * radius);
            }
            return vertices;
        }

        /// <summary>
        /// Build polygon triangles index base on center vertice.
        /// </summary>
        /// <param name="edge">Edge count of polygon.</param>
        /// <param name="center">Index of center vertice.</param>
        /// <param name="start">Index of start vertice.</param>
        /// <param name="clockwise">Triangle indexs is clockwise?</param>
        /// <returns>Triangles base on polygon.</returns>
        public static List<int> BuildPolygonTriangles(int edge, int center, int start, bool clockwise = true)
        {
            var triangles = new List<int>();
            var offset = clockwise ? 0 : 1;
            for (int i = 0; i < edge; i++)
            {
                triangles.Add(start + i + offset);
                triangles.Add(start + i - offset + 1);
                triangles.Add(center);
            }
            return triangles;
        }

        /// <summary>
        /// Build prism triangles index.
        /// </summary>
        /// <param name="polygon">Edge count of prism polygon.</param>
        /// <param name="segments">Segments count of prism vertices vertical division.</param>
        /// <param name="start">Start index of prism vertice.</param>
        /// <returns>Triangles index base on prism.</returns>
        public static List<int> BuildPrismTriangles(int polygon, int segments, int start)
        {
            var triangles = new List<int>();
            var polygonVs = polygon + 1;
            var currentSegment = 0;
            var nextSegment = 0;
            for (int s = 0; s < segments - 1; s++)
            {
                // Calculate start index.
                currentSegment = polygonVs * s;
                nextSegment = polygonVs * (s + 1);
                for (int p = 0; p < polygon; p++)
                {
                    // Left-Bottom triangle.
                    triangles.Add(start + currentSegment + p);
                    triangles.Add(start + currentSegment + p + 1);
                    triangles.Add(start + nextSegment + p + 1);

                    // Right-Top triangle.
                    triangles.Add(start + currentSegment + p);
                    triangles.Add(start + nextSegment + p + 1);
                    triangles.Add(start + nextSegment + p);
                }
            }
            return triangles;
        }

        /// <summary>
        /// Build polygon uv.
        /// </summary>
        /// <param name="edge">Edge count of polygon.</param>
        /// <param name="clockwise">UV indexs is clockwise?</param>
        /// <returns>UV base on polygon.</returns>
        public static List<Vector2> BuildPolygonUV(int edge, bool clockwise = true)
        {
            var uv = new List<Vector2>();
            var sector = 2 * Mathf.PI / edge;
            var radian = 0f;
            var center = Vector2.one * 0.5f;
            for (int i = 0; i <= edge; i++)
            {
                radian = sector * (clockwise ? i : edge - i);
                uv.Add(center + new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)) * 0.5f);
            }
            return uv;
        }

        /// <summary>
        /// Build prism uv.
        /// </summary>
        /// <param name="polygon">Edge count of prism polygon.</param>
        /// <param name="segments">Segments count of prism vertices vertical division.</param>
        /// <returns>UV base on prism.</returns>
        public static List<Vector2> BuildPrismUV(int polygon, int segments)
        {
            var uv = new List<Vector2>();
            var polygonVs = polygon + 1;
            var vertices = polygonVs * segments;
            var slice = 1.0f / polygon;
            var u = 0f;
            var v = 0f;
            for (int i = 0; i < vertices; i++)
            {
                u = slice * (i % polygonVs);
                v = (i / polygonVs) % 2;
                uv.Add(new Vector2(u, v));
            }
            return uv;
        }
        #endregion
    }
}