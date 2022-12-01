/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  PolygonUtility.cs
 *  Description  :  Utility for polygon.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  7/23/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using UnityEngine;

namespace MGS.Mathematics
{
    /// <summary>
    /// Utility for polygon.
    /// </summary>
    public sealed class PolygonUtility
    {
        /// <summary>
        /// Swell the vertices of polygon to get new group vertices.
        /// </summary>
        /// <param name="d">Distance.</param>
        /// <returns></returns>
        public static List<Vector2> Swell(List<Vector2> vs, float d)
        {
            var nps = new List<Vector2>();
            for (int i = 0; i < vs.Count; i++)
            {
                var pi = (i == 0 ? vs.Count - 1 : i - 1);
                var pv = (vs[i] - vs[pi]).normalized;

                var ni = (i == vs.Count - 1 ? 0 : i + 1);
                var nv = (vs[ni] - vs[i]).normalized;

                var yxv = new Vector2(nv.y, -nv.x);
                var a = Vector2.Dot(pv, yxv);
                var sd = d / a;

                var sv = nv - pv;
                var np = vs[i] + sv * sd;

                nps.Add(np);
            }
            return nps;
        }

        /// <summary>
        /// Check the polygon is  contains the point?
        /// </summary>
        /// <param name="vs"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static bool Contains(List<Vector2> vs, Vector2 p)
        {
            var pvs = new List<Vector2>();
            foreach (var v in vs)
            {
                var pv = (v - p).normalized;
                pvs.Add(pv);
            }

            var cos = 0f;
            for (int i = 0; i < pvs.Count; i++)
            {
                var pvF = pvs[i];
                var vn = i == pvs.Count - 1 ? 0 : i + 1;
                var pvN = pvs[vn];
                var angle = VectorUtility.SignedAngle(pvF, pvN);
                cos += angle;
            }

            //theoretically cos==0
            if (Mathf.Abs(cos) < 1e-3)
            {
                return false;
            }
            return true;
        }
    }
}