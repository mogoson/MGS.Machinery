/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  PolygonAreaDemo.cs
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
    public class PolygonAreaDemo : MonoBehaviour
    {
        public MeshRenderer tangMonk;
        public Material greenMat;
        public Material redMat;
        bool isInside = false;
        List<Vector2> ps;

        [ContextMenu("Build Area")]
        void Start()
        {
            var line = GetComponent<LineRenderer>();
            line.useWorldSpace = false;

            line.SetVertexCount(transform.childCount + 1);
            line.SetPosition(transform.childCount, transform.GetChild(0).localPosition);

            var index = 0;
            ps = new List<Vector2>();
            foreach (Transform child in transform)
            {
                line.SetPosition(index, child.localPosition);
                ps.Add(new Vector2(child.position.x, child.position.z));
                index++;
            }
        }

        void Update()
        {
            var p = new Vector2(tangMonk.transform.position.x, tangMonk.transform.position.z);
            var isIn = PolygonUtility.Contains(ps, p);
            if (isIn != isInside)
            {
                isInside = isIn;
                tangMonk.material = isInside ? greenMat : redMat;
            }
        }
    }
}