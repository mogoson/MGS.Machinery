/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  PyramidCambersDemo.cs
 *  Description  :  Ignore.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  8/14/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using UnityEngine;

namespace MGS.SkinnedMesh.Demo
{
    public class PyramidCambersDemo : MonoBehaviour
    {
        void Start()
        {
            #region
            //Open planar pyramid.
            CreatePyramid(0.5f, 5, Vector3.zero, "00", false);

            //Close planar pyramid.
            CreatePyramid(0.5f, 5, Vector3.zero, "01", true);
            #endregion

            #region
            //Open planar pyramid.
            CreatePyramid(0.5f, 5, Vector3.up * 0.5f, "10", false);

            //Close planar pyramid.
            CreatePyramid(0.5f, 5, Vector3.up * 0.5f, "11", true);
            #endregion

            #region
            //Open planar pyramid.
            CreatePyramid(0.5f, 5, 0, Vector3.up, "20", false);

            //Close planar pyramid.
            CreatePyramid(0.5f, 5, 0, Vector3.up, "21", true);
            #endregion

            #region
            //Open planar pyramid.
            CreatePyramid(0.5f, 5, Vector3.zero, Vector3.up, "30", false);

            //Close planar pyramid.
            CreatePyramid(0.5f, 5, Vector3.zero, Vector3.up, "31", true);
            #endregion

            #region
            //Open planar pyramid.
            CreatePyramid(0.5f, 5, Vector3.down, Vector3.up, "40", false);

            //Close planar pyramid.
            CreatePyramid(0.5f, 5, Vector3.down, Vector3.up, "41", true);
            #endregion
        }

        void CreatePyramid(float r, int c, Vector3 tp, string index, bool close)
        {
            var vs = CreateVectors(r, c);
            CreatePrismCamber(vs, tp, index, close);
        }

        void CreatePyramid(float r, int c, int vpi, Vector3 tp, string index, bool close)
        {
            var vs = CreateVectors(r, c);
            CreatePrismCamber(vs, vpi, tp, index, close);
        }

        void CreatePyramid(float r, int c, Vector3 vp, Vector3 tp, string index, bool close)
        {
            var vs = CreateVectors(r, c);
            CreatePrismCamber(vs, vp, tp, index, close);
        }

        ICollection<Vector3> CreateVectors(float r, int c)
        {
            var vs = new List<Vector3>();
            var twoPI = Mathf.PI * 2;
            var eachA = twoPI / c;
            for (float a = 0; a < twoPI; a += eachA)
            {
                var x = r * Mathf.Sin(a);
                var z = r * Mathf.Cos(a);
                vs.Add(new Vector3(x, 0, z));
            }
            return vs;
        }

        void CreatePrismCamber(ICollection<Vector3> vs, Vector3 tp, string index, bool close)
        {
            var camber = GeometryMeshUtility.CreatePyramidCamber(vs, tp, close);
            FindCamber(index).GetComponent<SkinnedMeshRenderer>().sharedMesh = camber;
        }

        void CreatePrismCamber(ICollection<Vector3> vs, int vpi, Vector3 tp, string index, bool close)
        {
            var camber = GeometryMeshUtility.CreatePyramidCamber(vs, vpi, tp, close);
            FindCamber(index).GetComponent<SkinnedMeshRenderer>().sharedMesh = camber;
        }

        void CreatePrismCamber(ICollection<Vector3> vs, Vector3 vp, Vector3 tp, string index, bool close)
        {
            var camber = GeometryMeshUtility.CreatePyramidCamber(vs, vp, tp, close);
            FindCamber(index).GetComponent<SkinnedMeshRenderer>().sharedMesh = camber;
        }

        Transform FindCamber(string index)
        {
            return transform.Find(string.Format("Camber {0}", index));
        }
    }
}