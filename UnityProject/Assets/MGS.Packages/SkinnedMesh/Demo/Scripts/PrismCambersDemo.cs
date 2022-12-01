/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  PrismCambersDemo.cs
 *  Description  :  Ignore.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  7/24/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using UnityEngine;

namespace MGS.SkinnedMesh.Demo
{
    public class PrismCambersDemo : MonoBehaviour
    {
        void Start()
        {
            #region
            //Open planar prism.
            CreatePrism(0.5f, 0, 0.3f, 0, "00", false);

            //Close planar prism.
            CreatePrism(0.5f, 0, 0.3f, 0, "01", true);
            #endregion

            #region
            //Open stereoscopic prism.
            CreatePrism(0.5f, 0.1f, 0.3f, 0.3f, "10", false);

            //Close stereoscopic prism.
            CreatePrism(0.5f, 0.3f, 0.3f, 0.3f, "11", true);

            //Close and sealed stereoscopic prism.
            CreatePrism(0.5f, 0.3f, 0.3f, 0.3f, 0, 4, "12", true);

            //Open and sealed stereoscopic prism.
            CreatePrism(0.5f, 0.3f, 0.3f, 0.3f, 0, 4, "13", false);

            //Close and sealed stereoscopic prism.
            CreatePrism(0.5f, 0.3f, 0.3f, 0.3f, -0.5f * Vector3.up, 0.5f * Vector3.up, "14", true);

            //Open and sealed stereoscopic prism.
            CreatePrism(0.5f, 0.3f, 0.3f, 0.3f, -0.5f * Vector3.up, 0.5f * Vector3.up, "15", false);
            #endregion

            #region
            //Open square prism.
            CreatePrism(0.5f, 0.5f, 0.5f, 0.5f, "20", false);

            //Close square prism.
            CreatePrism(0.5f, 0.5f, 0.5f, 0.5f, "21", true);

            //Open and sealed square prism.
            CreatePrism(0.5f, 0.5f, 0.5f, 0.5f, 0, 4, "22", true);

            //Close and sealed square prism.
            CreatePrism(0.5f, 0.5f, 0.5f, 0.5f, 0, 4, "23", false);

            //Open and sealed square prism.
            CreatePrism(0.5f, 0.5f, 0.5f, 0.5f, -0.7f * Vector3.up, 0.7f * Vector3.up, "24", true);

            //Close and sealed square prism.
            CreatePrism(0.5f, 0.5f, 0.5f, 0.5f, -0.7f * Vector3.up, 0.7f * Vector3.up, "25", false);
            #endregion
        }

        void CreatePrism(float bv, float bp, float tv, float tp, string index, bool close)
        {
            ICollection<Vector3> bvs;
            ICollection<Vector3> tvs;
            CreateVectors(bv, bp, tv, tp, out bvs, out tvs);
            CreatePrismCamber(bvs, tvs, index, close);
        }

        void CreatePrism(float bv, float bp, float tv, float tp, int bpi, int tpi, string index, bool close)
        {
            ICollection<Vector3> bvs;
            ICollection<Vector3> tvs;
            CreateVectors(bv, bp, tv, tp, out bvs, out tvs);
            CreatePrismCamber(bvs, tvs, bpi, tpi, index, close);
        }

        void CreatePrism(float bv, float bp, float tv, float tp, Vector3 bpv, Vector3 tpv, string index, bool close)
        {
            ICollection<Vector3> bvs;
            ICollection<Vector3> tvs;
            CreateVectors(bv, bp, tv, tp, out bvs, out tvs);
            CreatePrismCamber(bvs, tvs, bpv, tpv, index, close);
        }

        void CreateVectors(float bv, float bp, float tv, float tp,
            out ICollection<Vector3> bvs, out ICollection<Vector3> tvs)
        {
            bvs = new List<Vector3>()
            {
                new Vector3(-bv, -bp, -bv),
                new Vector3(-bv, -bp, bv),
                new Vector3(bv, -bp, bv),
                new Vector3(bv, -bp, -bv),
            };
            tvs = new List<Vector3>()
            {
                new Vector3(-tv, tp, -tv),
                new Vector3(-tv, tp, tv),
                new Vector3(tv, tp, tv),
                new Vector3(tv, tp, -tv),
            };
        }

        void CreatePrismCamber(ICollection<Vector3> bvs, ICollection<Vector3> tvs, string index, bool close)
        {
            var camber = GeometryMeshUtility.CreatePrismCamber(bvs, tvs, close);
            FindCamber(index).GetComponent<SkinnedMeshRenderer>().sharedMesh = camber;
        }

        void CreatePrismCamber(ICollection<Vector3> bvs, ICollection<Vector3> tvs, int bpi, int tpi, string index, bool close)
        {
            var camber = GeometryMeshUtility.CreatePrismCamber(bvs, tvs, bpi, tpi, close);
            FindCamber(index).GetComponent<SkinnedMeshRenderer>().sharedMesh = camber;
        }

        void CreatePrismCamber(ICollection<Vector3> bvs, ICollection<Vector3> tvs, Vector3 bp, Vector3 tp, string index, bool close)
        {
            var camber = GeometryMeshUtility.CreatePrismCamber(bvs, tvs, bp, tp, close);
            FindCamber(index).GetComponent<SkinnedMeshRenderer>().sharedMesh = camber;
        }

        Transform FindCamber(string index)
        {
            return transform.Find(string.Format("Camber {0}", index));
        }
    }
}