/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  GeometryMeshUtility.cs
 *  Description  :  Utility for geometry mesh.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  7/23/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using UnityEngine;

namespace MGS.SkinnedMesh
{
    /// <summary>
    /// Utility for geometry mesh.
    /// </summary>
    public sealed class GeometryMeshUtility
    {
        #region
        /// <summary>
        /// Build a prismatic camber mesh by two group vertices.
        /// The length of group vertices must be same and greater than 3.
        /// </summary>
        /// <param name="bvs">Bottom vertices.</param>
        /// <param name="tvs">Top vertices.</param>
        /// <param name="close">The vertice is close?</param>
        /// <returns></returns>
        public static Mesh CreatePrismCamber(ICollection<Vector3> bvs, ICollection<Vector3> tvs, bool close = true)
        {
            //Vertices
            var vs = new List<Vector3>(bvs);
            vs.AddRange(tvs);

            //Side triangles
            var ts = CreateCamberTriangles(bvs, tvs, close);

            //Mesh
            return CreateMesh(vs.ToArray(), ts.ToArray());
        }

        /// <summary>
        /// Build a prismatic camber mesh by two group vertices.
        /// The length of group vertices must be same and greater than 3.
        /// </summary>
        /// <param name="bvs">Bottom vertices.</param>
        /// <param name="tvs">Top vertices.</param>
        /// <param name="bpi">Vertex index of bottom pivot.</param>
        /// <param name="tpi">Vertex index of top pivot.</param>
        /// <param name="close">The vertice is close?</param>
        /// <returns></returns>
        public static Mesh CreatePrismCamber(ICollection<Vector3> bvs, ICollection<Vector3> tvs, int bpi, int tpi, bool close = true)
        {
            //Vertices
            var vs = new List<Vector3>(bvs);
            vs.AddRange(tvs);

            //Side triangles
            var ts = CreateCamberTriangles(bvs, tvs, close);

            //Top Bottom triangles
            var vc = bvs.Count;
            var tsb = CreateCamberTriangles(bpi, vc, 0, false, close);
            var tst = CreateCamberTriangles(tpi, vc, vc, true, close);
            ts.AddRange(tsb);
            ts.AddRange(tst);

            //Mesh
            return CreateMesh(vs.ToArray(), ts.ToArray());
        }

        /// <summary>
        /// Build a prismatic camber mesh by two group vertices.
        /// The length of group vertices must be same and greater than 3.
        /// </summary>
        /// <param name="bvs">Bottom vertices.</param>
        /// <param name="tvs">Top vertices.</param>
        /// <param name="bp">Vertex of bottom pivot.</param>
        /// <param name="tp">Vertex of top pivot.</param>
        /// <param name="close">The vertice is close?</param>
        /// <returns></returns>
        public static Mesh CreatePrismCamber(ICollection<Vector3> bvs, ICollection<Vector3> tvs, Vector3 bp, Vector3 tp, bool close = true)
        {
            //Vertices
            var vs = new List<Vector3>(bvs);
            vs.AddRange(tvs);
            vs.Add(bp);
            vs.Add(tp);

            //Side triangles
            var ts = CreateCamberTriangles(bvs, tvs, close);

            //Top Bottom triangles
            var vc = bvs.Count;
            var tsb = CreateCamberTriangles(vs.Count - 2, vc, 0, false, close);
            var tst = CreateCamberTriangles(vs.Count - 1, vc, vc, true, close);
            ts.AddRange(tsb);
            ts.AddRange(tst);

            //Mesh
            return CreateMesh(vs.ToArray(), ts.ToArray());
        }

        /// <summary>
        /// Build a pyramid camber mesh base pivot to vertices.
        /// </summary>
        /// <param name="vs">Bottom vertices.</param>
        /// <param name="tp">Vertex of top pivot.</param>
        /// <param name="close">The vertice is close?</param>
        /// <returns></returns>
        public static Mesh CreatePyramidCamber(ICollection<Vector3> vs, Vector3 tp, bool close = true)
        {
            //Vertices
            var vsn = new List<Vector3>(vs);
            vsn.Add(tp);

            //Side triangles
            var vc = vs.Count;
            var ts = CreateCamberTriangles(vc, vc, 0, true, close);

            //Mesh
            return CreateMesh(vsn.ToArray(), ts.ToArray());
        }

        /// <summary>
        /// Build a pyramid camber mesh base pivot to vertices.
        /// </summary>
        /// <param name="vs">Bottom vertices.</param>
        /// <param name="vpi">Index of bottom pivot.</param>
        /// <param name="tp">Vertex of top pivot.</param>
        /// <param name="close">The vertice is close?</param>
        /// <returns></returns>
        public static Mesh CreatePyramidCamber(ICollection<Vector3> vs, int vpi, Vector3 tp, bool close = true)
        {
            //Vertices
            var vsn = new List<Vector3>(vs);
            vsn.Add(tp);

            //Side and Bottom triangles
            var vc = vs.Count;
            var ts = CreateCamberTriangles(vc, vc, 0, true, close);
            var tsb = CreateCamberTriangles(vpi, vc, 0, false, close);
            ts.AddRange(tsb);

            //Mesh
            return CreateMesh(vsn.ToArray(), ts.ToArray());
        }

        /// <summary>
        /// Build a pyramid camber mesh base pivot to vertices.
        /// </summary>
        /// <param name="vs">Bottom vertices.</param>
        /// <param name="vpi">Vertex of bottom pivot.</param>
        /// <param name="tp">Vertex of top pivot.</param>
        /// <param name="close">The vertice is close?</param>
        /// <returns></returns>
        public static Mesh CreatePyramidCamber(ICollection<Vector3> vs, Vector3 vp, Vector3 tp, bool close = true)
        {
            //Vertices
            var vsn = new List<Vector3>(vs);
            vsn.Add(vp);
            vsn.Add(tp);

            //Side and Bottom triangles
            var vc = vs.Count;
            var ts = CreateCamberTriangles(vc + 1, vc, 0, true, close);
            var tsb = CreateCamberTriangles(vc, vc, 0, false, close);
            ts.AddRange(tsb);

            //Mesh
            return CreateMesh(vsn.ToArray(), ts.ToArray());
        }

        /// <summary>
        /// Create triangles of camber base two group vertices.
        /// </summary>
        /// <param name="bvs">Bottom vertices.</param>
        /// <param name="tvs">Top vertices.</param>
        /// <param name="close">The vertice is close?</param>
        /// <returns></returns>
        public static List<int> CreateCamberTriangles(ICollection<Vector3> bvs, ICollection<Vector3> tvs, bool close = true)
        {
            /*
             * Triangle index:
             * (i, ni+c, i+c),(i, ni, ni+c)
             */

            var ts = new List<int>();
            var c = bvs.Count;
            var tc = close ? bvs.Count : bvs.Count - 1;
            for (int i = 0; i < tc; i++)
            {
                var ni = close && i == tc - 1 ? 0 : i + 1;

                //Right-Top triangle
                ts.Add(i);
                ts.Add(ni + c);
                ts.Add(i + c);

                //Left-Bottom triangle
                ts.Add(i);
                ts.Add(ni);
                ts.Add(ni + c);
            }
            return ts;
        }

        /// <summary>
        /// Create triangles of camber base pivot to vertices.
        /// </summary>
        /// <param name="pi">Index of pivot.</param>
        /// <param name="vc">Count of vertices.</param>
        /// <param name="vi">Start index of vertices.</param>
        /// <param name="c">Clockwise?</param>
        /// <param name="close">The vertices is close?</param>
        /// <returns></returns>
        public static List<int> CreateCamberTriangles(int pi, int vc, int vi = 0, bool c = true, bool close = true)
        {
            /*
             * Triangle index:
             * (p, i, ni)
             */

            var ts = new List<int>();
            var vp = pi < vi + vc;
            var sn = vp ? 1 : 0;
            var en = vp ? (close ? vc - 1 : vc - 2) : (close ? vc : vc - 1);

            for (int i = sn; i < en; i++)
            {
                var ci = i + vi;
                var ni = (close && i == vc - 1 ? 0 : i + 1) + vi;

                ts.Add(pi);
                ts.Add(c ? ci : ni);
                ts.Add(c ? ni : ci);
            }

            return ts;
        }

        /// <summary>
        /// Create a mesh base vertices and triangles.
        /// </summary>
        /// <param name="vs">The vertices of mesh.</param>
        /// <param name="ts">The triangles of mesh.</param>
        /// <returns></returns>
        public static Mesh CreateMesh(Vector3[] vs, int[] ts)
        {
            var mesh = new Mesh
            {
                vertices = vs,
                triangles = ts
            };

            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            return mesh;
        }
        #endregion

        #region
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