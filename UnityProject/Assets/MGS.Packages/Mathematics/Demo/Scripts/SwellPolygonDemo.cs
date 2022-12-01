/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  SwellPolygonDemo.cs
 *  Description  :  Ignore.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  09/02/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using UnityEngine;

namespace MGS.Mathematics.Demo
{
    [RequireComponent(typeof(LineRenderer))]
    public class SwellPolygonDemo : MonoBehaviour
    {
        [ContextMenu("Build Frame")]
        void Start()
        {
            var bvs = new List<Vector3>();
            var bvs2 = new List<Vector2>();
            foreach (Transform child in transform)
            {
                bvs.Add(child.localPosition);
                bvs2.Add(new Vector2(child.localPosition.x, child.localPosition.z));
            }

            var line = GetComponent<LineRenderer>();
            var tvs2 = PolygonUtility.Swell(bvs2, -0.2f);
            var index = 0;
            foreach (var tv in tvs2)
            {
                var pos = new Vector3(tv.x, bvs[index].y, tv.y);
                line.SetPosition(index, pos);
                index++;
            }
            var startPos = new Vector3(tvs2[0].x, bvs[0].y, tvs2[0].y);
            line.SetPosition(index, startPos);
        }
    }
}