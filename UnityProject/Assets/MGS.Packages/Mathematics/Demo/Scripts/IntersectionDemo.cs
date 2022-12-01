/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  IntersectionDemo.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  11/29/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Mathematics.Demo
{
    public class IntersectionDemo : MonoBehaviour
    {
        public Transform circle0;
        public Transform circle1;
        public Transform line;
        public Transform[] Intersections;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                foreach (var item in Intersections)
                {
                    item.gameObject.SetActive(false);
                }

                var c0 = new Circle(new Vector2D(circle0.position.x, circle0.position.y), circle0.localScale.z * 0.5f);
                var c1 = new Circle(new Vector2D(circle1.position.x, circle1.position.y), circle1.localScale.z * 0.5f);
                var psCC = GeometryUtility.GetIntersections(c0, c1);
                if (psCC != null && psCC.Count > 0)
                {
                    Intersections[0].gameObject.SetActive(true);
                    Intersections[0].position = new Vector3((float)psCC[0].x, (float)psCC[0].y);
                    if (psCC.Count > 1)
                    {
                        Intersections[1].gameObject.SetActive(true);
                        Intersections[1].position = new Vector3((float)psCC[1].x, (float)psCC[1].y);
                    }
                }

                var offset = line.position + line.right;
                var p0 = new Vector2D(line.position.x, line.position.y);
                var p1 = new Vector2D(offset.x, offset.y);
                var L = Line.FromPoints(p0, p1);
                var psCL = GeometryUtility.GetIntersections(c1, L);
                if (psCL != null && psCL.Count > 0)
                {
                    Intersections[2].gameObject.SetActive(true);
                    Intersections[2].position = new Vector3((float)psCL[0].x, (float)psCL[0].y);
                    if (psCL.Count > 1)
                    {
                        Intersections[3].gameObject.SetActive(true);
                        Intersections[3].position = new Vector3((float)psCL[1].x, (float)psCL[1].y);
                    }
                }
            }
        }
    }
}