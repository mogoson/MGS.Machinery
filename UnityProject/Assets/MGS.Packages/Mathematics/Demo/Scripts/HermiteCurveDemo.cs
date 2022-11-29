/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  HermiteCurveDemo.cs
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
    public class HermiteCurveDemo : MonoBehaviour
    {
        LineRenderer lineRenderer;

        void Start()
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                var fs = new List<KeyFrame>();
                foreach (Transform item in transform)
                {
                    fs.Add(new KeyFrame(item.position.x, item.position.y));
                }

                var curve = new HermiteCurve(fs.ToArray());
                curve.SmoothTangents();

                var start = fs[0].time;
                var end = fs[fs.Count - 1].time;
                var delta = (end - start) / 30.0f;
                var timer = start;
                var index = 0;
                while (timer < end)
                {
                    var fm = curve.Evaluate(timer);
                    var pos = new Vector3(timer, fm);
                    lineRenderer.SetPosition(index, pos);
                    timer += delta;
                    index++;
                }
            }
        }
    }
}