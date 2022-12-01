/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MonoSpringDemo.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  9/21/2021
 *  Description  :  Initial development version.
 *************************************************************************/

using MGS.Curve;
using UnityEngine;

namespace MGS.SkinnedMesh.Demo
{
    public class MonoSpringDemo : MonoBehaviour
    {
        public Transform top;
        public MonoHelixCurve spring;
        public Transform buttom;

        void Update()
        {
            if (Input.GetKey(KeyCode.W))
            {
                if (top.localPosition.y < 0.975f)
                {
                    top.localPosition += Vector3.up * 0.5f * Time.deltaTime;
                    spring.altitude = Vector3.Distance(top.position, buttom.position);
                    spring.Rebuild();
                }
            }
            else if (Input.GetKey(KeyCode.S))
            {
                if (top.localPosition.y > 0.6f)
                {
                    top.localPosition -= Vector3.up * 0.5f * Time.deltaTime;
                    spring.altitude = Vector3.Distance(top.position, buttom.position);
                    spring.Rebuild();
                }
            }
        }
    }
}