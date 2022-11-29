/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MathSmoothDemo.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  11/29/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using UnityEngine;

namespace MGS.Mathematics.Demo
{
    public class MathSmoothDemo : MonoBehaviour
    {
        public LineRenderer[] lines;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                var ps = new List<Vector3>();
                var ys = new List<float>();
                foreach (Transform item in transform)
                {
                    ps.Add(item.position);
                    ys.Add(item.position.y);
                }

                var lps = LinearSmooth.SevenPointSmooth(ys.ToArray());
                for (int i = 0; i < lps.Length; i++)
                {
                    var pos = new Vector3(ps[i].x, lps[i], ps[i].z);
                    lines[0].SetPosition(i, pos);
                }

                var qps = QuadraticSmooth.SevenPointSmooth(ys.ToArray());
                for (int i = 0; i < qps.Length; i++)
                {
                    var pos = new Vector3(ps[i].x, qps[i], ps[i].z);
                    lines[1].SetPosition(i, pos);
                }

                var cps = CubicSmooth.SevenPointSmooth(ys.ToArray());
                for (int i = 0; i < cps.Length; i++)
                {
                    var pos = new Vector3(ps[i].x, cps[i], ps[i].z);
                    lines[2].SetPosition(i, pos);
                }
            }
        }
    }
}